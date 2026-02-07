namespace ESP32Display
{
    public interface IRiverLevelJsonDeserializer
    {
        RiverLevel PopulateRiverLevels(RiverIdentifier identifier, string jsonSource);
    }

    public class RiverLevelJsonDeserializer : IRiverLevelJsonDeserializer
    {

        public RiverLevel PopulateRiverLevels(RiverIdentifier identifier, string jsonSource)
        {
            var riverLevel = new RiverLevel();
            riverLevel.Identifier = identifier;
            var itemsJson = RemoveOpeningInformation(jsonSource);

            return new RiverLevel();
            
        }


        private string RemoveOpeningInformation(string jsonSource)
        {
            var indexOfItems = jsonSource.IndexOf("items");
            var itemsJson = jsonSource.Substring(indexOfItems);
            return itemsJson;
        }

        private RiverLevelReading GetReadingValues(string item)
        {
            var riverLevelReading = new RiverLevelReading();
            return riverLevelReading;


        }


        private string GetValue(string key, string item)
        {
            var indexOfKey = item.IndexOf('\"' + key + '\"');
            var keyValueString = item.Substring(indexOfKey);
            var indexOfNextComma = keyValueString.IndexOf(',');
            var valueString = keyValueString.Substring(0, indexOfNextComma);
            return string.Empty;
        }
    }
}
