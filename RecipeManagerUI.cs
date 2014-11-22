using System.Collections.Generic;
using System.Windows.Forms;

namespace RecipeManager
{
public class RecipeManagerUI : IRecipeManagerUI
{
    private ListView m_listView;
    private TextBox m_textBoxName;
    private TextBox m_textBoxDirections;

    public RecipeManagerUI(ListView listView, Button newButton, TextBox textBoxName, TextBox textBoxDirections)
    {
        m_listView = listView;
        newButton.Click += newButton_Click;
        m_textBoxName = textBoxName;
        m_textBoxDirections = textBoxDirections;
    }

    void newButton_Click(object sender, System.EventArgs e)
    {
        if (NewClick != null)
        {
            NewClick();
        }
    }

    public void PopulateList(List<Recipe> recipes)
    {
        m_listView.Items.Clear();

        foreach (Recipe recipe in recipes)
        {
            m_listView.Items.Add(new RecipeListViewItem(recipe));
        }
    }

    public event System.Action NewClick;

    public string RecipeName
    {
        get
        {
            return m_textBoxName.Text;
        }
        set
        {
            m_textBoxName.Text = value;
        }
    }

    public string RecipeDirections
    {
        get { return m_textBoxDirections.Text; }
        set { m_textBoxDirections.Text = value; }
    }
}
}