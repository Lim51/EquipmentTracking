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
    /// Represents a page for updating cable details.
    /// </summary>
    public sealed partial class UpdateCablesDetail : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        // Initializes a new instance of the UpdateCablesDetail class.
        public UpdateCablesDetail()
        {
            this.InitializeComponent();

            // Fetch the item to be updated from the global list based on CableID
            var itemToUpdate = GlobalData.CableDetailList.SingleOrDefault(r => r.CableID == GlobalData.CableID);
            if (itemToUpdate != null)
            {
                // Set the text fields to the values of the item to be updated
                cableNameTextbox.Text = itemToUpdate.cables;
                quantityTextbox.Text = itemToUpdate.quantity.ToString();

            }
        }

        // Event handler for the back button
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(updateCables));
        }

        // Event handler for the exit command bar button
        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        // Event handler for the save button
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cableNameTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        SqlCommand updateCommand = new SqlCommand();
                        updateCommand.Connection = db;

                        //Use parameterized query to prevent SQL injection attacks
                        updateCommand.CommandText = "UPDATE cable SET cables=@cables, quantity=@quantity WHERE CableID='" + GlobalData.CableID.ToString() + "'";
                       
                        // Set cables parameter
                        updateCommand.Parameters.AddWithValue("@cables", cableNameTextbox.Text);

                        // Set quantity parameter
                        if (string.IsNullOrEmpty(quantityTextbox.Text))
                        {
                            // If quantity is empty or null, set it to 0
                            updateCommand.Parameters.AddWithValue("@quantity", 0);
                        }
                        else
                        {
                            // Parse quantity value
                            int quantity;
                            if (!int.TryParse(quantityTextbox.Text, out quantity))
                            {
                                // Quantity is not a valid integer, display error
                                DisplayDialog("Input Error", "Enter a valid quantity.");
                                return;
                            }
                            // Set valid quantity
                            updateCommand.Parameters.AddWithValue("@quantity", quantity);
                        }

                        // Execute the SQL command to update data in the database
                        updateCommand.ExecuteNonQuery();

                        db.Close();
                    }
                    // Focus on cableNameTextbox
                    cableNameTextbox.Focus(FocusState.Programmatic);
                    DisplayDialog("Update", "Updated successfully.");
                    this.Frame.Navigate(typeof(updateCables));

                }
                else
                {
                    // Display error if cableName is empty
                    DisplayDialog("Input Error", "Enter Cable name.");
                    cableNameTextbox.Focus(FocusState.Programmatic);
                }

            }

            catch (Exception theException)
            {
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
