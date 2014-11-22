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

        public string RecipeName { get; set; }

        public string RecipeDirections { get; set; }

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
    }
}
