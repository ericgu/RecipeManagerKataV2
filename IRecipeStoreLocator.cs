namespace RecipeManager
{
    public interface IRecipeStoreLocator
    {
        string GetRecipeLocation();
        void SetRecipeLocation(string recipeLocation);
        void ResetToDefault();
    }
}