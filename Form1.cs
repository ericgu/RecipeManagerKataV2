using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeManager
{
    public partial class Form1 : Form
    {
        private List<Recipe> m_recipes = new List<Recipe>();
        private RecipeStore m_recipeStore;
        private RecipeStoreLocator m_recipeStoreLocator = new RecipeStoreLocator();

        public Form1()
        {
            InitializeComponent();

            LoadRecipes();
            textBoxRecipeDirectory.Text = m_recipeStoreLocator.GetRecipeDirectory();
            m_recipeStore = new RecipeStore(m_recipeStoreLocator.GetRecipeDirectory());
        }

        private void LoadRecipes()
{
    string directory = m_recipeStoreLocator.GetRecipeDirectory();
    m_recipes = m_recipeStore.Load();

    PopulateList();
}

        private void PopulateList()
        {
            listView1.Items.Clear();

            foreach (Recipe recipe in m_recipes)
            {
                listView1.Items.Add(new RecipeListViewItem(recipe));
            }
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            foreach (RecipeListViewItem recipeListViewItem in listView1.SelectedItems)
            {
                m_recipes.Remove(recipeListViewItem.Recipe);
                m_recipeStore.Delete(recipeListViewItem.Recipe.Name);
            }
            PopulateList();

            NewClick(null, null);
        }

        private void NewClick(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxObjectData.Text = "";
        }

        private void SaveClick(object sender, EventArgs e)
        {
            m_recipeStore.Save(textBoxName.Text, textBoxObjectData.Text);
            LoadRecipes();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RecipeListViewItem recipeListViewItem in listView1.SelectedItems)
            {
                textBoxName.Text = recipeListViewItem.Recipe.Name;
                textBoxObjectData.Text = recipeListViewItem.Recipe.Text;
                break;
            }
        }

        private void buttonSaveRecipeDirectory_Click(object sender, EventArgs e)
        {
            m_recipeStoreLocator.SetRecipeDirectory(textBoxRecipeDirectory.Text);
            m_recipeStore = new RecipeStore(m_recipeStoreLocator.GetRecipeDirectory());
            LoadRecipes();
            NewClick(null, null);
        }
    }
}
