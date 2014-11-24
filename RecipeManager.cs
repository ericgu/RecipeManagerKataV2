using System;
using System.Collections.Generic;

namespace RecipeManager
{
public class RecipeManager
{
    private IRecipeStore m_recipeStore;
    private IRecipeStoreLocator m_recipeStoreLocator;
    private IRecipeManagerUI m_recipeManagerUi;
    private List<Recipe> m_recipes; 

    public RecipeManager(IRecipeStore recipeStore, IRecipeStoreLocator recipeStoreLocator, IRecipeManagerUI recipeManagerUI)
    {
        m_recipeManagerUi = recipeManagerUI;
        m_recipeStore = recipeStore;
        m_recipeStoreLocator = recipeStoreLocator;

        m_recipeManagerUi.NewClick += New;
        m_recipeManagerUi.SaveClick += Save;
        m_recipeManagerUi.RecipeSelected += RecipeSelected;
        m_recipeManagerUi.DeleteClick += DeleteClick;
        m_recipeManagerUi.SaveRecipeDirectoryClick += SaveRecipeDirectoryClick;
    }

    public void Initialize()
    {

        m_recipeManagerUi.RecipeDirectory = m_recipeStoreLocator.GetRecipeDirectory();
        LoadRecipes();
    }

    void SaveRecipeDirectoryClick(string recipeDirectory)
    {
        m_recipeStoreLocator.SetRecipeDirectory(recipeDirectory);
        m_recipeStore.RecipeDirectory = m_recipeStoreLocator.GetRecipeDirectory();

        LoadRecipes();
        New();
    }

    void DeleteClick(Recipe recipe)
    {
        m_recipeStore.Delete(recipe.Name);
        LoadRecipes();

        New();
    }

    void RecipeSelected(Recipe recipe)
    {
        m_recipeManagerUi.RecipeName = recipe.Name;
        m_recipeManagerUi.RecipeDirections = recipe.Text;
    }

    public List<Recipe> Recipes { get { return m_recipes; } }

    public void LoadRecipes()
    {
        m_recipes = m_recipeStore.Load();

        m_recipeManagerUi.PopulateList(m_recipes);
    }

    public void New()
    {
        m_recipeManagerUi.RecipeName = "";
        m_recipeManagerUi.RecipeDirections = "";
    }

    public void Save()
    {
        m_recipeStore.Save(m_recipeManagerUi.RecipeName, m_recipeManagerUi.RecipeDirections);
        LoadRecipes();
    }
}
}