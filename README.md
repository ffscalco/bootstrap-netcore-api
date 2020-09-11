# Imperius

- Frontend repository can be found at [TODO]()
- Staging API is deployed to [TODO]()
- Production environment not deployed yet

## Dependencies

- [PostgreSQL](https://www.postgresql.org/download/)

Technologies used

- [.NET Core 3.1](https://dotnet.microsoft.com/download)
- [Entity Framework Core 3.1.8](https://docs.microsoft.com/ef/)
- PostgreSQL 12.3

## Project setup

1. Install the Entity Framework (EF) tool:
```sh
$ dotnet tool install --global dotnet-ef
```
Type `dotnet ef` in your terminal, you should see the unicorn.

2. To run the project, you can start it through Visual Studio or run the following command in your terminal:
```sh
$ dotnet run -p src/Api/Api.csproj
```

3. To add migrations via terminal

```sh
$ dotnet ef migrations add MigrationName --project src/Data --startup-project src/Api
```

4. To rollback a specific migration

If you need to undo an applied migration, use the below command to go back to a specific migration, which will undo all migrations up until the target migration. In the below example, all migrations after `AddedKillsheetToKillsheetLot` will be rolled back. See a discussion on StackOverflow [here](https://stackoverflow.com/a/40020931/660936).

```
$ dotnet ef database update TargetMigrationName --project src/Data --startup-project src/Api
```

5. To run the project tests, you can start them through Visual Studio or run the following command in your terminal:
```sh
$ cd src; dotnet test
```

5.1 To run the tests with coverage, use the following command:
```sh
$ cd src; dotnet test /p:CollectCoverage=true
```

## Project Architecture

This solution has 4 projects:

- Api: the presentation layer, which is where the app handles API Restful calls, resource mappings and resources validation.
- Core: is where you have yours models, interfaces and service.
- Data: is where it deals with the database migrations, configurations and repositories.
- Service: is the center of the business logic. it abstracts the business logic from the presentation layer, which is the API.
