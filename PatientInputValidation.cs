using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmatech
{
    public class PatientInputValidation
    {
        private string _id;
        private string _fname;
        private string _sname;
        private string _contactNo;
        private string _email;
        private string address1;
        private string address2;

        public string FirstName
        {
            get { return _fname; }
            set
            {
                _fname = value;
                if(String.IsNullOrEmpty(value))
                {
                    throw new ApplicationException("Name fields are mandatory.");
                }
               
            }

        }
    }
};
