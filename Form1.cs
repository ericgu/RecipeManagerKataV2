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
    private RecipeManager m_recipeManager;

    public Form1()
    {
        InitializeComponent();

        var recipeManagerUI = new RecipeManagerUI(listView1, 
            buttonNew, 
            buttonSave, 
            buttonDelete, 
            buttonSaveRecipeDirectory, 
            textBoxName, 
            textBoxObjectData, 
            textBoxRecipeDirectory);

        var recipeStoreLocator = new RecipeStoreLocator();
        var recipeStore = new RecipeStore(recipeStoreLocator.GetRecipeLocation());
        m_recipeManager = new RecipeManager(recipeStore, recipeStoreLocator, recipeManagerUI);
        m_recipeManager.Initialize();
    }
}
}
