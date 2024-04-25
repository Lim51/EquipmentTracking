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
    public sealed partial class addCables : Page
    {
        private string conn = (App.Current as App).ConnectionString; 
        public addCables()
        {
            this.InitializeComponent();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cableNameTextbox.Text = "";
            quantityTextbox.Text = "";
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cableNameTextbox.Text))
                {
                    using (SqlConnection db = new SqlConnection(conn))
                    {
                        db.Open();

                        SqlCommand insertCommand = new SqlCommand();
                        insertCommand.Connection = db;

                        // Use parameterized query to prevent SQL injection attacks
                        insertCommand.CommandText = "INSERT INTO cable (cables, quantity) VALUES (@cables, @quantity)";

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

                    DisplayDialog("Insert", "New record inserted successfully.");
                }
                else
                {
                    DisplayDialog("Input Error", "Enter Cable Name.");
                    cableNameTextbox.Focus(FocusState.Programmatic);
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

