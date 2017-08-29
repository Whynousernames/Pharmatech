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

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class ReportsMainWindow : Window
    {
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        DataTable dt = new DataTable();
        StringBuilder sqlBuilder = new StringBuilder(500);
        List<SqlParameter> cParameters = new List<SqlParameter>();

         // <add name = "connstring" providerName="System.Data.sqlclient" connectionString="Data Source = .; Initial Catalog = PharmaTech; User ID = THC; Password = password; Integrated Security = False"/>
        // <add name = "connstring" providerName="System.Data.sqlclient" connectionString="Data Source = (local); database = PharmaTech; Integrated Security = True"/>


        public ReportsMainWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            comboBox_selectSaleType.Items.Add("All Sales");
            comboBox_selectSaleType.Items.Add("Cash");
            comboBox_selectSaleType.Items.Add("Medical Aid");
            comboBox_selectSaleType.Items.Add("Card");


            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT MedID, MedName FROM Medication", con);
                    con.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_select_Item.Items.Add(sqlReader["MedName"].ToString());

                    }
                    sqlReader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
            }
            FillSalesGrid();
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


        private void FillSalesGrid( )
        {
                          
            using (SqlConnection con = new SqlConnection(conn))
            {              
                sqlBuilder.Append("SELECT FORMAT(date, 'd', 'en-gb') AS Date, saleID AS [Invoice ID], Patient.firstName + ' ' + Patient.lastName AS [Name], description AS Description, doctorName AS [DoctorName], saleType AS [Type of Sale], FORMAT(saleAmount, 'C', 'en-ZA') AS [Amount] FROM Sale LEFT JOIN Patient ON Sale.patientIDNumber = Patient.patientIDNumber WHERE 1=1");

                if (!string.IsNullOrEmpty(comboBox_selectSaleType.Text))
                {
                    
                    try
                    {
                        sqlBuilder.Append(" AND saleType = @saleType");
                        cParameters.Add(new SqlParameter("@saleType", comboBox_selectSaleType.SelectedItem.ToString()));

                    }
                    catch
                    {
                        MessageBox.Show("no results");
                    }

                if (comboBox_selectSaleType.Text == "All Sales")
                    {
                        sqlBuilder.Remove(sqlBuilder.Length-25, 25);
                       
                    
                    }
                }

                if(!string.IsNullOrEmpty(datePicker_StartDate.Text) || !string.IsNullOrEmpty(datePicker_EndDate.Text))
                {
                    //nested if statement to check if the datePickers are empty
                    //this allows all ranges of results to be queried from the report

                    if(!string.IsNullOrEmpty(datePicker_StartDate.Text) && string.IsNullOrEmpty(datePicker_EndDate.Text))
                    {
                        sqlBuilder.Append(" AND date > @startDate");
                        cParameters.Add(new SqlParameter("@startDate", datePicker_StartDate.Text));
                        
                    }
                    else if (string.IsNullOrEmpty(datePicker_StartDate.Text) && !string.IsNullOrEmpty(datePicker_EndDate.Text))
                    {
                        sqlBuilder.Append(" AND date < @endDate");
                        
                        cParameters.Add(new SqlParameter("@endDate", datePicker_EndDate.Text));
                    }
                    else
                    {
                        sqlBuilder.Append(" AND date BETWEEN @startDate AND @endDate");
                        cParameters.Add(new SqlParameter("@startDate", datePicker_StartDate.Text));
                        cParameters.Add(new SqlParameter("@endDate", datePicker_EndDate.Text));
                    }
                }
                if(!string.IsNullOrEmpty(comboBox_select_Item.Text))
                {
                    sqlBuilder.Append(" AND Description LIKE @medName + '%'");
                    cParameters.Add(new SqlParameter("@medName", comboBox_select_Item.SelectedItem.ToString()));
                }
                if (!string.IsNullOrEmpty(textBox_PatientIDSelect.Text))
                {
                    sqlBuilder.Append(" AND Sale.patientIDNumber = @patientID");
                    cParameters.Add(new SqlParameter("@patientID", textBox_PatientIDSelect.Text));
                }

                sqlBuilder.Append(" ORDER BY Sale.date");
                
                SqlCommand cmd = new SqlCommand(sqlBuilder.ToString(), con);
                if (cParameters.Count != 0)
                {
                    cmd.Parameters.AddRange(cParameters.ToArray());
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable("Sale");
                da.Fill(dt);
               // totalSales(dt);
                sqlBuilder.Clear();
                cParameters.Clear();
                dataGrid_Reports.ItemsSource = dt.DefaultView;
                
            }                                                   
        }


        //// Function to remove the time portion of the Date/time value. 
        //private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    if (e.PropertyType == typeof(System.DateTime))
        //        (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";

        //}


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
            Grid_SelectMedication.Visibility = Visibility.Hidden;
            Grid_SelectPatientID.Visibility = Visibility.Hidden;
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
            Grid_SelectMedication.Visibility = Visibility.Visible;
            Grid_ReportsMainWindow.Visibility = Visibility.Hidden;


        }

        private void button_filterByPatientID_Click(object sender, RoutedEventArgs e)
        {
            Grid_SelectPatientID.Visibility = Visibility.Visible;
            Grid_ReportsMainWindow.Visibility = Visibility.Hidden;
        }

        private void button_saveToPDF_Click(object sender, RoutedEventArgs e)
        {
            string startDate = "";
            string endDate = "";
            string saleType = comboBox_selectSaleType.Text;
            string medName = comboBox_select_Item.Text;
            string patientID = textBox_PatientIDSelect.Text;
            string Header = "sales report";

            if (string.IsNullOrEmpty(datePicker_StartDate.Text))
            {

            }
            else
            {
                startDate = datePicker_StartDate.Text;
            }

            if (string.IsNullOrEmpty(datePicker_EndDate.Text))
            {
               // endDate = "N/A";
            }
            else
            {
                endDate = datePicker_EndDate.Text;
            }
            if (string.IsNullOrEmpty(medName))
            {
                medName = "N/A";
            }
            if (!string.IsNullOrEmpty(patientID))
            {
              //  patientID = "n/a";
            }

           

            if (dataGrid_Reports.HasItems && !string.IsNullOrEmpty(startDate))
            {
                SalesReportExporting.ExportDataTableToPdf(dt, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SalesReport", Header, saleType, startDate, endDate, medName, patientID);
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
            FillSalesGrid();
        }

        private void button_nextSelectMedication_Click_1(object sender, RoutedEventArgs e)
        {
            FillSalesGrid();
            Grid_SelectMedication.Visibility = Visibility.Hidden;
            Grid_ReportsMainWindow.Visibility = Visibility.Visible;
            comboBox_select_Item.SelectedIndex = -1;
        }

        private void button_nextSelectPatientID_Click_1(object sender, RoutedEventArgs e)
        {
            string idNumber = textBox_PatientIDSelect.Text;

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT * FROM Sale WHERE PatientIDNumber = @id";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.AddWithValue("@id", idNumber);
                SqlDataReader reader = cmd.ExecuteReader();

                // Check to see if the input Patient ID is present in the database.
                if (reader.HasRows)
                {
                    FillSalesGrid();
                    Grid_SelectPatientID.Visibility = Visibility.Hidden;
                    Grid_ReportsMainWindow.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("The patient ID number entered is not currently on the system.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                con.Close();
            }            
        }

        //private void totalSales(DataTable dt)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        // Function to total up sales amount at end of report.
        //        object saleTotal = 0;
        //        double vat = 0.14;
        //        double totalwithvat = 0;
        //        int grandtotal1 = 0;
        //        DataRow rowbreak = dt.NewRow();
        //        dt.Rows.Add(rowbreak);
        //        DataRow dr = dt.NewRow();


        //        dt.Rows.Add(dr);
        //        DataRow vatrow = dt.NewRow();
        //        grandtotal1 = Convert.ToInt32(dt.Compute("Sum(SaleAmount)", string.Empty));
        //        vatrow["Type of Sale"] = "VAT 14%:";
        //        totalwithvat = grandtotal1 * vat;
        //        vatrow["SaleAmount"] = totalwithvat;
        //        dt.Rows.Add(vatrow);


        //        DataRow grandtotal = dt.NewRow();
        //        grandtotal["Type of Sale"] = "GRAND TOTAL: ";
        //        grandtotal["SaleAmount"] = grandtotal1;
        //        dt.Rows.Add(grandtotal);
        //    }
        //}


    }

    


}

