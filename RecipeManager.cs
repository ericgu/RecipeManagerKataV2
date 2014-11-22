using System.Collections.Generic;

namespace RecipeManager
{
public class RecipeManager
{
    private IRecipeStore m_recipeStore;
    private IRecipeManagerUI m_recipeManagerUi;
    private List<Recipe> m_recipes; 

    public RecipeManager(IRecipeStore recipeStore, IRecipeManagerUI recipeManagerUI)
    {
        m_recipeManagerUi = recipeManagerUI;
        m_recipeStore = recipeStore;   
    }

    public List<Recipe> Recipes { get { return m_recipes; } }

    public void LoadRecipes()
    {
        m_recipes = m_recipeStore.Load();

        m_recipeManagerUi.PopulateList(m_recipes);
    }
}
}