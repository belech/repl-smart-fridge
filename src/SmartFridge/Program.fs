[<EntryPoint>]

let main argv =
    printfn "Welcome to the Smart Fridge!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    printf "> "

    let eierspeis: DomainSmartFridge.Recipe = { Name = "Eierspeis"; Ingredients = ["Butter"; "Eier"; "Salz"; "Pfeffer"] }
    let butterBrotMitSalz: DomainSmartFridge.Recipe = { Name = "ButterbrotMitSalz"; Ingredients = ["Butter"; "Brot"; "Salz"] }

    let initialRecipes: DomainSmartFridge.Recipes = [ butterBrotMitSalz; eierspeis ]

    let intialState: DomainSmartFridge.State = {
        FridgeContent = []
        Recipes = initialRecipes
    }

    ReplSmartFridge.loop intialState
    0 // return an integer exit code
