using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Pharmatech
{
    public class PatientValidation : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _id;
        private string _fname;
        private string _sname;
        private string _email;
        private string _contactNo;
        private string _address1;
        private string _address2;
        private string _suburb;
        private string _city;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));

        }

        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Fname
        {
            get
            {
                return _fname;
            }

            set
            {
                _fname = value;
            }
        }

        public string Sname
        {
            get
            {
                return _sname;
            }

            set
            {
                _sname = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string ContactNo
        {
            get
            {
                return _contactNo;
            }

            set
            {
                _contactNo = value;
            }
        }

        public string Address1
        {
            get
            {
                return _address1;
            }

            set
            {
                _address1 = value;
            }
        }

        public string Address2
        {
            get
            {
                return _address2;
            }

            set
            {
                _address2 = value;
            }
        }

        public string Suburb
        {
            get
            {
                return _suburb;
            }

            set
            {
                _suburb = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string PropertyName]
        {
            get
            {
                string result = null;

                switch (PropertyName)
                {
                    case nameof(Id):
                        if (string.IsNullOrEmpty(Id))
                            result = "ID number is required.";

                        else if (InputValidation.validateIDNumber(Id) != true)
                            result = "Invalid ID number.";
                        break;

                    case nameof(Fname):
                        if (string.IsNullOrEmpty(Fname))
                            result = "First name is required.";
                        else if (Fname.Length < 2)
                            result = "Invalid name entered. Too short.";
                        break;

                    case nameof(Sname):
                        if (string.IsNullOrEmpty(Sname))
                            result = "Surname is required.";
                        else if (Sname.Length < 2)
                            result = "Invalid name entered. Too short.";
                        break;

                    case nameof(Email):
                        if (string.IsNullOrEmpty(Email))
                            result = "Email address is required.";

                        else if (InputValidation.emailIsValid(Email) != true)
                            result = "Invalid email address.";
                        break;

                    case nameof(ContactNo):
                        if (string.IsNullOrEmpty(ContactNo))
                            result = "Contact number is required.";
                        else if (ContactNo.Length != 10)
                            result = "Invalid contact number entered. Include area code.";
                        break;

                    case nameof(Address1):
                        if (string.IsNullOrEmpty(Address1))
                            result = "Address is required.";
                        break;

                    case nameof(Address2):
                        if (string.IsNullOrEmpty(Address2))
                            result = "Address is required.";
                        break;

                    case nameof(Suburb):
                        if (string.IsNullOrEmpty(Suburb))
                            result = "Suburb is required.";
                        else if (Suburb.Length < 3)
                            result = "Invalid suburb entered. Too short.";
                        break;

                    case nameof(City):
                        if (string.IsNullOrEmpty(City))
                            result = "City is required.";
                        else if (City.Length < 3)
                            result = "Invalid city entered. Too short.";
                        break;

                }
                return result;
            }
        }
    }
}
