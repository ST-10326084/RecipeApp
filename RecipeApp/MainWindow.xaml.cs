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
        // main menu buttons
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new AddRecipePage(recipeBook)); 
        }

        private void DisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new DisplayRecipePage(recipeBook));
        }

        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ClearRecipePage(recipeBook));
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new ScaleRecipePage(recipeBook));
        }
    }
}