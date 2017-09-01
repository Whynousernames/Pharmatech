using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Sale
    {
        public int medID { get; set; }
        public string medName { get; set; }
        public string scheduleLevel { get; set; }
        public string description { get; set; }
        public int salePrice { get; set; }

        public int quantity { get; set; }

    }
}
