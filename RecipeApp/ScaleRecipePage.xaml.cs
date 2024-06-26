﻿using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class ScaleRecipePage : Page
    {
        private RecipeBook recipeBook;

        public ScaleRecipePage(RecipeBook book)
        {
            InitializeComponent();
            recipeBook = book;
            RecipeComboBox.ItemsSource = recipeBook.GetRecipes().Select(r => r.Name);
        }
        // follows the button to scale on the scalerecipepage. uses my orignak logic for resetting to og values
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            string selectedRecipeName = RecipeComboBox.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedRecipeName))
            {
                MessageBox.Show("Please select a recipe to scale.");
                return;
            }

            var selectedRecipe = recipeBook.GetRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                if (!double.TryParse(ScaleFactorTextBox.Text, out double scaleFactor))
                {
                    MessageBox.Show("Invalid scale factor. Please enter a valid number.");
                    return;
                }

                if (scaleFactor == 1)
                {
                    selectedRecipe.ResetToOriginal();
                    MessageBox.Show($"Recipe '{selectedRecipe.Name}' has been reset to original values.");
                }
                else
                {
                    selectedRecipe.Scale(scaleFactor);
                    MessageBox.Show($"Recipe '{selectedRecipe.Name}' has been scaled by a factor of {scaleFactor}.");
                }
            }
        }
    }
}
