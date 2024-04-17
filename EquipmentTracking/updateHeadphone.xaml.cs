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

                    h.Model = sdr["Model"].ToString();
                    h.Code_SN = sdr["Code_SN"].ToString();
                    // Format the date
                    h.Received_date = sdr["Received_date"].ToString();
                    h.Condition = sdr["Condition"].ToString();
                    h.Remarks = sdr["Remarks"].ToString();
                    h.OwnerID = sdr["OwnerID"] != DBNull.Value ? Convert.ToInt32(sdr["OwnerID"]) : 0;
                    /*if (!Convert.IsDBNull(sdr["OwnerID"]))
                    {
                        m.OwnerID = Convert.ToInt32(sdr["OwnerID"]);
                    }*/
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
