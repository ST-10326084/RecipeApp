using System.Windows;

namespace RecipeApp
{
    public partial class AddRecipeWindow : Window
    {
        private RecipeBook recipeBook;
        private Recipe newRecipe;

        public AddRecipeWindow(RecipeBook book)
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
            this.Close();
        }
    }
}
