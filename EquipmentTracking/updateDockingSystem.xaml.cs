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
    /// Represents a page for managing docking system information.
    /// </summary>
    public sealed partial class updateDockingSystem : Page
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

        // Event handlers for sorting columns
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
                GlobalData.DockingDetailList = GlobalData.DockingDetailList.OrderBy(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.DockingDetailList = GlobalData.DockingDetailList.OrderByDescending(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }

            // Update the ListView
            DockingList.ItemsSource = GlobalData.DockingDetailList;
        }

        // Initializes a new instance of the updateDockingSystem class.
        public updateDockingSystem()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("Select * from docking_sys", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                GlobalData.DockingDetailList = new List<DockingDetail>();
                // Iterating Data  
                while (sdr.Read())
                {
                    DockingDetail m = new DockingDetail();
                    if (!Convert.IsDBNull(sdr["DockingID"]))
                    {
                        m.DockingID = Convert.ToInt32(sdr["DockingID"]);
                    }

                    // Check if the columns are null or empty, if so, set them to "-"
                    m.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    m.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";
                    m.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    m.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    m.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    m.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";

                    GlobalData.DockingDetailList.Add(m);
                    m = null;
                }

                DockingList.ItemsSource = null;
                DockingList.ItemsSource = GlobalData.DockingDetailList;
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

            List<DockingDetail> filteredList = new List<DockingDetail>();

            foreach (var item in GlobalData.DockingDetailList)
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

            DockingList.ItemsSource = filteredList;
        }

        // Event handler for clearing filter
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria and display all records
            txtFilter.Text = "";


            DockingList.ItemsSource = GlobalData.DockingDetailList;
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
                SqlCommand selectOldDataCommand = new SqlCommand();
                selectOldDataCommand.Connection = con;
                selectOldDataCommand.CommandText = @"
            SELECT DockingID, Model, Code_SN, Received_date, Condition, Remarks, Owner
            FROM docking_sys
            WHERE DockingID = @DockingID";
                selectOldDataCommand.Parameters.AddWithValue("@DockingID", btn.Tag.ToString());

                con.Open();
                // Retrieve the old data before deleting the record
                SqlDataReader reader = selectOldDataCommand.ExecuteReader();
                if (reader.Read())
                {
                    // Store old data in variables
                    var oldDockingID = reader["DockingID"];
                    var oldModel = reader["Model"];
                    var oldCode_SN = reader["Code_SN"];
                    var oldReceived_date = reader["Received_date"];
                    var oldCondition = reader["Condition"];
                    var oldRemarks = reader["Remarks"];
                    var oldOwner = reader["Owner"];

                    reader.Close();

                    // Insert the old data into the history table
                    SqlCommand insertHistoryCommand = new SqlCommand();
                    insertHistoryCommand.Connection = con;
                    insertHistoryCommand.CommandText = @"
                INSERT INTO docking_history (DockingID, Model, Code_SN, Received_date, Condition, Remarks, Owner, UpdatedBy)
                VALUES (@DockingID, @Model, @Code_SN, @Received_date, @Condition, @Remarks, @Owner, @UpdatedBy)";
                    insertHistoryCommand.Parameters.AddWithValue("@DockingID", oldDockingID);
                    insertHistoryCommand.Parameters.AddWithValue("@Model", oldModel);
                    insertHistoryCommand.Parameters.AddWithValue("@Code_SN", oldCode_SN);
                    insertHistoryCommand.Parameters.AddWithValue("@Received_date", oldReceived_date);
                    insertHistoryCommand.Parameters.AddWithValue("@Condition", oldCondition);
                    insertHistoryCommand.Parameters.AddWithValue("@Remarks", oldRemarks);
                    insertHistoryCommand.Parameters.AddWithValue("@Owner", oldOwner);
                    insertHistoryCommand.Parameters.AddWithValue("@UpdatedBy", GlobalData.CurrentUser);

                    insertHistoryCommand.ExecuteNonQuery();
                }

                // Now delete the record from the docking_sys table
                SqlCommand deleteCommand = new SqlCommand();
                deleteCommand.Connection = con;
                deleteCommand.CommandText = "DELETE FROM docking_sys WHERE DockingID=@DockingID";
                deleteCommand.Parameters.AddWithValue("@DockingID", btn.Tag.ToString());
                deleteCommand.ExecuteNonQuery();

                var itemToRemove = GlobalData.DockingDetailList.SingleOrDefault(r => r.DockingID == Convert.ToInt32(btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.DockingDetailList.Remove(itemToRemove);

                DockingList.ItemsSource = null;
                DockingList.ItemsSource = GlobalData.DockingDetailList;

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

            GlobalData.DockingID = Convert.ToInt32(btn.Tag.ToString());
            
            // Navigate to the page for updating docking details
            this.Frame.Navigate(typeof(UpdateDockingDetail));
        }

        // Method to display a dialog box
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
