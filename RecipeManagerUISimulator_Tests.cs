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
    public class RecipeManagerUISimulatorTests
    {
        [TestMethod()]
        public void when_I_call_PopulateList__it_stores_the_recipes()
        {
            var recipeManagerUISimulator = new RecipeManagerUISimulator();

            var recipes = new List<Recipe>();
            recipes.Add(new Recipe {Name = "A", Text = "B"});

            recipeManagerUISimulator.PopulateList(recipes);

            RecipeStoreSimulatorTests.ValidateRecipe(recipeManagerUISimulator.SimulatorRecipes, 0, "A", "B");
        }
    }
}
