module DomainSmartFridge

type Food = string

type Recipe = {
    Name: string;
    Ingredients: list<Food>
}

type Recipes = list<Recipe>

type State = {
    FridgeContent: Food list
    Recipes: Recipes
}

type UpdateMessage =
    | AddFood of Food
    | RemoveFood of Food
    | AddRecipe of Recipe
    | RemoveRecipe of string

type ReadMessage =
    | GetAllRecipes
    | GetPossibleRecipes

type Message =
    | UpdateMessage
    | ReadMessage

let addFood (foodToAdd: Food) (oldState: State): State = {
    FridgeContent = foodToAdd::oldState.FridgeContent
    Recipes = oldState.Recipes
}

let addRecipe (recipeToAdd: Recipe) (oldState: State): State = {
    FridgeContent = oldState.FridgeContent
    Recipes = recipeToAdd::oldState.Recipes
}

let removeFood(foodToRemove: Food) (oldState: State): State = {
    FridgeContent = oldState.FridgeContent |> List.filter (fun food -> food <> foodToRemove)
    Recipes = oldState.Recipes
}

let removeRecipe (recipeNameToRemove: string) (oldState: State): State = {
    FridgeContent = oldState.FridgeContent
    Recipes = oldState.Recipes |> List.filter (fun recipe -> recipe.Name <> recipeNameToRemove)
}

let getAllRecipes (state: State): Recipes = state.Recipes

let rec checkReceiptContainsAllAvailableFood (availableFood: list<Food>) (recipe: Recipe): bool =
    let neededFood = Set.ofList recipe.Ingredients
    let availableFood = Set.ofList availableFood
    Set.isSubset neededFood availableFood

let getPossibleRecipes (state: State): Recipes = List.filter (checkReceiptContainsAllAvailableFood state.FridgeContent) state.Recipes

let update (msg : UpdateMessage) (model : State) : State =
    match msg with
    | AddFood food -> addFood food model
    | RemoveFood food -> removeFood food model
    | AddRecipe recipe -> addRecipe recipe model
    | RemoveRecipe recipeName -> removeRecipe recipeName model

let read (msg : ReadMessage) (model: State) : Recipes =
    match msg with
    | GetAllRecipes -> getAllRecipes model
    | GetPossibleRecipes -> getPossibleRecipes model
