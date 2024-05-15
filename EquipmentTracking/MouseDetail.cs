using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a mouse equipment item
    public class MouseDetail
    {
        // Unique identifier for the mouse
        public int MouseID { get; set; }

        // Model of the mouse
        public string Model { get; set; }

        // Code or serial number of the mouse
        public string Code_SN { get; set; }

        // Date when the mouse was received
        public string Received_date { get; set; }

        // Condition of the mouse
        public string Condition { get; set; }

        // Any remarks associated with the mouse
        public string Remarks { get; set; }

        // Owner of the mouse
        public string Owner { get; set; }
    }
}

