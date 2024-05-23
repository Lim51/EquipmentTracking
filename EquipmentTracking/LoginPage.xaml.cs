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
using System.Threading.Tasks;

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

        //Sign In/Register Account Event Handler
        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignUpConfirmPasswordBox.Visibility == Visibility.Collapsed)
            {
                // Switch to sign-up mode
                ActionButton.Content = "Sign Up / Register";
                PageTitleTextBlock.Text = "Sign Up / Register";
                SignUpConfirmPasswordBox.Visibility = Visibility.Visible;
                UsernameTextBox.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Switch to login mode
                ActionButton.Content = "Login";
                PageTitleTextBlock.Text = "Login";
                SignUpConfirmPasswordBox.Visibility = Visibility.Collapsed;
                UsernameTextBox.Visibility = Visibility.Collapsed;
            }

            // Change text of the switch button
            SwitchButton.Content = SignUpConfirmPasswordBox.Visibility == Visibility.Visible ? "Switch to Login" : "Sign Up / Register";
        }

        //Login Account Event Handler
        private async void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            string userID = UserIDTextBox.Text;
            string password = PasswordBox.Password;

            if (SignUpConfirmPasswordBox.Visibility == Visibility.Collapsed || UsernameTextBox.Visibility==Visibility.Collapsed)
            {
                // Perform login action
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    try
                    {
                        await connection.OpenAsync();
                        string query = "SELECT COUNT(1) FROM Users WHERE userID = @userID AND password = @Password";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            cmd.Parameters.AddWithValue("@Password", password);

                            var result = await cmd.ExecuteScalarAsync();
                            int count = Convert.ToInt32(result);

                            if (count > 0)
                            {
                                ErrorMessageTextBlock.Text = "Login successful!";
                                // Retrieve the username from the database query result
                                string username = await GetUsernameAsync(userID);
                                // Set the current user in GlobalData
                                GlobalData.CurrentUser = username;
                                // Navigate to MainPage
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
            else
            {
                // Perform sign-up action
                string confirmPassword = SignUpConfirmPasswordBox.Password;
                string username = UsernameTextBox.Text;

                // Validate input
                if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
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
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    try
                    {
                        await connection.OpenAsync();
                        string query = "INSERT INTO Users (userID, username, password) VALUES (@UserID,@Username, @Password)";
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.Parameters.AddWithValue("@Password", password);

                            int rowsAffected = await cmd.ExecuteNonQueryAsync();
                            if (rowsAffected > 0)
                            {
                                ErrorMessageTextBlock.Text = "Sign up successful!";
                                ErrorMessageTextBlock.Visibility = Visibility.Visible;

                                // Clear input fields after successful sign-up
                                UserIDTextBox.Text = "";
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
        }

        //retrieve the username from the database
        private async Task<string> GetUsernameAsync(string userID)
        {
            string username = null;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT username FROM Users WHERE userID = @userID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        username = await cmd.ExecuteScalarAsync() as string;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                    DisplayDialog("Error: ", "Error: " + ex.Message);
                }
            }
            return username;
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ForgotPassword));
        }

        // Method to display a dialog with a specified title and content
        private async void DisplayDialog(string title, string content)
        {
            ContentDialog noDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"

            };

            ContentDialogResult result = await noDialog.ShowAsync();


        }

        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

    }
}





