using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class MedicationDA
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public static bool AddMedication(string name, string schedLevel, string descrip, int costprice, int saleprice, int quantity)
        {
            // Function to add new medication items to the database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Medication (medName, scheduleLevel, description, costPrice, salePrice, quantityInStock) VALUES (@name, @schedlevel, @descrip, @costprice, @saleprice, @quantity)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@schedlevel", schedLevel);
                cmd.Parameters.AddWithValue("@descrip", descrip);
                cmd.Parameters.AddWithValue("@costprice", costprice);
                cmd.Parameters.AddWithValue("@saleprice", saleprice);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public static bool UpdateMedication(string id, string name, string schedLevel, string descrip, int costprice, int saleprice, int quantity)
        {
            // Function to update a medication item in the database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Medication SET medName = @name, scheduleLevel = @schedlevel, description = @descrip, costPrice = @costprice, salePrice = @saleprice, quantityInStock = @quantity WHERE medID = @id";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@schedlevel", schedLevel);
                cmd.Parameters.AddWithValue("@descrip", descrip);
                cmd.Parameters.AddWithValue("@costprice", costprice);
                cmd.Parameters.AddWithValue("@saleprice", saleprice);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public static bool DeleteMedication(string id)
        {
            // Function to delete a medication item from the database.

            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Medication WHERE medID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
    }
}
