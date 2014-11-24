using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeManager
{
    [TestClass]
    public class RecipeManagerTests
    {
[TestMethod()]
public void when_I_call_LoadRecipes_with_two_recipes_in_the_store__it_sends_them_to_the_UI_class()
{
    RecipeStoreSimulator recipeStore = new RecipeStoreSimulator();
    recipeStore.Save("Grits", "Stir");
    recipeStore.Save("Bacon", "Fry");

    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();

    RecipeManager recipeManager = new RecipeManager(recipeStore, null, recipeManagerUI);

    recipeManager.LoadRecipes();

    Assert.AreEqual(2, recipeManagerUI.SimulatorRecipes.Count);
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 0, "Grits", "Stir");
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 1, "Bacon", "Fry");
}

[TestMethod()]
public void when_I_call_Initialize_with_two_recipes_in_the_store__it_sends_them_to_the_UI_class()
{
    RecipeStoreSimulator recipeStore = new RecipeStoreSimulator();
    recipeStore.Save("Grits", "Stir");
    recipeStore.Save("Bacon", "Fry");

    RecipeStoreLocatorSimulator recipeStoreLocator = new RecipeStoreLocatorSimulator();
    recipeStoreLocator.SetRecipeDirectory("First");

    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();

    RecipeManager recipeManager = new RecipeManager(recipeStore, recipeStoreLocator, recipeManagerUI);

    recipeManager.Initialize();

    Assert.AreEqual(2, recipeManagerUI.SimulatorRecipes.Count);
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 0, "Grits", "Stir");
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 1, "Bacon", "Fry");

    Assert.AreEqual("First", recipeManagerUI.RecipeDirectory);
}

[TestMethod()]
public void when_I_click_on_new__it_clears_the_name_and_directions()
{
    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();

    RecipeManager recipeManager = new RecipeManager(null, null, recipeManagerUI);

    recipeManagerUI.RecipeName = "Grits";
    recipeManagerUI.RecipeDirections = "Stir";

    Assert.AreEqual("Grits", recipeManagerUI.RecipeName);
    Assert.AreEqual("Stir", recipeManagerUI.RecipeDirections);

    recipeManagerUI.SimulateNewClick();

    Assert.AreEqual("", recipeManagerUI.RecipeName);
    Assert.AreEqual("", recipeManagerUI.RecipeDirections);
}


[TestMethod()]
public void when_I_click_on_save__it_stores_the_recipe_to_the_store_and_updates_the_display()
{
    RecipeStoreSimulator recipeStore = new RecipeStoreSimulator();
    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();

    RecipeManager recipeManager = new RecipeManager(recipeStore, null, recipeManagerUI);

    recipeManagerUI.RecipeName = "Grits";
    recipeManagerUI.RecipeDirections = "Stir";

    recipeManagerUI.SimulateSaveClick();

    var recipes = recipeStore.Load();

    RecipeStoreSimulatorTests.ValidateRecipe(recipes, 0, "Grits", "Stir");

    recipes = recipeManagerUI.SimulatorRecipes;

    RecipeStoreSimulatorTests.ValidateRecipe(recipes, 0, "Grits", "Stir");
}

[TestMethod()]
public void when_I_select_a_recipe__it_sets_the_name_and_directions()
{
    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();

    RecipeManager recipeManager = new RecipeManager(null, null, recipeManagerUI);

    Recipe recipe = new Recipe {Name = "Grits", Text = "Stir"};

    recipeManagerUI.SimulateRecipeSelected(recipe);

    Assert.AreEqual("Grits", recipeManagerUI.RecipeName);
    Assert.AreEqual("Stir", recipeManagerUI.RecipeDirections);
}

[TestMethod()]
public void when_I_change_the_recipe_directory__it_should_change_the_store_directory_reload_recipes_and_clear_the_current_recipe()
{
    RecipeStoreSimulator recipeStore = new RecipeStoreSimulator();
    recipeStore.RecipeDirectory = "Second";
    recipeStore.Save("Milk", "Pour");
    
    recipeStore.RecipeDirectory = "First";
    recipeStore.Save("Grits", "Stir");
    recipeStore.Save("Bacon", "Fry");
    
    
    RecipeManagerUISimulator recipeManagerUI = new RecipeManagerUISimulator();
    RecipeStoreLocatorSimulator recipeStoreLocatorSimulator = new RecipeStoreLocatorSimulator();

    RecipeManager recipeManager = new RecipeManager(recipeStore, recipeStoreLocatorSimulator, recipeManagerUI);

    recipeManager.LoadRecipes();

    Assert.AreEqual(2, recipeManagerUI.SimulatorRecipes.Count);
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 0, "Grits", "Stir");
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 1, "Bacon", "Fry");

    recipeManagerUI.SimulateSaveRecipeDirectoryClick("Second");

    Assert.AreEqual("Second", recipeStore.RecipeDirectory);

    Assert.AreEqual(1, recipeManagerUI.SimulatorRecipes.Count);
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 0, "Milk", "Pour");

    Assert.AreEqual("", recipeManagerUI.RecipeName);
    Assert.AreEqual("", recipeManagerUI.RecipeDirections);
}
    }
}
