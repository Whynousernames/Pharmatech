using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Patient
    {
        //delcaring table attributes
        private string _patientIDnumber;
        private string _fName;
        private string _lName;
        private string _contactNumber;
        private string _email;
        private string _address1;
        private string _address2;

        //get and set
        public string patientIDnumber{ get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }

    }
}
