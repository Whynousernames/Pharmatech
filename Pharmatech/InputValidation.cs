using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Pharmatech
{
    public class InputValidation
    {
        public static Boolean validateIDNumber(string idNumber)
        {
            // Function to validate Patient's ID number. Adapted from a free online resource at: http://www.synerics.com/resources.html

            int validLength = 13;
            int controlDigitLocation = 12;
            int controlDigitCheckValue = 10;
            int controlDigitCheckExceptionValue = 9;
            string RegexIDPattern = "(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";
            bool valid = true;
            bool invalid = false;

            // assume that the id number is invalid
            bool isValidPattern = false;
            bool isValidLength = false;
            bool isValidControlDigit = false;

            // check length
            if (idNumber.Length == validLength)
            {
                isValidLength = true;
            }

            // match regex pattern, only if length is valid
            if (isValidLength)
            {
                Regex idPattern = new Regex(RegexIDPattern);

                if (idPattern.IsMatch(idNumber))
                {
                    //00 will slip through the regex and checksum
                    if (idNumber.Substring(2, 2) != "00" && idNumber.Substring(4, 2) != "00")
                    {
                        isValidPattern = true;
                    }
                }
            }

            // check control digit, only if previous validations passed
            if (isValidLength && isValidPattern)
            {
                int a = 0;
                int b = 0;
                int c = 0;
                int cDigit = -1;
                int tmp = 0;
                StringBuilder even = new StringBuilder();
                string evenResult = null;

                // sum odd digits
                for (int i = 0; i < validLength - 1; i = i + 2)
                {
                    a = a + int.Parse(idNumber[i].ToString());
                }

                // build a string containing even digits
                for (int i = 1; i < validLength - 1; i = i + 2)
                {
                    even.Append(idNumber[i]);
                }
                // multipy by 2
                tmp = int.Parse(even.ToString()) * 2;
                // convert to string again
                evenResult = tmp.ToString();
                // sum the digits in evenResult
                for (int i = 0; i < evenResult.Length; i++)
                {
                    b = b + int.Parse(evenResult[i].ToString());
                }

                c = a + b;

                cDigit = controlDigitCheckValue - int.Parse(c.ToString()[1].ToString());
                if (cDigit == int.Parse(idNumber[controlDigitLocation].ToString()))
                {
                    isValidControlDigit = true;
                }
                else
                {
                    if (cDigit > controlDigitCheckExceptionValue)
                    {
                        if (0 == int.Parse(idNumber[controlDigitLocation].ToString()))
                        {
                            isValidControlDigit = true;
                        }
                    }
                }
            }

            // final check
            if (isValidLength && isValidPattern && isValidControlDigit)
            {
                return valid;
            }
            else
            {
                return invalid;
            }
        }


        public static Boolean validateFirstName(string name)
        {
            // Function to validate first name input, using RegEx.

            Regex regex = new Regex(@"^[a - zA - Z'][a-zA-Z' - ] *[a - zA - Z']?$");
              if (name.Length >= 2 && regex.IsMatch(name))
            {
                return true;
            }
            
              else
            {
                return false;
            }
        }

        public static Boolean validateSurname(string name)
        {
            // Function to validate surname input, using RegEx.

            Regex regex = new Regex(@"^[a - zA - Z'][a-zA-Z' - ] *[a - zA - Z']?$");
            if (name.Length >= 2 && regex.IsMatch(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean validateEmail(string email)
        {
            // Function to validate an email address, using .NET's MailAddress class.
            
            try
            {
                MailAddress MailAddress = new MailAddress(email);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static Boolean validateAddress(string address)
        {
            // Function to validate residency address. Valid for both address line 1 and 2.

            Regex regex = new Regex(@"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$");
            if (address.Length >= 2 && regex.IsMatch(address))
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}
