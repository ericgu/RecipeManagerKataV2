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
    public class RecipeStoreLocatorTests
    {
        private static IRecipeStoreLocator CreateRecipeStoreLocator()
        {
            //return new RecipeStoreLocator();
            return new RecipeStoreLocatorSimulator();
        }

        [TestMethod()]
        public void when_I_call_GetDirectory_in_the_default_case__it_returns_the_default_value()
        {
            var recipeStoreLocator = CreateRecipeStoreLocator();

            var directory = recipeStoreLocator.GetRecipeDirectory();

            VerifyDirectoryIsTheDefault(directory);
        }

        private static void VerifyDirectoryIsTheDefault(string directory)
        {
            Assert.AreEqual(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"RecipeMaker\RecipeDirectory"),
                directory);
        }

        [TestMethod()]
        public void when_I_save_a_directory_and_then_call_GetDirectory__it_returns_the_saved_value()
        {
            var recipeStoreLocator = CreateRecipeStoreLocator();

            recipeStoreLocator.SetRecipeDirectory(@"c:\recipes");
            var directory = recipeStoreLocator.GetRecipeDirectory();

            Assert.AreEqual(@"c:\recipes", directory);

            recipeStoreLocator.ResetToDefault();
        }

        [TestMethod()]
        public void when_I_save_a_directory_and_call_reset_and_then_call_GetDirectory__it_returns_the_saved_value()
        {
            var recipeStoreLocator = CreateRecipeStoreLocator();

            recipeStoreLocator.SetRecipeDirectory(@"c:\recipes");
            recipeStoreLocator.ResetToDefault();
            var directory = recipeStoreLocator.GetRecipeDirectory();

            VerifyDirectoryIsTheDefault(directory);
        }
    }
}
