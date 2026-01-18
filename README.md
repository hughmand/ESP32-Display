# ESP32 Display Project

## Summary
This is an embedded software project using the ESP32 platform and Nanoframework. The hardware is shown in Setup.jpg. Display board is a MAX7219 Dot matrix, 32 x 8 LED pixels.

Just a for fun project. Currently working on implementing some mini arcade games. Written in C# using nanoframework runtime. 

## General Structure
There are two main threads running:
1) The main execution thread, which runs worker classes. These worker classes have responsibility for updating the state of the system. 
2) The second thread is the display thread. This has a responsibility for taking the state of the system and pushing up-to-date information to the display.

The content on the display is set up using the element classes. Each class defines a group of pixels, and their position in their parent element (or on the screen, if no parent element). 
The element can be dependent on the state classes, and when updated the elements are re-drawn recursively with the new state.
