module ReplSmartFridge

open System
open ParserSmartFridge

type Message =
    | ReadDomainMessage of DomainSmartFridge.ReadMessage
    | UpdateDomainMessage of DomainSmartFridge.UpdateMessage
    | NotParsable of string

type State = DomainSmartFridge.State

let read (input : string) =
    match input with
    | AddFood v -> DomainSmartFridge.AddFood v |> UpdateDomainMessage
    | RemoveFood v -> DomainSmartFridge.RemoveFood v |> UpdateDomainMessage
    | AddRecipe v -> DomainSmartFridge.AddRecipe v |> UpdateDomainMessage
    | RemoveRecipe v -> DomainSmartFridge.RemoveRecipe v |> UpdateDomainMessage
    | GetAllRecipes -> DomainSmartFridge.GetAllRecipes |> ReadDomainMessage
    | GetPossibleRecipes -> DomainSmartFridge.GetPossibleRecipes |> ReadDomainMessage
    | ParseFailed -> NotParsable input

open Microsoft.FSharp.Reflection

let createHelpText () : string =
    FSharpType.GetUnionCases typeof<DomainSmartFridge.Message>
    |> Array.map (fun case -> case.Name)
    |> Array.fold (fun prev curr -> prev + " " + curr) ""
    |> (fun s -> s.Trim() |> sprintf "Known commands are: %s")

let evaluate (state : State) (msg : Message) =
    match msg with
    | UpdateDomainMessage msg ->
        let newState = DomainSmartFridge.update msg state
        //todo remove
        let message = sprintf "The message was %A. New state is %A" msg newState
        (newState, message)
    | ReadDomainMessage msg ->
        let recipes = DomainSmartFridge.read msg state
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
