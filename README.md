# TechnicalAssessmentAirTek
Technical Assessment for Senior Software Engineer at AirTek by Prithwiraj Das 

## AirTeK Console Application
This is a console application that schedules orders on flights based on the destination city. It is written in C# and uses Newtonsoft.Json package to deserialize the JSON data.

### Usage
- Open Visual Studio and navigate to "File" -> "Open" -> "Project/Solution"
- Browse to the location of the AirTeKConsoleApp solution on your computer and select it.
- Once the solution is open, make sure that the AirTeKConsoleApp project is selected in the Solution Explorer.
- Next, build the project by selecting "Build" -> "Build Solution" from the top menu bar or by pressing "Ctrl + Shift + B".
- After building the project, place the order list json file named "orders.json" in the following location: "~\AirTeKConsoleApp\AirTeKConsoleApp\bin\Debug\net7.0". 
- Finally, to run the application, press "F5" or select "Debug" -> "Start Debugging" from the top menu bar.


### Possible updates
Move the code to separate Service classes.
Make the departure and arrival cities configurable by passing them as arguments to the function, so that we can use the same function to schedule flights from other cities.
Add error handling for JSON deserialization errors.
