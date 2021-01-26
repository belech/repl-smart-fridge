module ParserSmartFridge

open System

let safeEquals (it : string) (theOther : string) =
    String.Equals(it, theOther, StringComparison.OrdinalIgnoreCase)

let (|AddFood|RemoveFood|AddRecipe|RemoveRecipe|GetAllRecipes|GetPossibleRecipes|ParseFailed|) (input : string) =
    let tryParseArgument (arg : string) valueConstructor =
        if arg.Length > 0 then valueConstructor arg else ParseFailed

    let tryParseRecipe (arg1: string) (arg2: string) valueConstructor =
        if arg1.Length > 0 && arg2.Length > 0 then
            let ingredients = arg2.Split(',') |> List.ofArray
            let newRecipe: DomainSmartFridge.Recipe = {
                Name = arg1
                Ingredients = ingredients
            }
            valueConstructor newRecipe
        else ParseFailed

    let parts = input.Split(' ') |> List.ofArray
    match parts with
    | [ verb ] when safeEquals verb ("GetAllRecipes") -> GetAllRecipes
    | [ verb ] when safeEquals verb ("GetPossibleRecipes") -> GetPossibleRecipes
    | [ verb; arg ] when safeEquals verb ("AddFood") -> tryParseArgument arg (fun value -> AddFood value)
    | [ verb; arg ] when safeEquals verb ("RemoveFood") -> tryParseArgument arg (fun value -> RemoveFood value)
    | [ verb; arg1; arg2] when safeEquals verb ("AddRecipe") -> tryParseRecipe arg1 arg2 (fun value -> AddRecipe value)
    | [ verb; arg ] when safeEquals verb ("RemoveRecipe") -> tryParseArgument arg (fun value -> RemoveRecipe value)
    | _ -> ParseFailed
