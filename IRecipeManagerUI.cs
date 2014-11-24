using System;
using System.Collections.Generic;

namespace RecipeManager
{
public interface IRecipeManagerUI
{
    void PopulateList(List<Recipe> recipes);

    event Action NewClick;
    event Action SaveClick;
    event Action<Recipe> RecipeSelected;
    event Action<Recipe> DeleteClick;

    string RecipeName { get; set; }
    string RecipeDirections { get; set; }
}
}