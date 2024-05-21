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
    public sealed partial class view : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;
        public view()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating connection to the database
                con = new SqlConnection(conn);
                con.Open();

                // Counting available mouse
                SqlCommand cmMouse = new SqlCommand("SELECT COUNT(MouseID) FROM mouse WHERE Owner IS NULL", con);
                int mouseCount = (int)cmMouse.ExecuteScalar();
                MouseCount.Text = mouseCount.ToString();

                // Counting available headphones
                SqlCommand cmHeadphone = new SqlCommand("SELECT COUNT(hID) FROM headphone WHERE Owner IS NULL", con);
                int headphoneCount = (int)cmHeadphone.ExecuteScalar();
                HeadphoneCount.Text = headphoneCount.ToString();

                // Counting available monitors
                SqlCommand cmMonitor = new SqlCommand("SELECT COUNT(MonitorID) FROM monitor WHERE Owner IS NULL", con);
                int monitorCount = (int)cmMonitor.ExecuteScalar();
                MonitorCount.Text = monitorCount.ToString();

                // Counting available docking
                SqlCommand cmDocking = new SqlCommand("SELECT COUNT(DockingID) FROM docking_sys WHERE Owner IS NULL", con);
                int dockingCount = (int)cmMonitor.ExecuteScalar();
                DockingCount.Text = dockingCount.ToString();

                // Counting available docking
                SqlCommand cmLaptop = new SqlCommand("SELECT COUNT(LaptopID) FROM laptop WHERE Owner IS NULL", con);
                int laptopCount = (int)cmMonitor.ExecuteScalar();
                LaptopCount.Text = laptopCount.ToString();
            }
            catch (Exception ex)
            {
                // Display an error dialog if an exception occurs
                DisplayDialog("Error: ", "Error: " + ex.Message);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        // Event handler for the Docking System filter button click
        private void FilterDocking_Click(object sender, RoutedEventArgs e)
        {
            // Get the owner filter criteria from the text box
            string ownerFilter = DockingFilterTextBox.Text;

            // Modify the SQL query based on the owner filter criteria
            string query = "SELECT * FROM docking_sys WHERE Owner = @Owner";

            // Execute the modified query to get the filtered data
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Owner", ownerFilter);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    

                    while (reader.Read())
                    {
                        DockingDetail docking = new DockingDetail();
                        docking.Model = reader["Model"].ToString();
                        docking.Code_SN = reader["Code_SN"].ToString();
                       
                        DockingList.ItemsSource = null;
                        
                    }

                    // Bind the ListView to the filtered data
                    DockingList.ItemsSource = GlobalData.DockingDetailList;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    DisplayDialog("Error: ", "Error: " + ex.Message);
                }
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void exitCommandBar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
