# SistemaMAV

## Página con la explicación de Scaffolding con dotnet:
https://gavilanch.wordpress.com/2018/04/28/asp-net-core-2-haciendo-scaffolding-con-el-dotnet-cli-aspnet-codegenerator/

## Creación de los scaffolds

### Marca:

dotnet aspnet-codegenerator controller -name MarcaController -actions -outDir Controllers -m SistemaMAV.Models.Marca -dc SistemaMAV.Models.MavDbContext -udl

dotnet build
dotnet aspnet-codegenerator view Create Create -udl -outDir Views/Marca -m SistemaMAV.Models.Marca
dotnet aspnet-codegenerator view Edit Edit -udl -outDir Views/Marca -m SistemaMAV.Models.Marca
dotnet aspnet-codegenerator view Delete Delete -udl -outDir Views/Marca -m SistemaMAV.Models.Marca
dotnet aspnet-codegenerator view List List -udl -outDir Views/Marca -m SistemaMAV.Models.Marca
dotnet aspnet-codegenerator view Details Details -udl -outDir Views/Marca -m SistemaMAV.Models.Marca
dotnet build

## Inicialización de la base de datos

Esto lo saqué de la siguiente ubicación:
https://docs.microsoft.com/es-es/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-3.0&tabs=visual-studio-code

dotnet ef migrations add InitialCreate
dotnet ef database update
