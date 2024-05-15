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
    /// Represents a page for managing mouse information.
    /// </summary>
    public sealed partial class updateMouse : Page
    {
        // Connection string to the database
        private string conn = (App.Current as App).ConnectionString;

        // Define enum for sorting order
        public enum SortDirection
        {
            Ascending,
            Descending
        }

        // Track current sorting column and direction
        private string currentSortColumn = "";
        private SortDirection currentSortDirection = SortDirection.Ascending;

        // Event handler for sorting by Model column
        private void SortByModel_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Model");
        }
        // Event handler for sorting by Code_SN column
        private void SortByCodeSN_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Code_SN");
        }
        // Event handler for sorting by Received_date column
        private void SortByDate_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Received_date");
        }
        // Event handler for sorting by Condition column
        private void SortByCondition_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Condition");
        }
        // Event handler for sorting by Remarks column
        private void SortByRemark_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Remarks");
        }
        // Event handler for sorting by Owner column
        private void SortByOwner_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Owner");
        }
        // Generic method to sort by any column
        private void SortByColumn(string columnName)
        {
            // Toggle sorting direction if the same column is clicked
            if (columnName == currentSortColumn)
            {
                currentSortDirection = currentSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                currentSortDirection = SortDirection.Ascending;
            }

            // Update current sort column
            currentSortColumn = columnName;

            // Sort the list based on the selected column and direction
            if (currentSortDirection == SortDirection.Ascending)
            {
                // Sort in ascending order
                GlobalData.mouseDetailList = GlobalData.mouseDetailList.OrderBy(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.mouseDetailList = GlobalData.mouseDetailList.OrderByDescending(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }

            // Update the ListView
            MouseList.ItemsSource = GlobalData.mouseDetailList;
        }

        // Constructor to initialize the page and load data
        public updateMouse()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating connection to the database
                con = new SqlConnection(conn);
                // Writing SQL query to select all records from the mouse table
                SqlCommand cm = new SqlCommand("Select * from mouse", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();
                // Initialize the list to hold mouse details
                GlobalData.mouseDetailList = new List<MouseDetail>();
                // Iterating through the data
                while (sdr.Read())
                {
                    MouseDetail m = new MouseDetail();
                    if (!Convert.IsDBNull(sdr["MouseID"]))
                    {
                        m.MouseID = Convert.ToInt32(sdr["MouseID"]);
                    }

                    // Check if the columns are null or empty, if so, set them to "-"
                    m.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    m.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";
                    m.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    m.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    m.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    m.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";
                    
                    // Add the mouse details to the list
                    GlobalData.mouseDetailList.Add(m);
                    m = null;
                }
                // Update the ListView with the data
                MouseList.ItemsSource = null;
                MouseList.ItemsSource = GlobalData.mouseDetailList;
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

        // Event handler to apply filter based on the user input
        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            string filterText = txtFilter.Text.Trim().ToLower();
            // Filter the list based on the input text
            List<MouseDetail> filteredList = new List<MouseDetail>();

            foreach (var item in GlobalData.mouseDetailList)
            {
                // Check if any field contains the filter text
                if (item.Model.ToLower().Contains(filterText) ||
                    item.Code_SN.ToLower().Contains(filterText) ||
                    item.Received_date.ToLower().Contains(filterText) ||
                    item.Condition.ToLower().Contains(filterText) ||
                    item.Remarks.ToLower().Contains(filterText) ||
                    item.Owner.ToLower().Contains(filterText))
                {
                    filteredList.Add(item);
                }
            }
            // Update the ListView with the filtered data
            MouseList.ItemsSource = filteredList;
        }

        // Event handler to clear the filter and display all records
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria 
            txtFilter.Text = "";

            // Display all records
            MouseList.ItemsSource = GlobalData.mouseDetailList;
        }

        // Event handler to delete a record
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            display.Text = "";

            Button btn = sender as Button;

            SqlConnection con = null;
            try
            {
                // Creating connection to the database 
                con = new SqlConnection(conn);
                // Writing SQL query to delete a record by MouseID
                SqlCommand cm = new SqlCommand("delete from mouse where MouseID= '" + btn.Tag.ToString() + "'", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();

                // Remove the deleted item from the list
                var itemToRemove = GlobalData.mouseDetailList.SingleOrDefault(r => r.MouseID == Convert.ToInt32(btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.mouseDetailList.Remove(itemToRemove);

                // Update the ListView
                MouseList.ItemsSource = null;
                MouseList.ItemsSource = GlobalData.mouseDetailList;

                // Display success message
                display.Text = "Record Deleted Successfully";
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

        // Event handler to navigate to the update details page
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            // Store the selected MouseID globally
            GlobalData.MouseID = Convert.ToInt32(btn.Tag.ToString());

            // Navigate to the UpdateMouseDetail page
            this.Frame.Navigate(typeof(UpdateMouseDetail));
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
