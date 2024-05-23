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
    /// Represents a page for updating headphone details.
    /// </summary>
    public sealed partial class UpdateHeadphoneDetail : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        // Initializes a new instance of the UpdateHeadphoneDetail class.
        public UpdateHeadphoneDetail()
        {
            this.InitializeComponent();
            // Fetch the item to be updated from the global list based on hID
            var itemToUpdate = GlobalData.HeadphoneDetailList.SingleOrDefault(r => r.hID == GlobalData.hID);
            if (itemToUpdate != null)
            {
                // Set the text fields to the values of the item to be updated
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

        // Event handler for the back button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateHeadphone));
        }

        // Event handler for the exit command bar button
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Event handler for Save button click to update the headphone details in the database
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(modelTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        // Step 1: Retrieve the old data
                        SqlCommand selectOldDataCommand = new SqlCommand();
                        selectOldDataCommand.Connection = db;
                        selectOldDataCommand.CommandText = @"
                            SELECT hID, Model, Code_SN, Received_date, Condition, Remarks, Owner
                            FROM headphone
                            WHERE hID = @hID";
                        selectOldDataCommand.Parameters.AddWithValue("@hID", GlobalData.hID);

                        SqlDataReader reader = selectOldDataCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            // Store old data in variables
                            var oldhID = reader["hID"];
                            var oldModel = reader["Model"];
                            var oldCode_SN = reader["Code_SN"];
                            var oldReceived_date = reader["Received_date"];
                            var oldCondition = reader["Condition"];
                            var oldRemarks = reader["Remarks"];
                            var oldOwner = reader["Owner"];

                            reader.Close();

                            // Step 2: Insert the old data into the history table
                            SqlCommand insertHistoryCommand = new SqlCommand();
                            insertHistoryCommand.Connection = db;
                            insertHistoryCommand.CommandText = @"
                                INSERT INTO headphone_history (hID, Model, Code_SN, Received_date, Condition, Remarks, Owner, UpdatedBy)
                                VALUES (@hID, @Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner, @UpdatedBy)";
                            insertHistoryCommand.Parameters.AddWithValue("@hID", oldhID);
                            insertHistoryCommand.Parameters.AddWithValue("@Model", oldModel);
                            insertHistoryCommand.Parameters.AddWithValue("@Code_SN", oldCode_SN);
                            insertHistoryCommand.Parameters.AddWithValue("@Received_date", oldReceived_date);
                            insertHistoryCommand.Parameters.AddWithValue("@Condition", oldCondition);
                            insertHistoryCommand.Parameters.AddWithValue("@Remarks", oldRemarks);
                            insertHistoryCommand.Parameters.AddWithValue("@Owner", oldOwner);
                            insertHistoryCommand.Parameters.AddWithValue("@UpdatedBy", GlobalData.CurrentUser);

                            insertHistoryCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();
                            DisplayDialog("Error", "Old data not found.");
                            return;
                        }

                        // Step 3: Update the headphone table with the new data
                        SqlCommand updateCommand = new SqlCommand();
                        updateCommand.Connection = db;
                        updateCommand.CommandText = @"
                            UPDATE headphone
                            SET Model = @Model, Code_SN = @Code_SN, Received_date = @Received_date, Condition = @Condition, Remarks = @Remarks, Owner = @Owner
                            WHERE hID = @hID";
                        updateCommand.Parameters.AddWithValue("@hID", GlobalData.hID);
                        updateCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);

                        // Check if the date is in the correct format (yyyy-MM-dd)
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
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
                                DisplayDialog("Input Error", "Enter a valid date in the format yyyy-MM-dd.");
                                return;
                            }
                        }

                        updateCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        updateCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);

                        updateCommand.ExecuteNonQuery();

                        db.Close();
                    }

                    modelTextbox.Focus(FocusState.Programmatic);
                    DisplayDialog("Update", "Updated successfully.");
                    this.Frame.Navigate(typeof(updateHeadphone));
                }
                else
                {
                    DisplayDialog("Input Error", "Enter Model name.");
                    modelTextbox.Focus(FocusState.Programmatic);
                }
            }
            catch (Exception theException)
            {
                DisplayDialog("Error", "Error: " + theException.Message);
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
