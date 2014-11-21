using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
class RecipeStoreSimulator: IRecipeStore
{
    private List<Recipe> m_recipes = new List<Recipe>();

    public List<Recipe> Load()
    {
        return m_recipes;
    }

    public void Delete(string name)
    {
        m_recipes = m_recipes.Where(recipe => recipe.Name != name).ToList();
    }

    public void Save(string name, string directions)
    {
        m_recipes.Add(new Recipe {Name = name, Text = directions});
    }
}
}
