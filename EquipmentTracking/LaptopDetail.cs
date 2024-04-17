using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    public class LaptopDetail
    {
        public int LaptopID { get; set; }
        public string Model { get; set; }
        public string Code_SN { get; set; }
        public string Received_date { get; set; }
        public string Condition { get; set; }
        public string Remarks { get; set; }
        public int OwnerID { get; set; }
    }
}
