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

                        // Step 1: Retrieve the old data
                        SqlCommand selectOldDataCommand = new SqlCommand();
                        selectOldDataCommand.Connection = db;
                        selectOldDataCommand.CommandText = @"
                            SELECT LaptopID, Model, Code_SN, Received_date, Condition, Remarks, Owner
                            FROM laptop
                            WHERE LaptopID = @LaptopID";
                        selectOldDataCommand.Parameters.AddWithValue("@LaptopID", GlobalData.LaptopID);

                        SqlDataReader reader = selectOldDataCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            // Store old data in variables
                            var oldLaptopID = reader["LaptopID"];
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
                                INSERT INTO laptop_history (LaptopID, Model, Code_SN, Received_date, Condition, Remarks, Owner, UpdatedBy)
                                VALUES (@LaptopID, @Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner, @UpdatedBy)";
                            insertHistoryCommand.Parameters.AddWithValue("@LaptopID", oldLaptopID);
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

                        // Step 3: Update the laptop table with the new data
                        SqlCommand updateCommand = new SqlCommand();
                        updateCommand.Connection = db;
                        updateCommand.CommandText = @"
                            UPDATE laptop
                            SET Model = @Model, Code_SN = @Code_SN, Received_date = @Received_date, Condition = @Condition, Remarks = @Remarks, Owner = @Owner
                            WHERE LaptopID = @LaptopID";
                        updateCommand.Parameters.AddWithValue("@LaptopID", GlobalData.LaptopID);
                        updateCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);

                        // Check if the date is in the correct format (yyyy)
                        if (string.IsNullOrWhiteSpace(dateTextbox.Text))
                        {
                            updateCommand.Parameters.AddWithValue("@Received_date", DBNull.Value);
                        }
                        else
                        {
                            string yearPattern = @"^\d{4}$";
                            if (System.Text.RegularExpressions.Regex.IsMatch(dateTextbox.Text, yearPattern))
                            {
                                updateCommand.Parameters.AddWithValue("@Received_date", dateTextbox.Text);
                            }
                            else
                            {
                                DisplayDialog("Input Error", "Enter a valid year in the format yyyy.");
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
                    this.Frame.Navigate(typeof(updateLaptop));
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

        private async void DisplayDialog(string title, string content)
        {
            ContentDialog noDialog = new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = "Ok"
            };

            await noDialog.ShowAsync();
        }
    }
}