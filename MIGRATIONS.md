## Add new Migration
$ dotnet ef migrations add InitialCreate --context <context>

## Update to the latest Migration
$ dotnet ef database update --connection "Server=localhost,1433;Database=CustomerManagamentService;User=sa;Password=enter_password;"
