using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess
{
    public class EmployeeDA
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public static bool AddEmployee(string fname, string sname, string contactNo, string email, string address1, string address2, string username, string password, char emptype)
        {
            // Add new employee account to database
    
            SqlConnection con = new SqlConnection(connection.ToString());
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee (firstName, lastName, contactNumber, email, physicalAddressLine1, physicalAddressLine2, username, password, employeeType) VALUES (@fname, @sname, @contactNo, @email, @address1, @address2, @username, @password, @emptype)", con))
            {
                Guid userGuid = System.Guid.NewGuid();
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@sname", sname);
                cmd.Parameters.AddWithValue("@contactNo", contactNo);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address1", address1);
                cmd.Parameters.AddWithValue("@address2", address2);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", Security.HashSHA1(password));
                cmd.Parameters.AddWithValue("@emptype", emptype);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        public static bool ChangeEmployeePassword(string username, string password)
        {
            SqlConnection con = new SqlConnection(connection);
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "UPDATE Employee SET password = @password WHERE username = @username";           
                cmd.Parameters.AddWithValue("@password", Security.HashSHA1(password));
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

            return true;

        }

        public static int AuthenticateLogin(string username, string password)
        {
            int empID = 0;
            SqlConnection con = new SqlConnection(connection.ToString());

            using (SqlCommand cmd = new SqlCommand("SELECT empIDNumber, username, password FROM [Employee] WHERE username=@username", con))
            {
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {     
                    int dbEmpID = Convert.ToInt32(dr["empIDNumber"]);
                    string dbPassword = Convert.ToString(dr["password"]);
                    string hashedPassword = Security.HashSHA1(password);

                    if (dbPassword == hashedPassword)
                    {
                        empID = dbEmpID;
                    }
                }
                con.Close();
            }
            return empID;
        }
    }
}
