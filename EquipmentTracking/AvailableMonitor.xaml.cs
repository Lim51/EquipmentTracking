﻿using System;
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
    public sealed partial class AvailableMonitor : Page
    {
        private string conn = (App.Current as App).ConnectionString;

        public enum SortDirection
        {
            Ascending,
            Descending
        }

        private string currentSortColumn = "";
        private SortDirection currentSortDirection = SortDirection.Ascending;

        public AvailableMonitor()
        {
            this.InitializeComponent();
            LoadAvailableMonitor();
        }
        private void LoadAvailableMonitor()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(conn);
                SqlCommand cm = new SqlCommand("SELECT * FROM monitor WHERE Owner IS NULL OR Owner = '' OR Owner = '-'", con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                GlobalData.MonitorDetailList = new List<MonitorDetail>();
                while (sdr.Read())
                {
                    MonitorDetail m = new MonitorDetail();
                    m.MonitorID = Convert.ToInt32(sdr["MonitorID"]);
                    m.Model = !string.IsNullOrEmpty(sdr["Model"].ToString()) ? sdr["Model"].ToString() : "-";
                    m.Code_SN = !string.IsNullOrEmpty(sdr["Code_SN"].ToString()) ? sdr["Code_SN"].ToString() : "-";
                    m.Received_date = !string.IsNullOrEmpty(sdr["Received_date"].ToString()) ? sdr["Received_date"].ToString() : "-";
                    m.Condition = !string.IsNullOrEmpty(sdr["Condition"].ToString()) ? sdr["Condition"].ToString() : "-";
                    m.Remarks = !string.IsNullOrEmpty(sdr["Remarks"].ToString()) ? sdr["Remarks"].ToString() : "-";
                    m.Owner = !string.IsNullOrEmpty(sdr["Owner"].ToString()) ? sdr["Owner"].ToString() : "-";
                    GlobalData.MonitorDetailList.Add(m);
                }
                MonitorList.ItemsSource = GlobalData.MonitorDetailList;
                con.Close();
            }
            catch (Exception ex)
            {
                display.Text = ex.Message;
            }
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterMice();
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            FilterMice();
        }

        private void FilterMice()
        {
            var filteredList = GlobalData.MonitorDetailList
                .Where(m => m.Model.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase)
                         || m.Code_SN.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase)
                         || m.Received_date.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase)
                         || m.Condition.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase)
                         || m.Remarks.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase)
                         || m.Owner.Contains(txtFilter.Text, StringComparison.OrdinalIgnoreCase))
                .ToList();
            MonitorList.ItemsSource = filteredList;
        }



        private void SortByModel_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Model");
        private void SortByCodeSN_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Code_SN");
        private void SortByDate_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Received_date");
        private void SortByCondition_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Condition");
        private void SortByRemark_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Remarks");
        private void SortByOwner_Click(object sender, PointerRoutedEventArgs e) => SortByColumn("Owner");

        private void SortByColumn(string columnName)
        {
            if (currentSortColumn == columnName)
            {
                currentSortDirection = currentSortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
            }
            else
            {
                currentSortColumn = columnName;
                currentSortDirection = SortDirection.Ascending;
            }

            var sortedList = currentSortDirection == SortDirection.Ascending
                ? GlobalData.MonitorDetailList.OrderBy(m => m.GetType().GetProperty(currentSortColumn).GetValue(m, null))
                : GlobalData.MonitorDetailList.OrderByDescending(m => m.GetType().GetProperty(currentSortColumn).GetValue(m, null));

            MonitorList.ItemsSource = sortedList.ToList();
        }

    }
}