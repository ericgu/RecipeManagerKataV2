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
    private string m_recipeLocation = String.Empty;

    public RecipeStoreSimulator()
    {
        RecipeLocation = String.Empty;
    }
    private List<Recipe> CurrentRecipes
    {
        get { return m_recipes[m_recipeLocation]; }
    }

    public List<Recipe> Load()
    {
        return CurrentRecipes;
    }

    public void Delete(string name)
    {
        m_recipes[m_recipeLocation] = m_recipes[m_recipeLocation].Where(recipe => recipe.Name != name).ToList();
    }

    public void Save(string name, string directions)
    {
        CurrentRecipes.Add(new Recipe { Name = name, Directions = directions, Size = directions.Length });
    }

    public string RecipeLocation
    {
        get
        {
            return m_recipeLocation;
        }
        set
        {
            m_recipeLocation = value;
            if (!m_recipes.ContainsKey(m_recipeLocation))
            {
                m_recipes.Add(m_recipeLocation, new List<Recipe>());
            }
        }
    }
}
}
