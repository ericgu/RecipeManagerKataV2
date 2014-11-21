namespace RecipeManager
{
    public interface IRecipeStoreLocator
    {
        string GetRecipeDirectory();
        void SetRecipeDirectory(string recipeDirectory);
        void ResetToDefault();
    }
}