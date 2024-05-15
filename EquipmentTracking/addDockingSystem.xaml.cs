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
    /// Page for adding details of a docking system.
    /// </summary>
    public sealed partial class addDockingSystem : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        // Constructor
        public addDockingSystem()
        {
            this.InitializeComponent();
        }

        // Clear button click event handler
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear input fields
            modelTextbox.Text = "";
            codeTextbox.Text = "";
            dateTextbox.Text = "";
            remarkTextbox.Text = "";
            ownerNameTextbox.Text = "";
            conditionComboBox.SelectedIndex = -1; // Deselect any selected item in the combo box
        }

        // Save button click event handler
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if model name is provided
                if (!string.IsNullOrEmpty(modelTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO docking_sys (Model, Code_SN, Received_date, Condition, Remarks, Owner) VALUES (@Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner)";

                        // Set parameter values
                        insertCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);

                        // Check and format received date
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
                            // If the date is empty, set it to null in the database
                            insertCommand.Parameters.AddWithValue("@Received_date", DBNull.Value);
                        }
                        else
                        {
                            DateTime receivedDate;
                            if (DateTime.TryParseExact(dateTextbox.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out receivedDate))
                            {
                                insertCommand.Parameters.AddWithValue("@Received_date", receivedDate.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                // Handle invalid date format
                                DisplayDialog("Input Error", "Enter a valid date in the format yyyy-MM-dd.");
                                return;
                            }
                        }

                        // Set condition parameter value
                        insertCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");

                        // Set remarks and owner parameter values
                        insertCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);

                        // Execute the query
                        insertCommand.ExecuteNonQuery();

                        db.Close();
                    }

                    // Reset input fields after successful insertion
                    modelTextbox.Text = "";
                    codeTextbox.Text = "";
                    dateTextbox.Text = "";
                    conditionComboBox.Text = "";
                    remarkTextbox.Text = "";
                    ownerNameTextbox.Text = "";
                    modelTextbox.Focus(FocusState.Programmatic);

                    // Display success message
                    DisplayDialog("Insert", "New record inserted successfully.");
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
                // Display error message if an exception occurs
                DisplayDialog("Error", "Error: " + theException.Message);
            }
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



    }
}
