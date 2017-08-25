using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AllergiesDA
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public static bool AddAllergy(string allergyID, string patientID)
        {
            // Function to process adding of a paitents allergies. Adds record to the database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO PatientAllergies (allergyID, patientIDNumber) VALUES (@allergyID, @patientID)";
                cmd.Parameters.AddWithValue("@allergyID", allergyID);
                cmd.Parameters.AddWithValue("@patientID", patientID);                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
    }
}
