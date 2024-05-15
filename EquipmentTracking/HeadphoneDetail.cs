using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a headphone equipment item
    public class HeadphoneDetail
    {
        // Unique identifier for the headphone
        public int hID { get; set; }

        // Model of the headphone
        public string Model { get; set; }

        // Code or serial number of the headphone
        public string Code_SN { get; set; }

        // Date when the headphone was received
        public string Received_date { get; set; }

        // Condition of the headphone
        public string Condition { get; set; }

        // Any remarks associated with the headphone
        public string Remarks { get; set; }

        // Owner of the headphone
        public string Owner { get; set; }
    }
}
