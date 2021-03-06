﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using DataAccess;
using BusinessLogic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();

        public MainWindow()
        {
            InitializeComponent();
            textBox_Username.Focus();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            // Log user (employee) into system. Throw error if username/password incorrect, proceed to main system window if correct.

            var username = textBox_Username.Text;
            var password = passwordBox_password.Password.ToString();
            string empIDNumber = "";
            string empFName = "";
            string empLName = "";
            string empType = "";
            string startTime = DateTime.Now.ToString();
            
            int empID = EmployeeDA.AuthenticateLogin(username, password);
            if (empID > 0)
            {
                
                
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT empIDNumber, firstName, lastName, employeeType FROM Employee WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        
                        empIDNumber = Convert.ToString(reader["empIDNumber"]);
                        empFName = Convert.ToString(reader["firstName"]);
                        empLName = Convert.ToString(reader["lastName"]);

                        if (reader["employeeType"].ToString() == "A")
                            empType = "A";
                        if (reader["employeeType"].ToString() == "P")
                            empType = "P";

                        


                    }
                    using (StreamWriter writeText = new StreamWriter("emp.txt"))
                    {
                        writeText.WriteLine(empIDNumber.ToString());
                        writeText.WriteLine(empFName.ToString());
                        writeText.WriteLine(empLName.ToString());
                        writeText.WriteLine(empType.ToString());
                        writeText.WriteLine(startTime.ToString());

                    }

                    reader.Close();
                    con.Close();
                }
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.Show();
                this.Close();
            }
            else
            {                
                MessageBox.Show("Invalid username and/or password. Please try again.", "Incorrect login!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                textBox_Username.Focus();
            }
            
            
                           
        }

        

       
    }
}
