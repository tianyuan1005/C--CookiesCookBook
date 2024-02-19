

using System;
using System.Text.Json;


public class Ingredient
{
    private static int nextId = 0;
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Instruction { get; private set; }

    public Ingredient(string name,string instruction)
    {
        Name = name;
        Instruction = instruction;
        ID = nextId++;
    }
}

public class Recipe
{
    public List<Ingredient> Ingredients { get; private set; }
    public Recipe()
    {
        Ingredients = new List<Ingredient>();
    }
   public void AddIngredientToRecipe(Ingredient ingredient
       )
    {
        Ingredients.Add(ingredient);
        
    }

    public void ShowRecipe()
    {
        foreach(var ingredient in Ingredients)
        {
            Console.WriteLine($"{ingredient.Name}.{ingredient.Instruction}");
        }
    }
}

 public class FileReader
{
    public string FileName { get; private set; }

    public FileReader(string fileName)
    {
        FileName = fileName;
    }

    public List<List<int>> ReadFromFile()
    {
        // 存储从文件中读取的所有整数列表
        List<List<int>> allLists = new List<List<int>>();

        // 按行读取文件
        string[] lines = File.ReadAllLines(FileName);

        // 对每一行进行反序列化
        foreach (string line in lines)
        {
            List<int> numbers = JsonSerializer.Deserialize<List<int>>(line);
            allLists.Add(numbers);
        }

        return allLists;
    }
}


public class IngredientManager
{
    private List<Ingredient> _ingredients = new List<Ingredient>
    {
        new Ingredient("Wheat flour","Sieve. Add to other ingredients."),
        new Ingredient("Coconut flour","Sieve. Add to other ingredients."),
        new Ingredient("Butter","Melt on low heat. Add to other ingredients."),
        new Ingredient("Chocolate","Melt in a water bath. Add to other ingredients"),
        new Ingredient("Sugar","Add to other ingredients."),
        new Ingredient("Cardamom","Take half a teaspoon. Add to other ingredients."),
        new Ingredient("Cinnamon","Take half a teaspoon. Add to other ingredients."),
        new Ingredient("Cocoa powder","Add to other ingredients."),

    };

    public void ShowIngredients()
    {
        foreach(var ingredient in _ingredients)
        {
            Console.WriteLine($"{ingredient.Name}.{ingredient.Instruction}");
        }
    }
}

public class InputReader
{
    public MessagePrinter MessagePrinter { get; private set; }


    public InputReader(MessagePrinter messagePrinter = null)
    {
        MessagePrinter = messagePrinter;
    }

    public string GetInput()
    {
        return Console.ReadLine();
    }


}

public class MessagePrinter
{
    public string Message { get; private set; }

    public MessagePrinter(string message)
    {
        Message = message;
        Console.WriteLine(Message);
    }
}

public class InputStore
{
    public InputReader InputReader { get; private set; }
    public List<int> List { get; private set; }
    private int _maxNumber = 8;

    public InputStore(InputReader inputReader,int maxNumber)
    {
        InputReader = inputReader;
        List = new List<int>();
        _maxNumber = maxNumber;
    }

    public void AddToList()
    {
        while (true)
        {
            string input = InputReader.GetInput();
            bool isValid = int.TryParse(input, out int number);

            if (!isValid || number < 1 || number >= _maxNumber)
            {
                break; 
            }

            List.Add(number);
        }

    }
}

public class FileStorage
{
    public string Path { get; private set; }
    public InputStore InputStore  { get; private set; }

    public FileStorage(string path,InputStore inputStore)
    {
        Path = path;
        InputStore = inputStore;
    }


    public void Save()
    {
        string jsonString = JsonSerializer.Serialize(InputStore.List);
        File.WriteAllText(Path, jsonString);
    }

}

public class EnumConverter
{
    public static Ingredient ConvertToIngredient(int ingredientNumber)
    {
        switch (ingredientNumber)
        {
            case (int)Ingredients.WheatFlour:
                return new Ingredient("Wheat flour", "Sieve. Add to other ingredients.");
            case (int)Ingredients.CoconutFlour:
                return new Ingredient("Coconut flour", "Sieve. Add to other ingredients.");
            case (int)Ingredients.Butter:
                return new Ingredient("Butter", "Melt on low heat. Add to other ingredients.");
            case (int)Ingredients.Chocolate:
                return new Ingredient("Chocolate", "Melt in a water bath. Add to other ingredients.");
            case (int)Ingredients.Sugar:
                return new Ingredient("Sugar", "Add to other ingredients.");
            case (int)Ingredients.Cardamom:
                return new Ingredient("Cardamom", "Take half a teaspoon. Add to other ingredients.");
            case (int)Ingredients.Cinnamon:
                return new Ingredient("Cinnamon", "Take half a teaspoon. Add to other ingredients.");
            case (int)Ingredients.CocoaPowder:
                return new Ingredient("Cocoa powder", "Add to other ingredients.");

            default:
                throw new ArgumentException("Unknown ingredient");
        }
    }
}

public enum Ingredients
{
    WheatFlour = 1,
    CoconutFlour = 2,
    Butter = 3,
    Chocolate = 4,
    Sugar = 5,
    Cardamom = 6,
    Cinnamon = 7,
    CocoaPowder = 8,
}

public class RecipeManager
{
    public List<Recipe> Recipes { get; private set; }
    public FileReader FileReader { get; private set; }

    public RecipeManager(FileReader fileReader)
    {
        FileReader = fileReader;
        Recipes = new List<Recipe>();
    }

    //create
    public void GenerateRecipe()
    {
        List<List<int>> allLists = FileReader.ReadFromFile();

        foreach(var list in allLists)
        {
            Recipe recipe = new Recipe();
            foreach(var item in list) {
                var ingredient = EnumConverter.ConvertToIngredient(item);
                recipe.AddIngredientToRecipe(ingredient);
            }
            Recipes.Add(recipe);

        }

    }
    //show
    public void ShowRecipe()
    {
        int number = 1;
        foreach(var recipe in Recipes)
        {
            Console.WriteLine($"********{number++}********");
            recipe.ShowRecipe();
        }
    }
}