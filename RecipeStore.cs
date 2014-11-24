using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecipeManager
{
public class RecipeStore : IRecipeStore
{
    private string m_recipeDirectory;

    public RecipeStore(string directory)
    {
        m_recipeDirectory = directory;
    }

    public List<Recipe> Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(m_recipeDirectory);
        directoryInfo.Create();

        return directoryInfo.GetFiles("*")
            .Select(
                fileInfo =>
                    new Recipe
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Length,
                        Text = File.ReadAllText(fileInfo.FullName)
                    })
            .ToList();
    }

    public void Delete(string name)
    {
        File.Delete(m_recipeDirectory + @"\" + name);
    }

    public void Save(string name, string directions)
    {
        File.WriteAllText(Path.Combine(m_recipeDirectory, name), directions);
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
        }
    }
}
}