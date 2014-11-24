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
        private string m_location;

        public string GetRecipeLocation()
        {
            if (m_location != null)
            {
                return m_location;
            }

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"RecipeMaker\RecipeDirectory");
        }

        public void SetRecipeLocation(string recipeLocation)
        {
            m_location = recipeLocation;
        }

        public void ResetToDefault()
        {
            m_location = null;
        }
    }
}
