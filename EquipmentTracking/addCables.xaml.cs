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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EquipmentTracking
{
    /// <summary>
    /// Page for adding details of a cable.
    /// </summary>
    public sealed partial class addCables : Page
    {
        // Connection string for database
        private string conn = (App.Current as App).ConnectionString;

        //Initializes a new instance of the addCables class.
        public addCables()
        {
            this.InitializeComponent();
        }

        // Event handler for the "Clear" button click.
        // Clears input fields.
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cableNameTextbox.Text = "";
            quantityTextbox.Text = "";
        }

        
        // Saves cable details to the database.
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if cable name is provided
                if (!string.IsNullOrEmpty(cableNameTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO cable (cables, quantity) VALUES (@cables, @quantity)";

                        // Add parameters
                        insertCommand.Parameters.AddWithValue("@cables", cableNameTextbox.Text);
                        insertCommand.Parameters.AddWithValue("@quantity", quantityTextbox.Text);

                        // Execute the SQL command to insert data into the database
                        insertCommand.ExecuteNonQuery();

                        db.Close();
                    }

                    // Reset input fields after successful insertion
                    cableNameTextbox.Text = "";
                    quantityTextbox.Text = "";
                    cableNameTextbox.Focus(FocusState.Programmatic);

                    // Display success message
                    DisplayDialog("Insert", "New record inserted successfully.");
                }
                else
                {
                    // Display error message if cable name is not provided
                    DisplayDialog("Input Error", "Enter Cable Name.");
                    cableNameTextbox.Focus(FocusState.Programmatic);
                }
            }
            catch (Exception theException)
            {

                // Display error message if an exception occurs
                DisplayDialog("Error", "Error: " + theException.Message);
            }
        }

        // Displays a dialog with the specified title and content.
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

