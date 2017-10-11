using System;
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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using BusinessObject;
using System.Collections.ObjectModel;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class Timesheets : Window
    {
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        DataTable dt = new DataTable();
        StringBuilder sqlBuilder = new StringBuilder(500);
        List<SqlParameter> cParameters = new List<SqlParameter>();

        ObservableCollection<Sale> saleCollection = new ObservableCollection<Sale>();
        double sum;

        // <add name = "connstring" providerName="System.Data.sqlclient" connectionString="Data Source = .; Initial Catalog = PharmaTech; User ID = THC; Password = password; Integrated Security = False"/>
        // <add name = "connstring" providerName="System.Data.sqlclient" connectionString="Data Source = (local); database = PharmaTech; Integrated Security = True"/>


        public Timesheets()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            arrowHidden_True();
           


            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT empIDNumber, firstName  + ' ' + lastName AS Name FROM Employee ORDER by lastName ASC", con);
                    con.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_selectEmployee.Items.Add(new { empName = sqlReader["Name"].ToString(), empID = sqlReader["empIDNumber"].ToString() });
                        //comboBox_select_Item.Items.Add(new { MedName = sqlReader["MedName"].ToString(), MedID = sqlReader["MedID"].ToString() });

                    }
                    sqlReader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
            }

            comboBox_selectMonth.Items.Add("January");
            comboBox_selectMonth.Items.Add("February");
            comboBox_selectMonth.Items.Add("March");
            comboBox_selectMonth.Items.Add("April");
            comboBox_selectMonth.Items.Add("May");
            comboBox_selectMonth.Items.Add("June");
            comboBox_selectMonth.Items.Add("July");
            comboBox_selectMonth.Items.Add("August");
            comboBox_selectMonth.Items.Add("September");
            comboBox_selectMonth.Items.Add("October");
            comboBox_selectMonth.Items.Add("November");
            comboBox_selectMonth.Items.Add("December");



        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //The following is a method to display the "help function" on F1 keypress.
            //It displays a messagebox and arrows pointing to the relavent elements.
            //When the user clicks "OK" on the messagebox the messagebox and the arrows are closed.
            if (e.Key == Key.F1)
            {
                image_arrow.Visibility = Visibility.Visible;
                image_arrow_Copy.Visibility = Visibility.Visible;
                image_arrow_Copy1.Visibility = Visibility.Visible;
                image_arrow_Copy2.Visibility = Visibility.Visible;
                MessageBoxResult result = MessageBox.Show("Select filters to refine report display." + Environment.NewLine + "Click 'Generate Report Button' to apply filters."
                 + Environment.NewLine + "Results are displayed in the grid." + Environment.NewLine + "Click 'Save to PDF Button' to save results to PDF", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    arrowHidden_True();
                }
            }
        }

        void arrowHidden_True()
        {
            image_arrow.Visibility = Visibility.Hidden;
            image_arrow_Copy.Visibility = Visibility.Hidden;
            image_arrow_Copy1.Visibility = Visibility.Hidden;
            image_arrow_Copy2.Visibility = Visibility.Hidden;
        }


        

        void messageTimer_Tick(object sender, EventArgs e)
        {
            label_Time.Content = DateTime.Now.ToString();
        }
        
        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_sales.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            
            Grid_ViewPDF.Visibility = Visibility.Hidden;
        }

        private void button_newCashSale_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            NewSaleWindow newCashSaleWindow = new NewSaleWindow();
            newCashSaleWindow.label_SaleWindowType.Content = "New Sale";
            newCashSaleWindow.ShowDialog();
            this.Close();
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
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_updatePatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Update Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_ViewPatient_Click(object sender, RoutedEventArgs e)
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

        private void button_addMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Add Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();            
            reportsMainWindow.Show();
            this.Close();
        }

        private void button_updateMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_viewMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "View Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removeMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

       
        private void button_filterByMedicationName_Click(object sender, RoutedEventArgs e)
        {
       
            
            Grid_ReportsMainWindow.Visibility = Visibility.Hidden;


        }

        private void button_filterByPatientID_Click(object sender, RoutedEventArgs e)
        {
            
            Grid_ReportsMainWindow.Visibility = Visibility.Hidden;
        }

        private void button_saveToPDF_Click(object sender, RoutedEventArgs e)
        {
            string startDate = "";
            string endDate = "";
            string saleType = "";            
            string Header = "sales report";
            string patientName = "";


            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT * FROM Patient WHERE patientIDNumber = @id";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                
                SqlDataReader reader = cmd.ExecuteReader();

                // Check to see if the input Patient ID is present in the database.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patientName = reader["firstName"].ToString() + " " + reader["lastName"].ToString();
                    }
                }

                con.Close();
            }

           

            if (dataGrid_Timesheets.HasItems && !string.IsNullOrEmpty(startDate))
            {
                //SalesReportExporting.ExportDataTableToPdf(dt, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SalesReport", Header, saleType, startDate, endDate, patientName);
                Grid_ViewPDF.Visibility = Visibility.Visible;
                Grid_ReportsMainWindow.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Please generate a report including a starting date!", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        }
            

        private void button_No_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();
        }

        private void button_Yes_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SalesReport");
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();
        }

        private void button_GenerateReport_Click_1(object sender, RoutedEventArgs e)
        {

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT startTime AS [Start Time], endTime as [End Time], timeWorkedMinutes as [Total Time Worked] FROM TimeSheets WHERE empID = @id AND MONTH(startTime) = @month";
                
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.AddWithValue("@id", comboBox_selectEmployee.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@month", comboBox_selectMonth.SelectedIndex + 1);


                

                // Check to see if the input Patient ID is present in the database.
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable("Timesheets");
                da.Fill(dt);
                                
                dataGrid_Timesheets.ItemsSource = dt.DefaultView;

                con.Close();
            }


        }

        
        

        private void totalSales(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                // Function to total up sales amount at end of report.
                object saleTotal = 0;
                double vat = 0.14;
                double totalwithvat = 0;
                int grandtotal1 = 0;
                DataRow rowbreak = dt.NewRow();
                dt.Rows.Add(rowbreak);
                DataRow dr = dt.NewRow();


                dt.Rows.Add(dr);
                DataRow vatrow = dt.NewRow();
                grandtotal1 = Convert.ToInt32(dt.Compute("Sum(Amount)", string.Empty));
                vatrow["Sale Type"] = "VAT 14%:";
                totalwithvat = grandtotal1 * vat;
                vatrow["Amount"] =  totalwithvat;
                dt.Rows.Add(vatrow);


                DataRow grandtotal = dt.NewRow();
                grandtotal["Sale Type"] = "GRAND TOTAL:";
                grandtotal["Amount"] = grandtotal1;
                dt.Rows.Add(grandtotal);
            }
        }

        private void button_GeneratePaySlip_Click(object sender, RoutedEventArgs e)
        {
            string empName = comboBox_selectEmployee.Text;
            string month = comboBox_selectMonth.Text;
            double hourlyRate = 0;

            for (int i = 0; i < dataGrid_Timesheets.Items.Count - 1; i++)
            {
                sum += (int.Parse((dataGrid_Timesheets.Columns[2].GetCellContent(dataGrid_Timesheets.Items[i]) as TextBlock).Text));
            }
            MessageBox.Show(sum.ToString());

            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT hourlyRate FROM Employee WHERE empIDNumber = @empID", con);
                    con.Open();
                    sqlCmd.Parameters.AddWithValue("@empID", comboBox_selectEmployee.SelectedValue.ToString());
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();                

                    while (sqlReader.Read())
                    {
                        hourlyRate = Convert.ToDouble(sqlReader["hourlyRate"]);
                        
                    }
                    sqlReader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
            }


            double grossPayment =sum * (hourlyRate / 60);
            double uif = grossPayment / 100 * 5;
            double pension = grossPayment / 100 * 10;
            double netPayment = grossPayment - uif - pension;
            double taxAmount = grossPayment * 14 / 100;




            if (dataGrid_Timesheets.HasItems)
            {
                SalesReportExporting.ExportPayslip(dt, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Payslip", "payslip for: ", empName, month, Math.Round( uif,2), Math.Round(pension,2), Math.Round(taxAmount,2), Math.Round(netPayment,2), Math.Round(grossPayment,2));
                System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Payslip");


            }
        }

        private void button_addEmployee_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            EmployeeMainWindow employeeMainWindow = new EmployeeMainWindow();
            employeeMainWindow.Show();
        }

        private void button_addInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Timesheets timeSheets = new Timesheets();
            timeSheets.Show();
        }
    }
  
}

