using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Page for adding new equipment items.
    /// </summary>
    public sealed partial class add : Page
    {
        public add()
        {
            this.InitializeComponent();
            // Load categories in the categoryComboBox
            LoadCategories();
        }

        //Populates the categoryComboBox with available equipment categories.
        private void LoadCategories()
        {
            // Assuming you have a list of categories, you need to populate them in the ListView
            categoryComboBox.ItemsSource = new[]
            {
                new { Name = "Mouse", Page = typeof(addMouse) },
                new { Name = "Headphone", Page = typeof(addHeadphone) },
                new { Name = "Monitor", Page = typeof(addMonitor) },
                new { Name = "Docking System", Page = typeof(addDockingSystem) },
                new { Name = "Cables", Page = typeof(addCables) },
                new { Name = "Laptop", Page = typeof(addLaptop) }
            };
        }

        //Handles the selection changed event of the categoryComboBox.
        // Navigates to the corresponding page when a category is selected.
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

        //Handles the click event of the BackButton.
        /// Navigates to the MainPage.
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        // Handles the click event of the exitCommandBar.
        // Exits the application.
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
