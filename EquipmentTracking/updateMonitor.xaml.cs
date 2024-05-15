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
    /// Represents a page for managing monitor information.
    /// </summary>
    public sealed partial class updateMonitor : Page
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

        // Implement sorting logic for each column
        private void SortByModel_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Model");
        }

        private void SortByCodeSN_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Code_SN");
        }

        private void SortByDate_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Received_date");
        }
        private void SortByCondition_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Condition");
        }
        private void SortByRemark_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Remarks");
        }
        private void SortByOwner_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("Owner");
        }
        // Generic method to sort by column
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
                GlobalData.MonitorDetailList = GlobalData.MonitorDetailList.OrderBy(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.MonitorDetailList = GlobalData.MonitorDetailList.OrderByDescending(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }

            // Update the ListView
            MonitorList.ItemsSource = GlobalData.MonitorDetailList;
        }

        public updateMonitor()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("Select * from monitor", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                GlobalData.MonitorDetailList = new List<MonitorDetail>();
                // Iterating Data  
                while (sdr.Read())
                {
                    MonitorDetail m = new MonitorDetail();
                    if (!Convert.IsDBNull(sdr["MonitorID"]))
                    {
                        m.MonitorID = Convert.ToInt32(sdr["MonitorID"]);
                    }
                    // Check if the columns are null or empty, if so, set them to "-"
                    m.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    m.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";
                    m.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    m.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    m.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    m.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";

                    GlobalData.MonitorDetailList.Add(m);
                    m = null;
                }

                MonitorList.ItemsSource = null;
                MonitorList.ItemsSource = GlobalData.MonitorDetailList;
            }
            catch (Exception ex)
            {
                DisplayDialog("Error: ", "Error: " + ex.Message);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        // Event handler for applying filter
        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            string filterText = txtFilter.Text.Trim().ToLower();

            List<MonitorDetail> filteredList = new List<MonitorDetail>();

            foreach (var item in GlobalData.MonitorDetailList)
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

            MonitorList.ItemsSource = filteredList;
        }

        // Event handler for clearing filter
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria and display all records
            txtFilter.Text = "";


            MonitorList.ItemsSource = GlobalData.MonitorDetailList;
        }

        // Event handler for deleting a record
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            display.Text = "";

            Button btn = sender as Button;

            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("delete from monitor where MonitorID= '" + btn.Tag.ToString() + "'", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();

                var itemToRemove = GlobalData.MonitorDetailList.SingleOrDefault(r => r.MonitorID == Convert.ToInt32(btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.MonitorDetailList.Remove(itemToRemove);


                MonitorList.ItemsSource = null;
                MonitorList.ItemsSource = GlobalData.MonitorDetailList;


                display.Text = "Record Deleted Successfully";
            }
            catch (Exception ex)
            {
                DisplayDialog("Error: ", "Error: " + ex.Message);
            }
            // Closing the connection  
            finally
            {
                con.Close();
            }
        }

        // Event handler for updating a record
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            GlobalData.MonitorID = Convert.ToInt32(btn.Tag.ToString());

            this.Frame.Navigate(typeof(UpdateMonitorDetail));
        }

        // Method to display a dialog
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