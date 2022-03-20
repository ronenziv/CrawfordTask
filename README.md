# CrawfordTask
## Build and run
Clone or Download the repository,

Build the solution using visual studio 2019.

You can run the project by dotnet run command.

navigate to http://localhost:4000/swagger/index.html to see the api in action.

First run authenticate web api to get a JWT token. All others route API have secure route.
The HTTP Authorization header have to contains valid JWT authentication credentials (copy the token to an authorize -> value textbox)
