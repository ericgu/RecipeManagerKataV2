using System.Collections.Generic;

namespace RecipeManager
{
public interface IRecipeStore
{
    List<Recipe> Load();
    void Delete(string name);
    void Save(string name, string directions);
}
}