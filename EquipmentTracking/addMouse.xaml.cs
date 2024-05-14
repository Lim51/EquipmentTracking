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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class addMouse : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString; 
        public addMouse()
        {
            this.InitializeComponent();
        }

        // Event handler for the Clear button click event
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all input fields and reset the combo box
            modelTextbox.Text = "";
            codeTextbox.Text = "";
            dateTextbox.Text = ""; 
            remarkTextbox.Text = "";
            ownerNameTextbox.Text = "";
            conditionComboBox.SelectedIndex = -1; // Deselect any selected item in the combo box
        }


        // Event handler for the Save button click event
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if the model textbox is not empty
                if (!string.IsNullOrEmpty(modelTextbox.Text))
                {
                    // Establish a connection to the database
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        // Create a SQL command for inserting a new mouse record
                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO mouse (Model, Code_SN, Received_date, Condition, Remarks, Owner) VALUES (@Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner)";

                        // Add parameters to the SQL command
                        insertCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);

                        // Check if the date is in the correct format (yyyy-MM-dd)
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
                            // If the date is empty, set it to null in the database
                            insertCommand.Parameters.AddWithValue("@Received_date", DBNull.Value);
                        }
                        else
                        {
                            DateTime receivedDate;
                            // Try to parse the date from the input
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
                        // Add the remaining parameters
                        insertCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        insertCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);
                        // Execute the SQL command
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

                    DisplayDialog("Insert", "New record inserted successfully.");
                }
                else
                {
                    DisplayDialog("Input Error", "Enter Model Name.");
                    modelTextbox.Focus(FocusState.Programmatic);
                }
            }
            catch (Exception theException)
            {
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

