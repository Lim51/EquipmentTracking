using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a monitor equipment item
    public class MonitorDetail
    {
        // Unique identifier for the monitor
        public int MonitorID { get; set; }

        // Model of the monitor
        public string Model { get; set; }

        // Code or serial number of the monitor
        public string Code_SN { get; set; }

        // Date when the monitor was received
        public string Received_date { get; set; }

        // Condition of the monitor
        public string Condition { get; set; }

        // Any remarks associated with the monitor
        public string Remarks { get; set; }

        // Owner of the monitor
        public string Owner { get; set; }
    }
}
