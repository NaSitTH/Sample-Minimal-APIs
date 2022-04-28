## Install Package

.NET CLI

```sh
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Package Manager

```sh
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

## Install Tool

```sh
dotnet tool install -g dotnet-ef
```

update tool

```sh
dotnet tool update -g dotnet-ef
```

## Command

##### Migration

```sh
dotnet ef migrations add InitialCreate
```

specify folder ouput

```sh
dotnet ef migrations add InitialCreate -o Data/Migrations
```

remove migration

```sh
dotnet ef migrations remove
```

##### Update Database

```sh
dotnet ef database update
```

drop database

```sh
dotnet ef database drop
```
