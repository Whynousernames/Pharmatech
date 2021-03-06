﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        string empIDNumber;
        string empFName;
        string empLName;
        string empType;
        string startTime;
        string[] employeeDetails = new string[5];

        public MainMenuWindow()
        {
            InitializeComponent();

            //Creates a live system time and date display on the main menu window
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            arrowHidden_True();
            

            using (StreamReader reader = new StreamReader("emp.txt"))
            {
                while(!reader.EndOfStream)
                {
                    
                    for(var i = 0; i < 5; i++)
                    {
                        var currLine = reader.ReadLine();
                        employeeDetails[i] = currLine;
                    }
                }
            }

            empIDNumber = employeeDetails[0];
            empFName = employeeDetails[1];
            empLName = employeeDetails[2];
            empType = employeeDetails[3];
            startTime = employeeDetails[4];





            label_IDDisplay.Content = empIDNumber;
            label_FirstNameDisplay.Content = empFName.ToUpper() + " " + empLName.ToUpper();
            string name = label_FirstNameDisplay.Content.ToString();
            label_FirstNameDisplay_Copy.Content = "Welcome, " + name + ".";

            

            if (empType == "A")
            {
                label_EmployeeTypeDisplay.Content = "Administrator";
                label_login.Content = "You are currently logged in with Administrator system rights. \nYou have access to the entire PharmaTech system.";
            }
            else
            {
                label_EmployeeTypeDisplay.Content = "Pharmacist";
                label_login.Content = "You are currently logged in with Pharmacist system rights. \nYou have access to operation of a patient's account and the dispensing of medication.";
                button_Medication.IsEnabled = false;
                button_Instruction.IsEnabled = false;
                button_Reports.IsEnabled = false;
                button_Employee.IsEnabled = false;
            }
            
        }

        void messageTimer_Tick(object sender, EventArgs e)
        {
            label_Time.Content = DateTime.Now.ToString();

        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //The following is a method to display the "help function" on F1 keypress.
            //It displays a messagebox and arrows pointing to the relavent elements.
            //When the user clicks "OK" on the messagebox the messagebox and the arrows are closed.
            if (e.Key == Key.F1)
            {            
                image_arrow.Visibility = Visibility.Visible;               
                MessageBoxResult result = MessageBox.Show("Please select an item from the menu on the left to proceed." + Environment.NewLine , "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    arrowHidden_True();
                }

            }
        }

        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_sales.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
        }
        void arrowHidden_True()
        {
            image_arrow.Visibility = Visibility.Hidden;
            
        }

        private void button_newCashSale_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            NewSaleWindow newCashSaleWindow = new NewSaleWindow();
            newCashSaleWindow.label_SaleWindowType.Content = "New Sale";
            newCashSaleWindow.Show();
            this.Hide();
        }

        private void button_Sale_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_sales.Visibility = Visibility.Visible;
        }

        private void button_Patient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_patient.Visibility = Visibility.Visible;
        }

        private void button_Medication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_medication.Visibility = Visibility.Visible;
        }

        private void button_Instruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_instruction.Visibility = Visibility.Visible;
        }

        private void button_Employee_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Employee.Visibility = Visibility.Visible;
        }

        private void button_Reports_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Report.Visibility = Visibility.Visible;
        }

        private void button_AddPatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();           
            patientMainWindow.label_PatientWindowType.Content = "Add Patient";
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Hidden;            
            patientMainWindow.Show();
            this.Close();
        }

        private void button_updatePatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Update Patient";
            patientMainWindow.Show();
            this.Close();
        }

        private void button_ViewPatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "View Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removePatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Remove Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();            
            reportsMainWindow.Show();
            this.Close();
        }

        private void button_addMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Add Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_updateMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_viewMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "View Medication";
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removeMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Remove Medication";
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_Help_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Your session has been recorded. Session start time "+startTime.ToString() + " session end time " + DateTime.Now.ToString()  + Environment.NewLine, "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {

                SqlConnection con = new SqlConnection(connection);
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO TimeSheets(empID, startTime, endTime, timeWorkedMinutes) VALUES(@empID, @startTime, @endTime,@timeWorkedMinutes)";

                   

                    cmd.Parameters.AddWithValue("@empID", empIDNumber);
                    cmd.Parameters.AddWithValue("@startTime", startTime);
                    cmd.Parameters.AddWithValue("@endTime", DateTime.Now);

                    TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(startTime));

                    cmd.Parameters.AddWithValue("@timeWorkedMinutes", span.TotalMinutes);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    con.Close();

                }



                MainWindow mainwindow = new MainWindow();
                CloseAllWindows();
                mainwindow.ShowDialog();
            }

           
            
        }

        private void button_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            EmployeeMainWindow employeeMainWindow = new EmployeeMainWindow();
            employeeMainWindow.Show();

        }

        private void button_updateEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_viewEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_removeEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Hide();
        }


        //private void GoToArticle_Click(object sender, RoutedEventArgs e)
        //{
        //    string path = (sender as Hyperlink).Tag as string;
        //    System.Diagnostics.Process.Start(path);
        //}

        private void button_addInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Timesheets timeSheets = new Timesheets();
            timeSheets.Show();
        }
    }

}

