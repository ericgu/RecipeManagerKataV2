using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeManager
{
    [TestClass]
    public class RecipeStoreSimulatorTests
    {
        [TestMethod()]
        public void when_I_call_Load_on_an_empty_RecipeStore__it_returns_an_empty_list()
        {
            RecipeStoreSimulator recipeStoreSimulator = new RecipeStoreSimulator();

            var recipes = recipeStoreSimulator.Load();

            Assert.AreEqual(0, recipes.Count);
        }

        [TestMethod()]
        public void when_I_save_a_single_recipe_and_call_load__it_returns_that_recipe()
        {
            RecipeStoreSimulator recipeStoreSimulator = new RecipeStoreSimulator();
            recipeStoreSimulator.Save("Grits", "Grind and cook");

            var recipes = recipeStoreSimulator.Load();

            Assert.AreEqual(1, recipes.Count);
            ValidateRecipe(recipes, 0, "Grits", "Grind and cook");
        }

        private static void ValidateRecipe(List<Recipe> recipes, int index, string name, string directions)
        {
            Assert.AreEqual(name, recipes.Skip(index).First().Name);
            Assert.AreEqual(directions, recipes.Skip(index).First().Text);
        }

        [TestMethod()]
        public void when_I_save_two_recipes_and_call_load__it_returns_both_recipes()
        {
            RecipeStoreSimulator recipeStoreSimulator = new RecipeStoreSimulator();
            recipeStoreSimulator.Save("Grits", "Grind and cook");
            recipeStoreSimulator.Save("Bacon", "Saute");

            var recipes = recipeStoreSimulator.Load();

            Assert.AreEqual(2, recipes.Count);
            ValidateRecipe(recipes, 0, "Grits", "Grind and cook");
            ValidateRecipe(recipes, 1, "Bacon", "Saute");
        }

        [TestMethod()]
        public void when_I_save_two_recipes_delete_one_and_call_load__it_returns_one_recipe()
        {
            RecipeStoreSimulator recipeStoreSimulator = new RecipeStoreSimulator();
            recipeStoreSimulator.Save("Grits", "Grind and cook");
            recipeStoreSimulator.Save("Bacon", "Saute");

            recipeStoreSimulator.Delete("Grits");

            var recipes = recipeStoreSimulator.Load();

            Assert.AreEqual(1, recipes.Count);
            ValidateRecipe(recipes, 0, "Bacon", "Saute");
        }
    }
}
