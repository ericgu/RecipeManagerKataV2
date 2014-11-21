using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace RecipeManager
{
    class RecipeStoreLocatorSimulator: IRecipeStoreLocator
    {
        private string m_directory;

        public string GetRecipeDirectory()
        {
            if (m_directory != null)
            {
                return m_directory;
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"RecipeMaker\RecipeDirectory");
        }

        public void SetRecipeDirectory(string recipeDirectory)
        {
            m_directory = recipeDirectory;
        }

        public void ResetToDefault()
        {
            m_directory = null;
        }
    }
}
