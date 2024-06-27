using Microsoft.VisualBasic;
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

        private void IngredientNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == "<New Ingredient>")
            {
                string newIngredient = PromptForNewIngredient("New Ingredient");
                if (!string.IsNullOrEmpty(newIngredient))
                {
                    // Add the new ingredient to ComboBox
                    IngredientNameComboBox.Items.Insert(IngredientNameComboBox.Items.Count - 1, newIngredient);
                    IngredientNameComboBox.SelectedItem = newIngredient;
                }
            }
        }

        private void FoodGroupsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == "<New Food Group>")
            {
                string newFoodGroup = PromptForNewIngredient("New Food Group");
                if (!string.IsNullOrEmpty(newFoodGroup))
                {
                    // Add the new food group to ComboBox
                    FoodGroupsComboBox.Items.Insert(FoodGroupsComboBox.Items.Count - 1, newFoodGroup);
                    FoodGroupsComboBox.SelectedItem = newFoodGroup;
                }
            }
        }

        private string PromptForNewIngredient(string itemToAdd)
        {
            string newItem = null;
            // Show a dialog or input box to prompt the user for a new ingredient or food group
            // Example using MessageBox, replace with your preferred UI for input
            newItem = Microsoft.VisualBasic.Interaction.InputBox($"Enter new {itemToAdd}:", $"{itemToAdd} Input", "");

            return newItem;
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ingredientName = IngredientNameComboBox.Text.Trim();
                if (string.IsNullOrEmpty(ingredientName))
                {
                    MessageBox.Show("Ingredient name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(QuantityTextBox.Text.Trim(), out double quantity))
                {
                    MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string unitOfMeasure = UnitOfMeasureTextBox.Text.Trim();
                if (string.IsNullOrEmpty(unitOfMeasure))
                {
                    MessageBox.Show("Unit of measure cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(CaloriesTextBox.Text.Trim(), out double calories))
                {
                    MessageBox.Show("Please enter a valid calorie value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string foodGroup = FoodGroupsComboBox.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(foodGroup))
                {
                    MessageBox.Show("Please select a food group.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                    
                // Create new Ingredient object and add to ListBox
                Ingredient ingredient = new Ingredient(ingredientName, quantity, unitOfMeasure, calories, foodGroup);
                newRecipe.AddIngredient(ingredient);
                IngredientsListBox.Items.Add($"{ingredientName} - {quantity} {unitOfMeasure} - {calories} - {foodGroup}");
                MessageBox.Show("Ingredient added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Prompt user to enter new Ingredient or Food Group
            string newEntry = Interaction.InputBox("Enter new Ingredient or Food Group:", "New Entry");

            if (!string.IsNullOrEmpty(newEntry))
            {
                // Determine if it's an ingredient or food group based on context
                if (IngredientNameComboBox.Items.Contains(newEntry))
                {
                    MessageBox.Show("Ingredient already exists.");
                }
                else if (FoodGroupsComboBox.Items.Contains(newEntry))
                {
                    MessageBox.Show("Food group already exists.");
                }
                else
                {
                    // Ask user where to add the new entry
                    bool addToIngredients = AskUserToAddToIngredients();

                    // Add to respective ComboBox based on user choice
                    if (addToIngredients)
                    {
                        IngredientNameComboBox.Items.Add(newEntry);
                        IngredientNameComboBox.SelectedItem = newEntry;
                    }
                    else
                    {
                        FoodGroupsComboBox.Items.Add(newEntry);
                        FoodGroupsComboBox.SelectedItem = newEntry;
                    }
                }
            }
        }

        private bool AskUserToAddToIngredients()
        {
            // Show dialog to ask user where to add the new entry
            var result = MessageBox.Show("Add to Ingredients?", "Choose Destination", MessageBoxButton.YesNo, MessageBoxImage.Question);

            return result == MessageBoxResult.Yes;
        }
    }
}
