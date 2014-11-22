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

    RecipeManager recipeManager = new RecipeManager(recipeStore, new RecipeManagerUISimulator());

    recipeManager.LoadRecipes();

    Assert.AreEqual(2, recipeManagerUI.SimulatorRecipes.Count);
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 0, "Grits", "Stir");
    RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUI.SimulatorRecipes, 1, "Bacon", "Fry");
}
    }
}
