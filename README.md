# earthquake-monitor
Application to monitor the latest global earthquake activities and potentially affected cities.

###•	Dependencies and references used.
a.	Kd-Tree, Generic multi-dimensional binary search tree. This library is downloaded from nugget packet manager. This package has no other dependencies.

b.	Json.NET 8.0.3, popular high-performance JSON framework for .NET. This library is installed from nugget packet manager and it does not have any other dependencies.

c.	This application is using an external web service to generate and/or update the result.
url :  http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_hour.geojson
Http verb: GET
Format: JSON

d.	Application is using an external resource (worldcities.csv) which added to the solution as a resource. This file is using to upload the cities and its coordinates into the data structure.
Location: http://www.opengeocode.org/download/worldcities.zip

###•	To building and utilizing your application
a.	This application is developed on .NETFramework, Version=v4.5.2 and using WPF framework.

b.	Internet connection is required to run and update the result periodically.

c.	When the application is first loaded, it will display the earthquake activities from the past hour. Application will be updating the result automatically on top of the list periodically (every 5 minutes) for ongoing earthquake activities.

d.	Url to get the GeoJSON (key="AllHourGeojsonUrl"), interval to refresh the result (key="SourceUpdatIntervalMin"), number of nearest countries to display for each incidents (key="NearestCityCount") are stored in App.config. These variables can be updated without re-building the application.


