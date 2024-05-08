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
    public sealed partial class updateHeadphone : Page
    {
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
                GlobalData.HeadphoneDetailList = GlobalData.HeadphoneDetailList.OrderBy(h => h.GetType().GetProperty(columnName).GetValue(h, null)).ToList();
            }
            else
            {
                // Sort in descending order
                GlobalData.HeadphoneDetailList = GlobalData.HeadphoneDetailList.OrderByDescending(h => h.GetType().GetProperty(columnName).GetValue(h, null)).ToList();
            }

            // Update the ListView
            HeadphoneList.ItemsSource = GlobalData.HeadphoneDetailList;
        }

        public updateHeadphone()
        {
            this.InitializeComponent();
            SqlConnection con = null;
            try
            {
                // Creating Connection  
                con = new SqlConnection(conn);
                // writing sql query  
                SqlCommand cm = new SqlCommand("Select * from headphone", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                SqlDataReader sdr = cm.ExecuteReader();

                GlobalData.HeadphoneDetailList = new List<HeadphoneDetail>();
                // Iterating Data  
                while (sdr.Read())
                {
                    HeadphoneDetail h = new HeadphoneDetail();
                    if (!Convert.IsDBNull(sdr["hID"]))
                    {
                        h.hID = Convert.ToInt32(sdr["hID"]);
                    }

                    // Check if the columns are null or empty, if so, set them to "-"

                    h.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    h.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";            
                    h.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    h.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    h.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    h.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";
                    
                    GlobalData.HeadphoneDetailList.Add(h);
                    h = null;
                }

                HeadphoneList.ItemsSource = null;
                HeadphoneList.ItemsSource = GlobalData.HeadphoneDetailList;
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

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            string filterText = txtFilter.Text.Trim().ToLower();

            List<HeadphoneDetail> filteredList = new List<HeadphoneDetail>();

            foreach (var item in GlobalData.HeadphoneDetailList)
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

            HeadphoneList.ItemsSource = filteredList;
        }


        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // Clear filter criteria and display all records
            txtFilter.Text = "";


            HeadphoneList.ItemsSource = GlobalData.HeadphoneDetailList;
        }

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
                SqlCommand cm = new SqlCommand("delete from headphone where hID= '" + btn.Tag.ToString() + "'", con);
                // Opening Connection  
                con.Open();
                // Executing the SQL query  
                cm.ExecuteNonQuery();

                var itemToRemove = GlobalData.HeadphoneDetailList.SingleOrDefault(r => r.hID == Convert.ToInt32(btn.Tag.ToString()));
                if (itemToRemove != null)
                    GlobalData.HeadphoneDetailList.Remove(itemToRemove);


                HeadphoneList.ItemsSource = null;
                HeadphoneList.ItemsSource = GlobalData.HeadphoneDetailList;


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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            GlobalData.hID = Convert.ToInt32(btn.Tag.ToString());

            this.Frame.Navigate(typeof(UpdateHeadphoneDetail));
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
