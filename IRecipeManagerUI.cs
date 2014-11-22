using System.Collections.Generic;

namespace RecipeManager
{
    public interface IRecipeManagerUI
    {
        void PopulateList(List<Recipe> recipes);
    }
}