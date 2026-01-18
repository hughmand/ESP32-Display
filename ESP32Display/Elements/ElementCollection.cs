using System;

namespace ESP32Display
{
    //Dictionaries are clunky in nanoframework because no generics, and performance isn't a concern here (yet), with larger displays it would be
    /// <summary>
    /// Acts like a dictionary with some additional functionality. No generics in nanoframework, so dictionaries are a bit clunky. Also performance isn't an issue because we only lookup by name in the constructor, not when drawing new frames. 
    /// So this isn't a hashtable implementation.
    /// </summary>
    public interface IElementCollection
    {
        /// <summary>
        /// Add element to collection. Throws exception if element with same name already exists in the collection.
        /// </summary>
        ElementId AddElement(string name, Element element);
        void SetElementPosition(ElementId id, int xPosition, int yPosition);
        /// <summary>
        /// Returns null if element not found
        /// </summary>
        Element GetElement(ElementId id);
        /// <summary>
        /// Returns all elements
        /// </summary>
        Element[] GetElements();
        /// <summary>
        /// Progesses elements into the next state, ready for drawing a new frame. Affects animated and state elements. Recursively generates next frames on child elements.
        /// </summary>
        void NewFrame();
    }

    /// <summary>
    /// Key to use in the collection, easier than having just strings
    /// </summary>
    public struct ElementId
    { 
        public ElementId(string name)
        {
            Name = name;
        }
        public string Name;

        public override bool Equals(object obj)
        {
            if (!(obj is ElementId))
                return false;

            var element = (ElementId)obj ;

            return element.Name == Name;
        }
    }


    //TODO: Hash table implementation
    ///<inheritdoc/>
    class ElementCollection : IElementCollection
    {
        private class Entry
        {
            public Entry()
            {

            }
            public Entry(ElementId elementId, Element element)
            {
                Id = elementId;
                Element = element;
            }
            public ElementId Id;
            public Element Element;
        }

        Entry[] entries = new Entry[0];


        public ElementId AddElement(string name, Element element)
        {
            int currentLength = entries.Length;
            int newLength = currentLength + 1;
            var previousElements = entries;
            var elementId = new ElementId(name);
            if (GetElement(elementId) is not null) throw new ElementException("Cannot add element with duplicate name");

            entries = new Entry[newLength];
            Array.Copy(previousElements, entries, currentLength);
            entries[newLength - 1] = new Entry(elementId, element);
            return elementId;
        }

        public Element GetElement(ElementId id)
        {
            if (TryFindEntry(id, out var entry))
            {
                return entry.Element;
            }
            return null;
        }

        public Element[] GetElements()
        {
            Element[] elementArray = new Element[entries.Length];
            for (int i = 0; i <  elementArray.Length; i++)
            {
                elementArray[i] = entries[i].Element;
            }
            return elementArray;
         }

        //Recursive element reactivity
        public void NewFrame()
        {
            var elements = GetElements();
            if (elements.Length > 0)
            {
                foreach (Element element in elements)
                {
                    if (element is not null && element is IAnimatedElement)
                    {
                        var animatedElement = element as IAnimatedElement;
                        animatedElement.NextFrame();
                    }
                    if (element is not null && element is IStateElement)
                    {
                        var stateElement = element as IStateElement;
                        stateElement.UpdateFromState();
                    }

                    if (element.ChildElements is not null)
                    {
                        element.ChildElements.NewFrame();
                    }
                }
            }
        }

        public void SetElementPosition(ElementId id, int xPosition, int yPosition)
        {
            if (TryFindEntry(id, out var entry))
            {
                entry.Element.SetXY(xPosition, yPosition);
            }
        }

        private bool TryFindEntry(ElementId id, out Entry entry)
        {
            bool foundEntry = false;
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].Id.Equals(id))
                {
                    foundEntry = true;
                    entry = entries[i];
                    return true;
                }
            }
            entry = new Entry();
            return foundEntry;
        }
    }
}
