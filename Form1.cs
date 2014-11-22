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
        private RecipeStore m_recipeStore;
        private RecipeStoreLocator m_recipeStoreLocator = new RecipeStoreLocator();
        private RecipeManagerUI m_recipeManagerUI;
        private RecipeManager m_recipeManager;

        public Form1()
        {
            InitializeComponent();

            m_recipeManagerUI = new RecipeManagerUI(listView1, buttonNew, buttonSave, textBoxName, textBoxObjectData);

            textBoxRecipeDirectory.Text = m_recipeStoreLocator.GetRecipeDirectory();
            m_recipeStore = new RecipeStore(m_recipeStoreLocator.GetRecipeDirectory());
            m_recipeManager = new RecipeManager(m_recipeStore, m_recipeManagerUI);
            m_recipeManager.LoadRecipes();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            foreach (RecipeListViewItem recipeListViewItem in listView1.SelectedItems)
            {
                m_recipeStore.Delete(recipeListViewItem.Recipe.Name);
            }
            m_recipeManagerUI.PopulateList(m_recipeManager.Recipes);

            m_recipeManager.New();
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
            m_recipeManager.LoadRecipes();
            m_recipeManager.New();
        }
    }
}
