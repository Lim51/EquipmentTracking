using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a docking  equipment item
    public class DockingDetail
    {
        // Unique identifier for the docking 
        public int DockingID { get; set; }

        // Model of the docking 
        public string Model { get; set; }

        // Code or serial number of the docking 
        public string Code_SN { get; set; }

        // Date when the docking  was received
        public string Received_date { get; set; }

        // Condition of the docking 
        public string Condition { get; set; }

        // Any remarks associated with the docking 
        public string Remarks { get; set; }

        // Owner of the docking 
        public string Owner { get; set; }
    }
}
