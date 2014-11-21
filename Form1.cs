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

        public Form1()
        {
            InitializeComponent();

            LoadRecipes();
            textBoxRecipeDirectory.Text = GetRecipeDirectory();
        }

        private string GetRecipeDirectory()
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "RecipeMaker");

            if (File.Exists(directory + @"\" + "RecipeDirectory.txt"))
            {
                directory = File.ReadAllText(directory + @"\" + "RecipeDirectory.txt");
            }
            else
            {
                directory += @"\RecipeDirectory";
            }

            return directory;
        }

        private void SetRecipeDirectory(string recipeDirectory)
        {
            string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "RecipeMaker");

            File.WriteAllText(directory + @"\" + "RecipeDirectory.txt", recipeDirectory);
        }

private void LoadRecipes()
{
    string directory = GetRecipeDirectory();
    m_recipes = LoadRecipesPort(directory);

    PopulateList();
}

private static List<Recipe> LoadRecipesPort(string directory)
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
                string directory = GetRecipeDirectory();

                DeleteRecipe(directory, recipeListViewItem.Recipe.Name);
            }
            PopulateList();

            NewClick(null, null);
        }

        private static void DeleteRecipe(string directory, string name)
        {
            File.Delete(directory + @"\" + name);
        }

        private void NewClick(object sender, EventArgs e)
        {
            textBoxName.Text = "";
            textBoxObjectData.Text = "";
        }

        private void SaveClick(object sender, EventArgs e)
        {
            string directory = GetRecipeDirectory();

            SaveRecipe(directory, textBoxName.Text, textBoxObjectData.Text);
            LoadRecipes();
        }

private static void SaveRecipe(string directory, string name, string directions)
{
    File.WriteAllText(Path.Combine(directory, name), directions);
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
            SetRecipeDirectory(textBoxRecipeDirectory.Text);
            LoadRecipes();
            NewClick(null, null);
        }
    }
}
