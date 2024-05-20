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
using System.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EquipmentTracking
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgotPassword : Page
    {
        private string conn = (App.Current as App).ConnectionString;

        public ForgotPassword()
        {
            this.InitializeComponent();
        }


        // Handles the click event of the Back button.
        // Navigates to the main page.
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }

        // Handles the click event of the exit command bar button.
        // Exits the application.
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            // Get the user ID or username entered by the user
            string UserIDOrusername = UIDOrUsernameTextBox.Text;

            // Get the new password entered by the user
            string newPassword = NewPasswordBox.Password;

            // Update the password in the database
            using (SqlConnection connection = new SqlConnection(conn))
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "UPDATE Users SET password = @NewPassword WHERE userID = @UserID OR username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@NewPassword", newPassword);
                        cmd.Parameters.AddWithValue("@UserID", UserIDOrusername);
                        cmd.Parameters.AddWithValue("@Username", UserIDOrusername);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        if (rowsAffected > 0)
                        {
                            // Password reset successful
                            ErrorMessageTextBlock.Text = "Password reset successful. Your new password is: " + newPassword;
                            ErrorMessageTextBlock.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            // User not found
                            ErrorMessageTextBlock.Text = "User not found.";
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
}
