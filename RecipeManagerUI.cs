using System.Collections.Generic;
using System.Windows.Forms;

namespace RecipeManager
{
public class RecipeManagerUI : IRecipeManagerUI
{
    private ListView m_listView;

    public RecipeManagerUI(ListView listView)
    {
        m_listView = listView;
    }

    public void PopulateList(List<Recipe> recipes)
    {
        m_listView.Items.Clear();

        foreach (Recipe recipe in recipes)
        {
            m_listView.Items.Add(new RecipeListViewItem(recipe));
        }
    }
}
}