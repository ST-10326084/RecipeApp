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

            // Populate Ingredient Name and Food Group ComboBoxes
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            var allIngredients = recipeBook.GetAllIngredients();
            var uniqueIngredients = new HashSet<string>(allIngredients.Select(i => i.Name));
            var allFoodGroups = recipeBook.GetAllFoodGroups();

            foreach (var ingredient in uniqueIngredients)
            {
                IngredientNameComboBox.Items.Add(ingredient);
            }

            foreach (var foodGroup in allFoodGroups)
            {
                FoodGroupsComboBox.Items.Add(foodGroup);
            }
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            string ingredientName = IngredientNameComboBox.Text.Trim();
            double quantity = double.Parse(QuantityTextBox.Text.Trim());
            string unitOfMeasure = UnitOfMeasureTextBox.Text.Trim();
            double calories = double.Parse(CaloriesTextBox.Text.Trim());
            string foodGroup = FoodGroupsComboBox.SelectedItem?.ToString();

            // Create new Ingredient object and add to ListBox
            Ingredient ingredient = new Ingredient(ingredientName, quantity, unitOfMeasure, calories, foodGroup);
            newRecipe.AddIngredient(ingredient);
            IngredientsListBox.Items.Add($"{ingredientName} - {quantity} {unitOfMeasure} - {calories} - {foodGroup}");

            // Clear input fields
            IngredientNameComboBox.SelectedIndex = -1;
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

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            newRecipe.Name = RecipeNameTextBox.Text;

            foreach (string step in StepsListBox.Items)
            {
                newRecipe.Steps.Add(step);
            }

            recipeBook.GetRecipes().Add(newRecipe);
            MessageBox.Show("Recipe added successfully!");
        }
    }
}
