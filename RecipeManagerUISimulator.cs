using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    class RecipeManagerUISimulator: IRecipeManagerUI
    {
        private List<Recipe> m_recipes;

        public List<Recipe> SimulatorRecipes {
            get { return m_recipes; }
        } 

        public void PopulateList(List<Recipe> recipes)
        {
            m_recipes = recipes;
        }

        public event Action NewClick;
        public event Action SaveClick;
        public event Action<Recipe> RecipeSelected;
        public event Action<Recipe> DeleteClick;
        public event Action<string> SaveRecipeDirectoryClick;


        public string RecipeName { get; set; }

        public string RecipeDirections { get; set; }

        public string RecipeDirectory { get; set;  }

        public void SimulateNewClick()
        {
            if (NewClick != null)
            {
                NewClick();
            }
        }

        internal void SimulateSaveClick()
        {
            if (SaveClick != null)
            {
                SaveClick();
            }
        }

        internal void SimulateRecipeSelected(Recipe recipe)
        {
            if (RecipeSelected != null)
            {
                RecipeSelected(recipe);
            }
        }

        internal void SimulateDeleteClick(Recipe recipe)
        {
            if (DeleteClick != null)
            {
                DeleteClick(recipe);
            }
        }

        internal void SimulateSaveRecipeDirectoryClick(string directory)
        {
            if (SaveRecipeDirectoryClick != null)
            {
                SaveRecipeDirectoryClick(directory);
            }
        }
    }
}
