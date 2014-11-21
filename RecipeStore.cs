using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecipeManager
{
public class RecipeStore
{
    public List<Recipe> Load(string directory)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(directory);
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

    public void Delete(string directory, string name)
    {
        File.Delete(directory + @"\" + name);
    }

    public void Save(string directory, string name, string directions)
    {
        File.WriteAllText(Path.Combine(directory, name), directions);
    }
}
}