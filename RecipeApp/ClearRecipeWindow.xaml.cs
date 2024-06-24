using System.Linq;
using System.Windows;
using RecipeApp;

namespace RecipeApp
{
    public partial class ClearRecipeWindow : Window
    {
        private RecipeBook recipeBook;

        public ClearRecipeWindow(RecipeBook book)
        {
            InitializeComponent();
            recipeBook = book;
            RecipeComboBox.ItemsSource = recipeBook.GetRecipes().Select(r => r.Name);
        }

        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            string selectedRecipeName = RecipeComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedRecipeName))
            {
                MessageBox.Show("Please select a recipe to clear.");
                return;
            }

            var recipeToDelete = recipeBook.GetRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

            if (recipeToDelete != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the recipe '{selectedRecipeName}'?", "Confirm Delete", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    recipeBook.GetRecipes().Remove(recipeToDelete);
                    MessageBox.Show($"Recipe '{selectedRecipeName}' cleared successfully.");
                    RecipeComboBox.ItemsSource = recipeBook.GetRecipes().Select(r => r.Name); // Refresh the list
                }
            }
        }
    }
}
