using System.Collections.Generic;
using System.Windows.Forms;

namespace RecipeManager
{
public class RecipeManagerUI : IRecipeManagerUI
{
    private ListView m_listView;
    private TextBox m_textBoxName;
    private TextBox m_textBoxDirections;

    public RecipeManagerUI(ListView listView, Button newButton, Button saveButton, Button deleteButton, TextBox textBoxName, TextBox textBoxDirections)
    {
        m_listView = listView;
        newButton.Click += newButton_Click;
        saveButton.Click += saveButton_Click;
        listView.SelectedIndexChanged += listView_SelectedIndexChanged;
        deleteButton.Click += deleteButton_Click;
        m_textBoxName = textBoxName;
        m_textBoxDirections = textBoxDirections;
    }

    private void deleteButton_Click(object sender, System.EventArgs e)
    {
        if (DeleteClick != null)
        {
            foreach (RecipeListViewItem recipeListViewItem in m_listView.SelectedItems)
            {
                DeleteClick(recipeListViewItem.Recipe);
            }
        }
    }

    void listView_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (RecipeSelected != null)
        {
            foreach (RecipeListViewItem recipeListViewItem in m_listView.SelectedItems)
            {
                RecipeSelected(recipeListViewItem.Recipe);
                break;
            }
        }
    }

    void saveButton_Click(object sender, System.EventArgs e)
    {
        if (SaveClick != null)
        {
            SaveClick();
        }
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

    public event System.Action SaveClick;
    public event System.Action NewClick;
    public event System.Action<Recipe> RecipeSelected;
    public event System.Action<Recipe> DeleteClick;

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