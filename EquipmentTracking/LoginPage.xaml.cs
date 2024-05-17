using System;
using System.Data.SqlClient;
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
        private string conn = (App.Current as App).ConnectionString;
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            // Perform login action
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    await connection.OpenAsync();

                    string query = "SELECT COUNT(1) FROM Users WHERE username = @Username AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);

                        var result = await cmd.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);

                        if (count > 0)
                        {
                            ErrorMessageTextBlock.Text = "Login successful!";
                            this.Frame.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            ErrorMessageTextBlock.Text = "Invalid username or password!";
                            ErrorMessageTextBlock.Visibility = Visibility.Visible;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessageTextBlock.Text = "An error occurred: " + ex.Message;
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                }
            }
        }

        //Register/Sign Up
        private async void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignUpConfirmPasswordBox.Visibility == Visibility.Collapsed)
            {
                // If the confirm password field is not visible, it means we are currently in login mode
                // Change the content of the action button to "Sign Up"
                ActionButton.Content = "Sign Up / Register";

                // Change the header to "Sign Up"
                PageTitleTextBlock.Text = "Sign Up / Register";

                // Show the confirm password field
                SignUpConfirmPasswordBox.Visibility = Visibility.Visible;

                // Hide the "Invalid username or password!" message
                ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                // If the confirm password field is visible, it means we are currently in sign-up mode
                // Perform sign-up action
                string username = UsernameTextBox.Text;
                string password = PasswordBox.Password;
                string confirmPassword = SignUpConfirmPasswordBox.Password;

                // Validate input (you can add more validation logic as needed)
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    ErrorMessageTextBlock.Text = "Please fill in all fields.";
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    return;
                }
                else if (password != confirmPassword)
                {
                    ErrorMessageTextBlock.Text = "Passwords do not match.";
                    ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    return;
                }

                // Insert the new user into the database
                string query = "INSERT INTO Users (username, password) VALUES (@Username, @Password)";
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", password);

                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            if (rowsAffected > 0)
                            {
                                ErrorMessageTextBlock.Text = "Sign up successful!";
                                ErrorMessageTextBlock.Visibility = Visibility.Visible;

                                // Clear input fields after successful sign-up
                                UsernameTextBox.Text = "";
                                PasswordBox.Password = "";
                                SignUpConfirmPasswordBox.Password = "";

                                // Automatically switch back to the login mode after sign-up
                                ActionButton.Content = "Login";
                                PageTitleTextBlock.Text = "Login";
                                SignUpConfirmPasswordBox.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                ErrorMessageTextBlock.Text = "Failed to sign up. Please try again.";
                                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMessageTextBlock.Text = "An error occurred: " + ex.Message;
                        ErrorMessageTextBlock.Visibility = Visibility.Visible;
                    }
                }
            }

            // Change text of the switch button
            SwitchButton.Content = SignUpConfirmPasswordBox.Visibility == Visibility.Visible ? "Switch to Login" : "Sign Up / Register";
        }



    /*
    // Validate username and password (You would typically do this against a database or some authentication service)
    if (username == "example" && password == "password")
    {
        // Successful login, navigate to Main Page or perform other actions
        ErrorMessageTextBlock.Text = "Login successful!";
        this.Frame.Navigate(typeof(MainPage));
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
        if (SignUpConfirmPasswordBox.Visibility == Visibility.Collapsed)
        {
            // If the confirm password field is not visible, it means we are currently in login mode
            // Change the content of the action button to "Sign Up"
            ActionButton.Content = "Sign Up / Register";

            // Change the header to "Sign Up"
            PageTitleTextBlock.Text = "Sign Up / Register";

            // Show the confirm password field
            SignUpConfirmPasswordBox.Visibility = Visibility.Visible;

            // Hide the "Invalid username or password!" message
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }
        else
        {
            // If the confirm password field is visible, it means we are currently in sign-up mode
            // Change the content of the action button back to "Login"
            ActionButton.Content = "Login";

            // Change the header back to "Login"
            PageTitleTextBlock.Text = "Login";

            // Hide the confirm password field
            SignUpConfirmPasswordBox.Visibility = Visibility.Collapsed;
        }

        // Change text of the switch button
        SwitchButton.Content = SignUpConfirmPasswordBox.Visibility == Visibility.Visible ? "Switch to Login" : "Sign Up / Register";

    }
    */


}
}

