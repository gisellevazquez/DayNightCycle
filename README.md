# Simple Unity Day/Night Cycle 
### with Enviroment options

Day Night script adds a normal day/Night cycle with the posibility to add Elements (lights) to turn it in and off based on the cycle. 

The hours can be changed on the line:

        bool isDaytime = (timeOfDay >= 6 && timeOfDay < 18);

Sound trigger acts similar by activating GameObjects (serialized in editor) with time range handlers.
With this script any object can be activated *Not only sounds*, since it activates a GameObject that has an Audiosource (Loop and Play on awake should be checked)

Sound trigger access to DayNightCycle to know the day hour.
