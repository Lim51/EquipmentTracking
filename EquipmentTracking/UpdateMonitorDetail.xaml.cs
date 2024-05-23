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
    /// Represents a page for updating monitor details.
    /// </summary>
    public sealed partial class UpdateMonitorDetail : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        public UpdateMonitorDetail()
        {
            this.InitializeComponent();
            // Retrieve the monitor item to update based on the GlobalData.MonitorID
            var itemToUpdate = GlobalData.MonitorDetailList.SingleOrDefault(r => r.MonitorID == GlobalData.MonitorID);
            if (itemToUpdate != null)
            {
                // Populate the UI elements with the monitor details
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

        // Event handler for the Back button click
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateMonitor));
        }

        // Event handler for the exitCommandBar click
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Event handler for Save button click to update the monitor details in the database
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
                            SELECT MonitorID, Model, Code_SN, Received_date, Condition, Remarks, Owner
                            FROM monitor
                            WHERE MonitorID = @MonitorID";
                        selectOldDataCommand.Parameters.AddWithValue("@MonitorID", GlobalData.MonitorID);

                        SqlDataReader reader = selectOldDataCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            // Store old data in variables
                            var oldMonitorID = reader["MonitorID"];
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
                                INSERT INTO monitor_history (MonitorID, Model, Code_SN, Received_date, Condition, Remarks, Owner, UpdatedBy)
                                VALUES (@MonitorID, @Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner, @UpdatedBy)";
                            insertHistoryCommand.Parameters.AddWithValue("@MonitorID", oldMonitorID);
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

                        // Step 3: Update the monitor table with the new data
                        SqlCommand updateCommand = new SqlCommand();
                        updateCommand.Connection = db;
                        updateCommand.CommandText = @"
                            UPDATE monitor
                            SET Model = @Model, Code_SN = @Code_SN, Received_date = @Received_date, Condition = @Condition, Remarks = @Remarks, Owner = @Owner
                            WHERE MonitorID = @MonitorID";
                        updateCommand.Parameters.AddWithValue("@MonitorID", GlobalData.MonitorID);
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
                    this.Frame.Navigate(typeof(updateMonitor));
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


        // Method to display a content dialog
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
