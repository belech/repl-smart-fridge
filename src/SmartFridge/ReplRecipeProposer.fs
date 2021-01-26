module ReplRecipeProposer

open System
open ParserRecipeProposer

type Message =
    | ReadDomainMessage of DomainRecipeProposer.ReadMessage
    | UpdateDomainMessage of DomainRecipeProposer.UpdateMessage
    | NotParsable of string

type State = DomainRecipeProposer.State

let read (input : string) =
    match input with
    | AddFood v -> DomainRecipeProposer.AddFood v |> UpdateDomainMessage
    | RemoveFood v -> DomainRecipeProposer.RemoveFood v |> UpdateDomainMessage
    | AddRecipe v -> DomainRecipeProposer.AddRecipe v |> UpdateDomainMessage
    | RemoveRecipe v -> DomainRecipeProposer.RemoveRecipe v |> UpdateDomainMessage
    | GetAllRecipes -> DomainRecipeProposer.GetAllRecipes |> ReadDomainMessage
    | GetPossibleRecipes -> DomainRecipeProposer.GetPossibleRecipes |> ReadDomainMessage
    | ParseFailed -> NotParsable input

open Microsoft.FSharp.Reflection

let createHelpText () : string =
    FSharpType.GetUnionCases typeof<DomainRecipeProposer.Message>
    |> Array.map (fun case -> case.Name)
    |> Array.fold (fun prev curr -> prev + " " + curr) ""
    |> (fun s -> s.Trim() |> sprintf "Known commands are: %s")

let evaluate (state : State) (msg : Message) =
    match msg with
    | UpdateDomainMessage msg ->
        let newState = DomainRecipeProposer.update msg state
        //todo remove
        let message = sprintf "The message was %A. New state is %A" msg newState
        (newState, message)
    | ReadDomainMessage msg ->
        let recipes = DomainRecipeProposer.read msg state
        let message = sprintf "Found recipes: \n%A" recipes
        (state, message)
    | NotParsable originalInput ->
        let message =
            sprintf """"%s" was not parsable. %s"""  originalInput "You can get information about known commands by typing \"Help\""
        (state, message)

let print (state : State, outputToPrint : string) =
    printfn "%s\n" outputToPrint
    printf "> "

    state

let rec loop (state : State) =
    Console.ReadLine()
    |> read
    |> evaluate state
    |> print
    |> loop