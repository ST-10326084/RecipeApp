using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class DisplayRecipePage : Page
    {
        private RecipeBook recipeBook;

        public DisplayRecipePage(RecipeBook book)
        {
            InitializeComponent();
            recipeBook = book;
            RecipeComboBox.ItemsSource = recipeBook.GetRecipes().Select(r => r.Name);
        }

        private void RecipeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedRecipeName = RecipeComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedRecipeName))
            {
                return;
            }

            var selectedRecipe = recipeBook.GetRecipes().FirstOrDefault(r => r.Name == selectedRecipeName);

            if (selectedRecipe != null)
            {
                IngredientsListBox.ItemsSource = selectedRecipe.Ingredients.Select(i => $"{i.Name} - {i.Quantity} {i.UnitOfMeasure}");
                StepsListBox.ItemsSource = selectedRecipe.Steps;
                TotalCaloriesTextBlock.Text = $"Total Calories: {selectedRecipe.CalculateTotalCalories()}";

                if (selectedRecipe.CalculateTotalCalories() > 300)
                {
                    MessageBox.Show($"Warning: Recipe '{selectedRecipe.Name}' exceeds 300 calories with a total of {selectedRecipe.CalculateTotalCalories()} calories.");
                }
            }
        }
    }
}
// working niw