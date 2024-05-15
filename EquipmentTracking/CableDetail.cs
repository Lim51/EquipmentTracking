using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentTracking
{
    // Class representing the details of a cable equipment item
    public class CableDetail
    {
        // Unique identifier for the cable
        public int CableID { get; set; }

        // Description of the cable
        public string cables { get; set; }

        // Quantity of the cable
        public int quantity { get; set; }
    }
}
