using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class for storing global data related to equipment
    internal class GlobalData
    {
        // Lists to store details of different types of equipment
        public static List<MouseDetail> mouseDetailList = new List<MouseDetail>();
        public static List<HeadphoneDetail> HeadphoneDetailList = new List<HeadphoneDetail>();
        public static List<MonitorDetail> MonitorDetailList = new List<MonitorDetail>();
        public static List<DockingDetail> DockingDetailList = new List<DockingDetail>();
        public static List<LaptopDetail> LaptopDetailList = new List<LaptopDetail>();
        public static List<CableDetail> CableDetailList = new List<CableDetail>();

        // Properties to store IDs for different equipment types
        public static int MouseID { get; set; }      // ID for mouse
        public static int hID { get; set; }          // ID for headphone
        public static int MonitorID { get; set; }    // ID for monitor
        public static int DockingID { get; set; }    // ID for docking station
        public static int LaptopID { get; set; }     // ID for laptop
        public static int CableID { get; set; }      // ID for cable


        public static string CurrentUser { get; set; }
    }
}
