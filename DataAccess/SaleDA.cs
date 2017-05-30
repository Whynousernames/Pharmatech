using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SaleDA
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public static bool NewSale(string empID, string patientID, DateTime date, string descrip, string docName, string saleType, int saleAmount)
        {
            // Function to process a new sale. Adds record to the database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Sale (emplIDNumber, patientIDNumber, date, description, doctorName, saleType, saleAmount) VALUES (@empID, @patientID, @date, @descrip, @docName, @saleType, @saleAmount)";
                cmd.Parameters.AddWithValue("@empID", empID);
                cmd.Parameters.AddWithValue("@patientID", patientID);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@descrip", descrip);
                cmd.Parameters.AddWithValue("@docName", docName);
                cmd.Parameters.AddWithValue("@saleType", saleType);
                cmd.Parameters.AddWithValue("@saleAmount", saleAmount);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

    }
}
