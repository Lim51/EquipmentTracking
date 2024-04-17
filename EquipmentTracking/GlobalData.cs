using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    internal class GlobalData
    {

        public static List<MouseDetail> mouseDetailList = new List<MouseDetail>();
        public static List<HeadphoneDetail> HeadphoneDetailList = new List<HeadphoneDetail>();
        public static List<MonitorDetail> MonitorDetailList = new List<MonitorDetail>();
        public static List<DockingDetail> DockingDetailList = new List<DockingDetail>();
        public static List<LaptopDetail> LaptopDetailList = new List<LaptopDetail>();

        public static int MouseID { get; set; }
        public static int hID { get; set; }
        public static int MonitorID { get; set; }
        public static int DockingID { get; set; }
        public static int LaptopID { get; set; }
    }
}
