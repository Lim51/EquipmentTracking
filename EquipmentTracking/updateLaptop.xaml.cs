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
    /// Represents a page for managing laptop information.
    /// </summary>
    public sealed partial class updateLaptop : Page
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
                GlobalData.LaptopDetailList = GlobalData.LaptopDetailList.OrderBy(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.LaptopDetailList = GlobalData.LaptopDetailList.OrderByDescending(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }

            // Update the ListView
            LaptopList.ItemsSource = GlobalData.LaptopDetailList;
        }

        // Constructor
        public updateLaptop()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("Select * from laptop", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                GlobalData.LaptopDetailList = new List<LaptopDetail>();
                // Iterating Data  
                while (sdr.Read())
                {
                    LaptopDetail m = new LaptopDetail();
                    if (!Convert.IsDBNull(sdr["LaptopID"]))
                    {
                        m.LaptopID = Convert.ToInt32(sdr["LaptopID"]);
                    }

                    // Check if the columns are null or empty, if so, set them to "-"
                    m.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    m.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";
                    m.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    m.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    m.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    m.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";

                    GlobalData.LaptopDetailList.Add(m);
                    m = null;
                }
                // Set the item source for the ListView
                LaptopList.ItemsSource = null;
                LaptopList.ItemsSource = GlobalData.LaptopDetailList;
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

            List<LaptopDetail> filteredList = new List<LaptopDetail>();

            foreach (var item in GlobalData.LaptopDetailList)
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
            // Update the ListView with filtered data
            LaptopList.ItemsSource = filteredList;
        }

        // Event handler for clearing filter
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria and display all records
            txtFilter.Text = "";


            LaptopList.ItemsSource = GlobalData.mouseDetailList;
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
            SELECT LaptopID, Model, Code_SN, Received_date, Condition, Remarks, Owner
            FROM laptop
            WHERE LaptopID = @LaptopID";
                selectOldDataCommand.Parameters.AddWithValue("@LaptopID", btn.Tag.ToString());

                con.Open();
                // Retrieve the old data before deleting the record
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

                    // Insert the old data into the history table
                    SqlCommand insertHistoryCommand = new SqlCommand();
                    insertHistoryCommand.Connection = con;
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

                // Now delete the record from the laptop table
                SqlCommand deleteCommand = new SqlCommand();
                deleteCommand.Connection = con;
                deleteCommand.CommandText = "DELETE FROM laptop WHERE LaptopID=@LaptopID";
                deleteCommand.Parameters.AddWithValue("@LaptopID", btn.Tag.ToString());
                deleteCommand.ExecuteNonQuery();

                var itemToRemove = GlobalData.LaptopDetailList.SingleOrDefault(r => r.LaptopID == Convert.ToInt32(btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.LaptopDetailList.Remove(itemToRemove);

                LaptopList.ItemsSource = null;
                LaptopList.ItemsSource = GlobalData.LaptopDetailList;

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

            GlobalData.LaptopID = Convert.ToInt32(btn.Tag.ToString());

            // Navigate to the UpdateLaptopDetail page for updating the record
            this.Frame.Navigate(typeof(UpdateLaptopDetail));
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
