using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
class RecipeStoreSimulator: IRecipeStore
{
    private Dictionary<string, List<Recipe>> m_recipes = new Dictionary<string, List<Recipe>>();
    private string m_recipeDirectory = String.Empty;

    public RecipeStoreSimulator()
    {
        RecipeDirectory = String.Empty;
    }
    private List<Recipe> CurrentRecipes
    {
        get { return m_recipes[m_recipeDirectory]; }
    }

    public List<Recipe> Load()
    {
        return CurrentRecipes;
    }

    public void Delete(string name)
    {
        m_recipes[m_recipeDirectory] = m_recipes[m_recipeDirectory].Where(recipe => recipe.Name != name).ToList();
    }

    public void Save(string name, string directions)
    {
        CurrentRecipes.Add(new Recipe { Name = name, Text = directions });
    }

    public string RecipeDirectory
    {
        get
        {
            return m_recipeDirectory;
        }
        set
        {
            m_recipeDirectory = value;
            if (!m_recipes.ContainsKey(m_recipeDirectory))
            {
                m_recipes.Add(m_recipeDirectory, new List<Recipe>());
            }
        }
    }
}
}
