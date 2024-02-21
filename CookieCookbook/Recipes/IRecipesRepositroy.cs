namespace CookieCookbook.Recipes
{
    public interface IRecipesRepositroy
    {
        List<Recipe> Read(string filePath);
        void Write(string filePath, List<Recipe> allRecipes);

    }
}


