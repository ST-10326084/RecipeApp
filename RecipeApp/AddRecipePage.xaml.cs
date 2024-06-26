using System.Collections.Generic;
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
            string ingredientName = IngredientNameTextBox.Text.Trim();
            double quantity = double.Parse(QuantityTextBox.Text.Trim());
            string unitOfMeasure = UnitOfMeasureTextBox.Text.Trim();
            double calories = double.Parse(CaloriesTextBox.Text.Trim());
            string foodGroup = ((ComboBoxItem)FoodGroupsComboBox.SelectedItem)?.Content.ToString();

            // Create new Ingredient object and add to ListBox
            Ingredient ingredient = new Ingredient(ingredientName, quantity, unitOfMeasure, calories, foodGroup);
            newRecipe.AddIngredient(ingredient);
            IngredientsListBox.Items.Add($"{ingredientName} - {quantity} {unitOfMeasure} - {calories} - {foodGroup}");

            // Clear input fields
            IngredientNameTextBox.Clear();
            QuantityTextBox.Clear();
            UnitOfMeasureTextBox.Clear();
            CaloriesTextBox.Clear();
            FoodGroupsComboBox.SelectedIndex = -1;
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            string step = StepTextBox.Text.Trim();
            StepsListBox.Items.Add(step);
            StepTextBox.Clear();
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e) // needs to handle if something was left blank
        {
            newRecipe.Name = RecipeNameTextBox.Text;

            // Add steps to recipe
            foreach (string step in StepsListBox.Items)
            {
                newRecipe.Steps.Add(step);
            }

            // Add newRecipe to recipeBook
            recipeBook.GetRecipes().Add(newRecipe);

            MessageBox.Show("Recipe added successfully!");

            
        }
    }
}
