using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecipeBook recipeBook;

        public MainWindow()
        {
            InitializeComponent();
            recipeBook = new RecipeBook();
            recipeBook.OnHighCalories += message => MessageBox.Show(message);
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            var addRecipeWindow = new AddRecipeWindow(recipeBook);
            addRecipeWindow.Show();
        }

        private void DisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            var displayRecipeWindow = new DisplayRecipeWindow(recipeBook);
            displayRecipeWindow.Show();
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            var scaleRecipeWindow = new ScaleRecipeWindow(recipeBook);
            scaleRecipeWindow.Show();
        }

        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            var clearRecipeWindow = new ClearRecipeWindow(recipeBook);
            clearRecipeWindow.Show();
        }
    }
}