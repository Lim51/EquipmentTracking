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
    /// Represents a page for updating mouse details.
    /// </summary>
    public sealed partial class UpdateMouseDetail : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        // Constructor to initialize the page and load the selected mouse details
        public UpdateMouseDetail()
        {
            this.InitializeComponent();
            var itemToUpdate = GlobalData.mouseDetailList.SingleOrDefault(r => r.MouseID == GlobalData.MouseID);
            if (itemToUpdate != null)
            {
                modelTextbox.Text = itemToUpdate.Model;
                codeTextbox.Text = itemToUpdate.Code_SN;
                dateTextbox.Text = itemToUpdate.Received_date;
                // Set the selected item in ComboBox for conditionTextbox
                SetConditionComboBox(itemToUpdate.Condition);
                remarkTextbox.Text = itemToUpdate.Remarks;
                ownerNameTextbox.Text = itemToUpdate.Owner.ToString();

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
        // Event handler for Back button click to navigate back to the updateMouse page
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateMouse));
        }

        // Event handler for Exit button click to close the application
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Event handler for Save button click to update the mouse details in the database
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

                        // Use parameterized query to prevent SQL injection attacks
                        updateCommand.CommandText = "UPDATE mouse SET Model=@Model, Code_SN=@Code_SN, Received_date=@Received_date, Condition=@Condition, Remarks=@Remarks,Owner=@Owner WHERE MouseID=@MouseID";

                        updateCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);


                        // Check if the date is in the correct format (yyyy-MM-dd)
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
                            // If the date is empty, set it to null in the database
                            updateCommand.Parameters.AddWithValue("@Received_date", DBNull.Value);
                        }
                        else
                        {
                            DateTime receivedDate;
                            if (DateTime.TryParseExact(dateTextbox.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out receivedDate))
                            {
                                updateCommand.Parameters.AddWithValue("@Received_date", receivedDate.ToString("yyyy-MM-dd"));
                            }
                            else
                            {
                                // Handle invalid date format
                                DisplayDialog("Input Error", "Enter a valid date in the format yyyy-MM-dd.");
                                return;
                            }
                        }
                        // Save the selected condition from ComboBox
                        updateCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        updateCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@MouseID", GlobalData.MouseID); // Add MouseID parameter

                        updateCommand.ExecuteNonQuery();

                        db.Close();
                    }

                    modelTextbox.Focus(FocusState.Programmatic);
                    DisplayDialog("Update", "Updated successfully.");
                    this.Frame.Navigate(typeof(updateMouse));
                }
                else
                {
                    DisplayDialog("Input Error", "Enter Employee name.");
                    modelTextbox.Focus(FocusState.Programmatic);
                }

            }

            catch (Exception theException)
            {
                DisplayDialog("Error: ", "Error: " + theException.Message);
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
