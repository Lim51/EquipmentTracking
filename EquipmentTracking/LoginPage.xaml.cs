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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            // Perform login action
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate username and password (You would typically do this against a database or some authentication service)
            if (username == "example" && password == "password")
            {
                // Successful login, navigate to another page or perform other actions
                ErrorMessageTextBlock.Text = "Login successful!";
            }
            else
            {
                // Display error message
                ErrorMessageTextBlock.Text = "Invalid username or password!";
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            // Toggle visibility of sign-up fields
            SignUpEmailTextBox.Visibility = SignUpEmailTextBox.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            SignUpConfirmPasswordBox.Visibility = SignUpConfirmPasswordBox.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            // Change text of the switch button
            SwitchButton.Content = SignUpEmailTextBox.Visibility == Visibility.Visible ? "Switch to Login" : "Switch to Sign Up";
        }
    }
}

