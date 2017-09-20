
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BusinessObject;

namespace DataAccess
{
    public class PatientDA
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public static bool AddPatient(string id, string fname, string sname, string contactNo, string email, string address1, string address2, string city, string suburb)
        {
            // Add new patient account to database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand()) 
            {
                cmd.CommandText = "INSERT INTO Patient(patientIDNumber, firstName, lastName, contactNumber, email, physicalAddress1, physicalAddress2, cityName, suburbName) VALUES (@id, @fName, @sName, @contactNo, @email, @address1, @address2, @cityName, @suburbName)";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@sname", sname);
                cmd.Parameters.AddWithValue("@contactNo", contactNo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address1", address1);
                cmd.Parameters.AddWithValue("@address2", address2);
                cmd.Parameters.AddWithValue("@cityName", city);
                cmd.Parameters.AddWithValue("@suburbName", suburb);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public static bool DeletePatient(string id)
        {
            // Delete patient account from database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Patient SET isActive = 'n' WHERE patientIDNumber = @id";
                cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public static bool UpdatePatient(string idNumber, string fname, string sname, string contactNo, string email, string address1, string address2, string city, string suburb)
        {
            // Update patient account on the database.
            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Patient SET firstName = @fname, lastName = @sname, contactNumber = @contactNo, email = @email, physicalAddress1 = @address1, physicalAddress2 = @address2, cityName = @city, suburbName = @suburb WHERE patientIDNumber = @idNumber";
                cmd.Parameters.Add("@idNumber", SqlDbType.NVarChar).Value = idNumber; 
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@sname", sname);
                cmd.Parameters.AddWithValue("@contactNo", contactNo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address1", address1);
                cmd.Parameters.AddWithValue("@address2", address2);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@suburb", suburb);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return true;
        }
    }
};
