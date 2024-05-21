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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EquipmentTracking
{
    /// <summary>
    /// Represents the main page of the application.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the MainPage class.
        /// </summary>
        
        public MainPage()
        {
            this.InitializeComponent();
            UpdateWelcomeMessage();
        }

        /// <summary>
        /// Event handler for the "View Equipment" button click.
        /// Navigates to the view equipment page.
        /// </summary>
        private void viewEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(view));
        }

        /// <summary>
        /// Event handler for the "Exit" command bar button click.
        /// Exits the application.
        /// </summary>
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        /// <summary>
        /// Event handler for the "Update Equipment" button click.
        /// Navigates to the update equipment page.
        /// </summary>
        private void updateEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(update));
        }

        /// <summary>
        /// Event handler for the "History" button click.
        /// Navigates to the history page.
        /// </summary>
        private void history_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(history));
        }

        /// <summary>
        /// Event handler for the "Add Equipment" button click.
        /// Navigates to the add equipment page.
        /// </summary>
        private void addEquipment_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(add));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateWelcomeMessage();
        }

        private void UpdateWelcomeMessage()
        {
            if (!string.IsNullOrEmpty(GlobalData.CurrentUser))
            {
                welcomeTextBlock.Text = $"Login as {GlobalData.CurrentUser}";
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }


    }
}
