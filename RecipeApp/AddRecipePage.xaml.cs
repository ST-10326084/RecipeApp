using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class AddRecipePage : Page
    {
        private RecipeBook recipeBook;
        private Recipe newRecipe;

        public AddRecipePage(RecipeBook book)
        {
            InitializeComponent();
            recipeBook = book;
            newRecipe = new Recipe(string.Empty);
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add an ingredient
            // You might want to show another dialog to enter ingredient details
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            // Logic to add a step
            // You might want to show another dialog to enter step details
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            newRecipe.Name = RecipeNameTextBox.Text;
            // Add logic to save ingredients and steps
            recipeBook.GetRecipes().Add(newRecipe);
            MessageBox.Show("Recipe added successfully!");

            // Navigate back to the previous page or another page
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack(); // Navigate back to the previous page
            }
            else
            {
                // Handle scenario when there is no page to navigate back to
                // For example, navigate to a default page or close the application
            }
        }

    }
}
