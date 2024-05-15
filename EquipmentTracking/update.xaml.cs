using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EquipmentTracking
{
    /// <summary>
    /// Represents the page for updating equipment details.
    /// </summary>
    public sealed partial class update : Page
    {
        // Initializes a new instance of the update class.
        public update()
        {
            this.InitializeComponent();
            LoadCategories();
        }
        // Loads equipment categories into the ComboBox.
        private void LoadCategories()
        {
            // Assuming you have a list of categories, you need to populate them in the ListView
            categoryComboBox.ItemsSource = new[]
            {
                new { Name = "Mouse", Page = typeof(updateMouse) },
                new { Name = "Headphone", Page = typeof(updateHeadphone) },
                new { Name = "Monitor", Page = typeof(updateMonitor) },
                new { Name = "Docking System", Page = typeof(updateDockingSystem) },
                new { Name = "Cables", Page = typeof(updateCables) },
                new { Name = "Laptop", Page = typeof(updateLaptop) }
            };
        }

        // Handles the selection change in the category ComboBox.
        // Navigates to the corresponding page based on the selected category.
        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When a category is selected, navigate to the corresponding page
            if (categoryComboBox.SelectedItem != null)
            {
                var selectedCategory = categoryComboBox.SelectedItem as dynamic;
                var pageType = selectedCategory.Page;
                categoryFrame.Navigate(pageType);
            }
        }

        // Handles the click event of the Back button.
        // Navigates to the main page.
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        // Handles the click event of the exit command bar button.
        // Exits the application.
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

    }


}
