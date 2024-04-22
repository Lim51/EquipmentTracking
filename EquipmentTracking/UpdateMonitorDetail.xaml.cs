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
    public sealed partial class UpdateMonitorDetail : Page
    {
        private string conn = (App.Current as App).ConnectionString;

        public UpdateMonitorDetail()
        {
            this.InitializeComponent();
            var itemToUpdate = GlobalData.MonitorDetailList.SingleOrDefault(r => r.MonitorID == GlobalData.MonitorID);
            if (itemToUpdate != null)
            {
                modelTextbox.Text = itemToUpdate.Model;
                codeTextbox.Text = itemToUpdate.Code_SN;
                datePicker.Date = DateTime.Parse(itemToUpdate.Received_date);
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateMonitor));
        }

        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

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
                        updateCommand.CommandText = "UPDATE monitor SET Model=@Model, Code_SN=@Code_SN, Received_date=@Received_date, Condition=@Condition, Remarks=@Remarks,Owner=@Owner WHERE MonitorID='" + GlobalData.MonitorID.ToString() + "'";

                        updateCommand.Parameters.AddWithValue("@Model", modelTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Code_SN", codeTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Received_date", datePicker.Date.ToString("yyyy-MM-dd")); // Format the date correctly
                        // Save the selected condition from ComboBox
                        updateCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        updateCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        updateCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);

                        updateCommand.ExecuteNonQuery();

                        db.Close();
                    }

                    modelTextbox.Focus(FocusState.Programmatic);
                    DisplayDialog("Update", "updated successfully.");
                    this.Frame.Navigate(typeof(updateMonitor));

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
