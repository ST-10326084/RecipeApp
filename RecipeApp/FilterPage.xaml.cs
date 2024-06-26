using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class FilterPage : Page
    {
        private RecipeBook recipeBook;
        private DisplayRecipePage displayRecipePage;

        public FilterPage(RecipeBook book, DisplayRecipePage displayPage)
        {
            InitializeComponent();
            recipeBook = book;
            displayRecipePage = displayPage;

            // Populate Ingredient Filter ComboBox
            PopulateIngredientFilterComboBox();
            PopulateFoodGroupFilterComboBox();
        }

        private void PopulateIngredientFilterComboBox()
        {
            // Get all unique ingredients from the RecipeBook
            var allIngredients = recipeBook.GetAllIngredients();
            var uniqueIngredients = allIngredients.GroupBy(i => i.Name).Select(g => g.First());

            // Populate ComboBox
            foreach (var ingredient in uniqueIngredients)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = ingredient.Name;
                IngredientFilterComboBox.Items.Add(item);
            }
        }

        private void PopulateFoodGroupFilterComboBox()
        {
            // Get all unique food groups from the RecipeBook
            var allFoodGroups = recipeBook.GetAllFoodGroups();

            // Populate ComboBox
            foreach (var foodGroup in allFoodGroups)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = foodGroup;
                FoodGroupFilterComboBox.Items.Add(item);
            }
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            var filteredRecipes = recipeBook.GetRecipes();

            // Filter by Ingredient
            if (IngredientFilterComboBox.SelectedItem is ComboBoxItem selectedIngredientItem && !string.IsNullOrEmpty(selectedIngredientItem.Content.ToString()))
            {
                var selectedIngredientName = selectedIngredientItem.Content.ToString();
                filteredRecipes = filteredRecipes
                    .Where(r => r.Ingredients.Any(i => i.Name.Equals(selectedIngredientName, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            // Filter by Food Group
            if (FoodGroupFilterComboBox.SelectedItem is ComboBoxItem selectedFoodGroupItem && !string.IsNullOrEmpty(selectedFoodGroupItem.Content.ToString()))
            {
                var selectedFoodGroup = selectedFoodGroupItem.Content.ToString();
                filteredRecipes = filteredRecipes
                    .Where(r => r.Ingredients.Any(i => i.FoodGroup.Equals(selectedFoodGroup, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            // Filter by Max Calories
            if (int.TryParse(MaxCaloriesFilterTextBox.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes
                    .Where(r => r.CalculateTotalCalories() <= maxCalories)
                    .ToList();
            }

            displayRecipePage.UpdateRecipeList(filteredRecipes);
            NavigationService.GoBack();
        }
    }
}
