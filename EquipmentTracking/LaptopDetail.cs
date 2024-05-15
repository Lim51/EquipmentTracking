using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a laptop equipment item
    public class LaptopDetail
    {
        // Unique identifier for the laptop
        public int LaptopID { get; set; }

        // Model of the laptop
        public string Model { get; set; }

        // Code or serial number of the laptop
        public string Code_SN { get; set; }

        // Date when the laptop was received
        public string Received_date { get; set; }

        // Condition of the laptop
        public string Condition { get; set; }

        // Any remarks associated with the laptop
        public string Remarks { get; set; }

        // Owner of the laptop
        public string Owner { get; set; }
    }
}
