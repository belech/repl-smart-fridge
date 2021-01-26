[<EntryPoint>]

let main argv =
    printfn "Welcome to the Smart Fridge!"
    printfn "Please enter your commands to interact with the system."
    printfn "Press CTRL+C to stop the program."
    printf "> "

    let eierspeis: DomainRecipeProposer.Recipe = { Name = "Eierspeis"; Ingredients = ["Butter"; "Eier"; "Salz"; "Pfeffer"] }
    let butterBrotMitSalz: DomainRecipeProposer.Recipe = { Name = "ButterbrotMitSalz"; Ingredients = ["Butter"; "Brot"; "Salz"] }
    
    let initialRecipes: DomainRecipeProposer.Recipes = [ butterBrotMitSalz; eierspeis ]
    
    let intialState: DomainRecipeProposer.State = {
        FridgeContent = []
        Recipes = initialRecipes
    }

    ReplRecipeProposer.loop intialState
    0 // return an integer exit code
