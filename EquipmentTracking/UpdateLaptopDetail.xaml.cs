using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Represents a page for updating laptop details.
    /// </summary>
    public sealed partial class UpdateLaptopDetail : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;
        public UpdateLaptopDetail()
        {
            this.InitializeComponent();
            // Retrieve the laptop item to update
            var itemToUpdate = GlobalData.LaptopDetailList.SingleOrDefault(r => r.LaptopID == GlobalData.LaptopID);
            if (itemToUpdate != null)
            {
                // Populate the UI fields with the existing data
                modelTextbox.Text = itemToUpdate.Model;
                codeTextbox.Text = itemToUpdate.Code_SN;
                dateTextbox.Text = itemToUpdate.Received_date;
                // Set the selected item in ComboBox for conditionTextbox
                SetConditionComboBox(itemToUpdate.Condition);
                remarkTextbox.Text = itemToUpdate.Remarks;
                ownerNameTextbox.Text = itemToUpdate.Owner;

            }
        }

        // Method to set the selected condition in ComboBox
        private void SetConditionComboBox(string condition)
        {
            if (condition == "Good")
            {
                conditionComboBox.SelectedIndex = 0; // Index 0 corresponds to "Good"
            }
            else if (condition == "Bad")
            {
                conditionComboBox.SelectedIndex = 1; // Index 1 corresponds to "Bad"
            }
        }

        // Event handler for navigating back to the updateLaptop page
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateLaptop));
        }

        // Event handler for exiting the application
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Event handler for saving updates to the laptop details
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(modelTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        SqlCommand updateCommand = new SqlCommand();
                        updateCommand.Connection = db;

                        //Use parameterized query to prevent SQL injection attacks
                        updateCommand.CommandText = "UPDATE laptop SET Model=@Model, Code_SN=@Code_SN, Received_date=@Received_date, Condition=@Condition, Remarks=@Remarks,Owner=@Owner WHERE LaptopID='" + GlobalData.LaptopID.ToString() + "'";

                        updateCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);
                        // Check if the date is in the correct format (yyyy)
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
                            // If the date is empty, set it to null in the database
                            updateCommand.Parameters.AddWithValue("@Received_date", DBNull.Value);
                        }
                        else
                        {
                            // Regular expression pattern to match yyyy format
                            string yearPattern = @"^\d{4}$";

                            if (System.Text.RegularExpressions.Regex.IsMatch(dateTextbox.Text, yearPattern))
                            {
                                // Date format is correct, set it to the database parameter
                                updateCommand.Parameters.AddWithValue("@Received_date", dateTextbox.Text);
                            }
                            else
                            {
                                // Handle invalid date format
                                DisplayDialog("Input Error", "Enter a valid year in the format yyyy.");
                                return;
                            }
                        }

                        // Save the selected condition from ComboBox
                        updateCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        updateCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);

                        // Execute the update command
                        updateCommand.ExecuteNonQuery();

                        db.Close();
                    }
                    // Set focus on the modelTextbox
                    modelTextbox.Focus(FocusState.Programmatic);
                    // Display success message
                    DisplayDialog("Update", "updated successfully.");
                    // Navigate back to the updateLaptop page
                    this.Frame.Navigate(typeof(updateLaptop));

                }
                else
                {
                    // Display error message if model name is not provided
                    DisplayDialog("Input Error", "Enter Model Name.");
                    modelTextbox.Focus(FocusState.Programmatic);
                }

            }

            catch (Exception theException)
            {
                // Handle any exceptions and display error message
                DisplayDialog("Error: ", "Error: " + theException.Message);
            }

        }
        // Displays a dialog with the provided title and content
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
    }
}
