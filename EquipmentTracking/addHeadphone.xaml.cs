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
    public sealed partial class addHeadphone : Page
    {
        private string conn = (App.Current as App).ConnectionString; 
        public addHeadphone()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            modelTextbox.Text = "";
            codeTextbox.Text = "";
            dateTextbox.Text = "";
            remarkTextbox.Text = "";
            ownerNameTextbox.Text = "";
            conditionComboBox.SelectedIndex = -1; // Deselect any selected item in the combo box
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

                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO headphone (Model, Code_SN, Received_date, Condition, Remarks, Owner) VALUES (@Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner)";


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

                        insertCommand.Parameters.AddWithValue("@Condition", conditionComboBox.SelectedItem != null ? (conditionComboBox.SelectedItem as ComboBoxItem).Content.ToString() : "");
                        insertCommand.Parameters.AddWithValue("@Remarks", remarkTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@Owner", ownerNameTextbox.Text);
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
