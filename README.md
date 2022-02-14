# SureCar

This project was generated with [.Net 6](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6#:~:text=Better%20performance%3A%20.,tools%2C%20and%20better%20team%20collaboration.)

Database: [MS SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

How to run: 
1. Create database `SureCar`. You can check it on `appsettings.json` file.
2. Before running you need to set `SureCar.Repositories` as the startup project. After that open `package manager` and run the command `Update-Database`. As the result, you have to set `SureCar.API` as the startup project.and run project [Update Databse](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs)
3. By default connection to a database, the project uses `Trusted_Connection` you can change it on `appsettings.json` file and `DataContext` class `OnConfiguring` method.
4. Unit tests located in `Tests` folder.


 
