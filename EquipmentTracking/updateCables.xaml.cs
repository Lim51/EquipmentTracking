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
    /// Represents a page for managing cable information.
    /// </summary>
    public sealed partial class updateCables : Page
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

        // Event handler for sorting by cable name
        private void SortByCable_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("cables");
        }

        // Event handler for sorting by quantity
        private void SortByQuantity_Click(object sender, PointerRoutedEventArgs e)
        {
            SortByColumn("quantity");
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
                GlobalData.CableDetailList = GlobalData.CableDetailList.OrderBy(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.CableDetailList = GlobalData.CableDetailList.OrderByDescending(m => m.GetType().GetProperty(columnName).GetValue(m, null)).ToList();
            }

            // Update the ListView
            CableList.ItemsSource = GlobalData.CableDetailList;
        }

        // Initializes a new instance of the updateCables class.
        public updateCables()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("Select * from cable", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                GlobalData.CableDetailList = new List<CableDetail>();
                // Iterating Data  
                while (sdr.Read())
                {
                    CableDetail c = new CableDetail();
                    if (!Convert.IsDBNull(sdr["CableID"]))
                    {
                        c.CableID = Convert.ToInt32(sdr["CableID"]);
                    }


                    // Check if the 'cables' column is null or empty, if so, set it to "-"
                    c.cables = !string.IsNullOrEmpty(sdr["cables"].ToString()) ? sdr["cables"].ToString() : "-";

                    // Check if the 'quantity' column is null or empty, if so, set it to 0
                    c.quantity = !string.IsNullOrEmpty(sdr["quantity"].ToString()) ? int.Parse(sdr["quantity"].ToString()) : 0;
                    GlobalData.CableDetailList.Add(c);
                    c = null;
                }

                // Set the ItemSource property of CableList to the CableDetailList
                CableList.ItemsSource = GlobalData.CableDetailList;
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

            List<CableDetail> filteredList = new List<CableDetail>();

            foreach (var item in GlobalData.CableDetailList)
            {
                // Check if any field contains the filter text
                if (item.cables.ToLower().Contains(filterText) ||
                    item.quantity.ToString().Contains(filterText))
                   
                {
                    filteredList.Add(item);
                }
            }

            CableList.ItemsSource = filteredList;
        }

        // Event handler for clearing filter
        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria and display all records
            txtFilter.Text = "";


            CableList.ItemsSource = GlobalData.CableDetailList;
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
                SqlCommand cm = new SqlCommand("delete from cable where cables= '" + btn.Tag.ToString() + "'", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();

                //var itemToRemove = GlobalData.CableDetailList.SingleOrDefault(r => r.cables == Convert.ToInt32(btn.Tag.ToString()));
                var itemToRemove = GlobalData.CableDetailList.SingleOrDefault(r => string.Equals(r.cables, btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.CableDetailList.Remove(itemToRemove);


                CableList.ItemsSource = null;
                CableList.ItemsSource = GlobalData.CableDetailList;


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

            GlobalData.CableID = Convert.ToInt32(btn.Tag.ToString());

            this.Frame.Navigate(typeof(UpdateCablesDetail));
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
