using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecipeManager
{
public class RecipeStore
{
    private string m_directory;

    public RecipeStore(string directory)
    {
        m_directory = directory;
    }

    public List<Recipe> Load()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(m_directory);
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
        File.Delete(m_directory + @"\" + name);
    }

    public void Save(string name, string directions)
    {
        File.WriteAllText(Path.Combine(m_directory, name), directions);
    }
}
}