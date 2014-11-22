using System;
using System.Collections.Generic;

namespace RecipeManager
{
public interface IRecipeManagerUI
{
    void PopulateList(List<Recipe> recipes);

    event Action NewClick;

    string RecipeName { get; set; }
    string RecipeDirections { get; set; }
}
}