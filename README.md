# SistemaMAV

## Página con la explicación de Scaffolding con dotnet:
https://gavilanch.wordpress.com/2018/04/28/asp-net-core-2-haciendo-scaffolding-con-el-dotnet-cli-aspnet-codegenerator/

## Creación de los scaffolds

### Marca:

```
dotnet aspnet-codegenerator controller -name TipoUnidadController -actions -outDir Controllers -m SistemaMAV.Entities.Models.TipoUnidad -dc SistemaMAV.DataAccess.Data.MavDbContext -udl

dotnet build
dotnet aspnet-codegenerator view Create Create -udl -outDir Views/TipoUnidad -m SistemaMAV.Entities.Models.TipoUnidad
dotnet aspnet-codegenerator view Edit Edit -udl -outDir Views/TipoUnidad -m SistemaMAV.Entities.Models.TipoUnidad
dotnet aspnet-codegenerator view Delete Delete -udl -outDir Views/TipoUnidad -m SistemaMAV.Entities.Models.TipoUnidad
dotnet aspnet-codegenerator view List List -udl -outDir Views/TipoUnidad -m SistemaMAV.Entities.Models.TipoUnidad
dotnet aspnet-codegenerator view Details Details -udl -outDir Views/TipoUnidad -m SistemaMAV.Entities.Models.TipoUnidad
dotnet build
```

## Inicialización de la base de datos

Esto lo saqué de la siguiente ubicación:
https://docs.microsoft.com/es-es/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.0&tabs=visual-studio-code

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Si esto arroja error, ejecutar lo siguiente:
```
dotnet tool install --global dotnet-ef
```

Est otra página muestra como crear la base de datos directamente:
https://www.talkingdotnet.com/create-sqlite-db-entity-framework-core-code-first/

## Agregar un nuevo proyecto de librería de clases

Para ver la lista de los tipos de proyectos que se pueden crear
```
dotnet new --list
```

Para agregar un nuevo project
```
dotnet new classlib -o Entities
```

Luego, si el nuevo project es una librería de cases, hay que agregar la referencia de netstandard2.0 en el nuevo project, agregando lo siguiente en el archivo .csproj:
```xml
  <ItemGroup>
    <Reference Include="netstandard, Version=2.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51" />
  </ItemGroup>
```

Para referenciar el nuevo project a otro
```
dotnet add DataAccess\SistemaMAV.DataAccess.csproj reference Entities\SistemaMAV.Entities.csproj

dotnet add UI\Web\SistemaMAV.UI.Web.csproj reference Entities\SistemaMAV.Entities.csproj
```

Finalmente, hay que agregar el nuevo proyecto a la solución:
```
dotnet sln SistemaMAV.sln add Entities/SistemaMAV.Entities.csproj
```
