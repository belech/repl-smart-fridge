[<EntryPoint>]

let main argv =
    printfn "Welcome to the Smart Fridge!"
    printfn "   _____ __  __          _____ _______   ______ _____  _____ _____   _____ ______ "
    printfn "  / ____|  \/  |   /\   |  __ \__   __| |  ____|  __ \|_   _|  __ \ / ____|  ____|"
    printfn " | (___ | \  / |  /  \  | |__) | | |    | |__  | |__) | | | | |  | | |  __| |__   "
    printfn "  \___ \| |\/| | / /\ \ |  _  /  | |    |  __| |  _  /  | | | |  | | | |_ |  __|  "
    printfn "  ____) | |  | |/ ____ \| | \ \  | |    | |    | | \ \ _| |_| |__| | |__| | |____ "
    printfn " |_____/|_|  |_/_/    \_\_|  \_\ |_|    |_|    |_|  \_\_____|_____/ \_____|______|"
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
