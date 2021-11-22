
# Channel Users

### High-level structure:

- Channel.Users.Console: Current entry-point to the application.
- Channel.Users.Application: Application layer in charge of handling requests coming from the user layers (at the moment only Channel.Users.Console).
- Channel.Users.Domain: Contains the business logic. Though in this case it's minimal, it resolves reporting concerns.
- Channel.Users.HttpDataProvider: User data provider. This could be replaced by other data sources (dbs, files, etc)
- Channel.Users.Domain.Tests: Unit tests. Only a couple added for demostration purposes and lack of time.

### Running the application

Go to the \Channel.Users.Console directory and run the following: 

```json
dotnet run get-users-report
```

That will build and run the console application.