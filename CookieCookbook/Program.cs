using CookieCookbook.Recipes;
using CookieCookbook.Recipes.Ingredients;

var cookiesRecipesApp = new CookiesRecipesApp(new RecipesRepositry(), new RecipesConsoleUserInteraction(new IngredientsRegister()));
cookiesRecipesApp.Run("recipes.txt");

public class CookiesRecipesApp
{
    private readonly IRecipesRepositroy _recipesRepository;
    private readonly IRecipesUserInteraction _recipesUserInteraction;

    public CookiesRecipesApp(IRecipesRepositroy recipesRepository, IRecipesUserInteraction recipesUserInteraction)
    {
        _recipesRepository = recipesRepository;
        _recipesUserInteraction = recipesUserInteraction;

    }

    public void Run(string filePath)
    {
        var allRecipes = _recipesRepository.Read(filePath);
        _recipesUserInteraction.PrintExistingRecipes(allRecipes);
        _recipesUserInteraction.PromptToCreateRecipe();

        //var ingredients = _recipesUserInteraction.ReadIngredientsFromUser();

        //if(ingredients.Count > 0)
        //{
        //    var recipe = new Recipe(ingredients);
        //    allRecipes.Add(recipe);
        //    _recipesUserInteraction.Write(filePath,allRecipes);
        //    _recipesUserInteraction.ShowMessage("Recipe added:");
        //    _recipesUserInteraction.ShowMessage(recipe.ToString());
        //}
        //else
        //{
        //    _recipesUserInteraction.ShowMessage("No ingredients have been selected. " + "Recipe will not be saved");
        //}

        _recipesUserInteraction.Exit();

    }
}

public interface IRecipesUserInteraction
{
    void ShowMessage(string message);
    void Exit();
    void PrintExistingRecipes(IEnumerable<Recipe> allRecipes);
    void PromptToCreateRecipe();
}

public class IngredientsRegister
{
    public IEnumerable<Ingredient> All { get; } = new List<Ingredient>
    {
        new WheatFlour(),
        new SpeltFlour(),
        new Butter(),
        new Chocolate(),
        new Sugar(),
        new Cardamom(),
        new Cinnamon(),
        new CocoaPowder()
    };
}

public class RecipesConsoleUserInteraction:IRecipesUserInteraction
{
    private readonly IngredientsRegister _ingredientsRegister;

    public RecipesConsoleUserInteraction(IngredientsRegister ingredientsRegister)
    {
        _ingredientsRegister = ingredientsRegister;
    }

    public void Exit()
    {
        Console.WriteLine("Press any key to close.");
        Console.ReadKey();
    }

    public void PrintExistingRecipes(IEnumerable<Recipe> allRecipes)
    {
        if (allRecipes.Count() > 0)
        {
            Console.WriteLine("Existing recipes are:" + Environment.NewLine);

            var counter = 1;
            foreach(var recipe in allRecipes)
            {
                Console.WriteLine($"******{counter}******");
                Console.WriteLine(recipe);
                Console.WriteLine();
                ++counter;
            }
        }
    }

   
    public void PromptToCreateRecipe()
    {
        Console.WriteLine("Create a new cookie recipe!" + "Available ingredients are:");

        foreach(var ingredient in _ingredientsRegister.All)
        {
            Console.WriteLine(ingredient);
        }
    }

    public RecipesRepositry ReadIngredientsFromUser()
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(object filePath, object allRecipes)
    {
        throw new NotImplementedException();
    }
}

public interface IRecipesRepositroy
{
    List<Recipe> Read(string filePath);
}

public class RecipesRepositry : IRecipesRepositroy
{
    public List<Recipe> Read(string filePath)
    {
        return new List<Recipe>
        {
            new Recipe(new List<Ingredient>
            {
                new WheatFlour(),
                new Butter(),
                new Sugar(),
            }),
            new Recipe(new List<Ingredient>
            {
                new CocoaPowder(),
                new SpeltFlour(),
                new Cinnamon(),
            })
        };
    }
}