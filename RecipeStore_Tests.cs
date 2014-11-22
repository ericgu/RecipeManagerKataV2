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
        private static IRecipeStore CreateRecipeStore()
        {
            //return new RecipeStoreSimulator();
            return new RecipeStore(@"e:\temp");
        }

        private static void ClearStore(IRecipeStore recipeStore)
        {
            foreach (Recipe recipe in recipeStore.Load())
            {
                recipeStore.Delete(recipe.Name);
            }
        }

        public static void ValidateRecipe(List<Recipe> recipes, int index, string name, string directions)
        {
            Assert.AreEqual(name, recipes.Skip(index).First().Name);
            Assert.AreEqual(directions, recipes.Skip(index).First().Text);
        }

        [TestMethod()]
        public void when_I_call_Load_on_an_empty_RecipeStore__it_returns_an_empty_list()
        {
            var recipeStore = CreateRecipeStore();

            var recipes = recipeStore.Load();

            Assert.AreEqual(0, recipes.Count);

            ClearStore(recipeStore);
        }


        [TestMethod()]
        public void when_I_save_a_single_recipe_and_call_load__it_returns_that_recipe()
        {
            var recipeStore = CreateRecipeStore();

            recipeStore.Save("Grits", "Grind and cook");

            var recipes = recipeStore.Load();

            Assert.AreEqual(1, recipes.Count);
            ValidateRecipe(recipes, 0, "Grits", "Grind and cook");

            ClearStore(recipeStore);
        }


        [TestMethod()]
        public void when_I_save_two_recipes_and_call_load__it_returns_both_recipes()
        {
            var recipeStore = CreateRecipeStore();
            recipeStore.Save("Grits", "Grind and cook");
            recipeStore.Save("Bacon", "Saute");

            var recipes = recipeStore.Load();

            Assert.AreEqual(2, recipes.Count);
            ValidateRecipe(recipes, 0, "Bacon", "Saute");
            ValidateRecipe(recipes, 1, "Grits", "Grind and cook");

            ClearStore(recipeStore);
        }

        [TestMethod()]
        public void when_I_save_two_recipes_delete_one_and_call_load__it_returns_one_recipe()
        {
            var recipeStore = CreateRecipeStore();
            recipeStore.Save("Grits", "Grind and cook");
            recipeStore.Save("Bacon", "Saute");

            recipeStore.Delete("Grits");

            var recipes = recipeStore.Load();

            Assert.AreEqual(1, recipes.Count);
            ValidateRecipe(recipes, 0, "Bacon", "Saute");

            ClearStore(recipeStore);
        }
    }
}
