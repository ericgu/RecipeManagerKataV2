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
        private const string FirstDirectory = @"e:\temp\recipe1";
        private const string SecondDirectory = @"e:\temp\recipe2";

        private static IRecipeStore CreateRecipeStore()
        {
            //return new RecipeStoreSimulator();
            return new RecipeStore(FirstDirectory);
        }

        private static void ClearStore(IRecipeStore recipeStore)
        {
            foreach (Recipe recipe in recipeStore.Load())
            {
                recipeStore.Delete(recipe.Name);
            }
        }

        public static void ValidateRecipe(List<Recipe> recipes, int index, string name, string directions, int size)
        {
            Recipe recipe = recipes.Skip(index).First();

            Assert.AreEqual(name, recipe.Name);
            Assert.AreEqual(directions, recipe.Directions);
            Assert.AreEqual(size, recipe.Size);
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
            ValidateRecipe(recipes, 0, "Grits", "Grind and cook", 14);

            ClearStore(recipeStore);
        }


        [TestMethod()]
        public void when_I_save_two_recipes_and_call_load__it_returns_both_recipes()
        {
            var recipeStore = CreateRecipeStore();
            recipeStore.Save("Bacon", "Saute");
            recipeStore.Save("Grits", "Grind and cook");

            var recipes = recipeStore.Load();

            Assert.AreEqual(2, recipes.Count);
            ValidateRecipe(recipes, 0, "Bacon", "Saute", 5);
            ValidateRecipe(recipes, 1, "Grits", "Grind and cook", 14);

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
            ValidateRecipe(recipes, 0, "Bacon", "Saute", 5);

            ClearStore(recipeStore);
        }

        [TestMethod()]
        public void when_I_save_a_recipe_in_two_locations__it_returns_the_proper_recipe_for_each_location()
        {
            var recipeStore = CreateRecipeStore();

            recipeStore.RecipeLocation = FirstDirectory;
            recipeStore.Save("Grits", "Grind and cook");
            recipeStore.RecipeLocation = SecondDirectory;
            recipeStore.Save("Bacon", "Saute");

            recipeStore.RecipeLocation = FirstDirectory;
            var recipes = recipeStore.Load();

            Assert.AreEqual(1, recipes.Count);
            ValidateRecipe(recipes, 0, "Grits", "Grind and cook", 14);

            recipeStore.RecipeLocation = SecondDirectory;
            recipes = recipeStore.Load();

            ValidateRecipe(recipes, 0, "Bacon", "Saute", 5);

            ClearStore(recipeStore);
            recipeStore.RecipeLocation = FirstDirectory;
            ClearStore(recipeStore);
        }
    }
}
