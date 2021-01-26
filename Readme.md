# SmartFridge

## Rationale

SmartFridge is a project showing a functional example of a F# program. It includes:

- A REPL for creating messages and reading the state of the system
- A simple food proposal domain
- Auxiliary files for git, .NET editors and the .NET Core SDK

## Prerequisites

- .NET core SDK (as specified in `global.json`)
- Preferred F# editor

## Run the application

Execute the following command.
```bash
dotnet run --project src/SmartFridge/SmartFridge.fsproj
```


## Example commands

AddFood Milch
AddFood Zucker
AddFood Salz
AddFood Hefe
AddFood Mehl

RemoveFood Milch

AddRecipe Brot Mehl Salz Hefe

RemoveRecipe ButterbrotMitSalz

GetAllRecipes

GetPossibleRecipes