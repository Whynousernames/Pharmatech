using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Allergies
    {
        //declaring object attributes
        private string _allergyID;
        private string _allergyName;
        private string _allergyDescription;

        //get and set
        public string allergyID { get; set; }
        public string allergyName{ get; set; }
        public string allergyDescription { get; set; }  

    }
}
