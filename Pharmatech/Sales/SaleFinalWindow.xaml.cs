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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessObject;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class SaleFinalWindow : Window
    {
        public double change;
        public DataTable dt;
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;



        public SaleFinalWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            label_WindowType.Content = "New Cash Sale";
            Grid_Sale.Visibility = Visibility.Visible;
            Grid_sales.Visibility = Visibility.Hidden;
            arrowHidden_True();


            

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

                if (button_CashSale.Visibility == Visibility.Visible)
                {
                    image_arrow.Visibility = Visibility.Visible;
                    image_arrow_Copy.Visibility = Visibility.Visible;
                    image_arrow_Copy1.Visibility = Visibility.Visible;
                    image_arrow_Copy2.Visibility = Visibility.Visible;
                    MessageBoxResult result = MessageBox.Show("Patient Details are displayed." + Environment.NewLine + "Sale details are displayed with total."
                     + Environment.NewLine + "Input tendered amount to display change." + Environment.NewLine + "Select weather sale type is cash or card."
                     + Environment.NewLine + "Click 'Proceed Button' to finalize the sale.", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
                }
                else
                {
                    image_arrow.Visibility = Visibility.Visible;
                    image_arrow_Copy.Visibility = Visibility.Visible;
                    image_arrow_Copy1.Visibility = Visibility.Visible;
                    image_arrow_Copy2.Visibility = Visibility.Visible;
                    MessageBoxResult result = MessageBox.Show("Patient Details are displayed." + Environment.NewLine + "Sale details are displayed with total."
                     + Environment.NewLine + "Input tendered amount to display change." + Environment.NewLine + "Click 'Proceed Button' to finalize the sale.", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
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

        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_Sale.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            
            
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

        

       

        private void button_proceed_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            
            NewSaleWindow newSaleWindow = new NewSaleWindow();
            newSaleWindow.Show();
            this.Hide();
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();
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
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removeMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();
            this.Hide();
            reportsMainWindow.ShowDialog();
        }

        private void button_CashSale_Click(object sender, RoutedEventArgs e)
        {
            label_displaySaleType.Content = "Cash Sale";
        }

        private void button_CardSale_Click(object sender, RoutedEventArgs e)
        {
            label_displaySaleType.Content = "Card Sale";
        }

        private void textBox_amountRecieved_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox_amountRecieved.Text != "")
            {
                
                change = 0;
                change = (int.Parse(textBox_amountRecieved.Text)) - (int.Parse(textBox_Total.Text));
                textBox_Change.Text = change.ToString();
            }
            else
            {
                textBox_Change.Text = "";
            }
        }

        private void button_completeSale_Click(object sender, RoutedEventArgs e)
        {
            double subTotal = Convert.ToDouble(textBox_SubTotal.Text);
            double vatAmount = Convert.ToDouble(textBox_VatDisplay.Text);

            if (Convert.ToDouble(textBox_Change.Text) >= 0)
            {

                if (label_displaySaleType.Content.ToString() != "Select sale type")
                {
                    MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to finalize the sale?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        string saleType;
                        string descrip = "";
                        MainMenuWindow window = new MainMenuWindow();
                        //string empID = window.label_ID.ToString();
                        string empID = "4200";
                        string patientID = label_displayID.Content.ToString();
                        StringBuilder strBuilder = new StringBuilder();
                        string patientName = label_displayName.Content.ToString();     
                        
                        if (label_displaySaleType.Content.ToString() =="Medical Aid")
                        {
                            using (SqlConnection conn2 = new SqlConnection(connection))
                            {
                                conn2.Open();
                                using (SqlCommand cmd2 = conn2.CreateCommand())
                                {
                                    int amountRemaining = 0;
                                    try
                                    {
                                        SqlCommand sqlCmd2 = new SqlCommand("SELECT amountRemaining FROM Patient_MedicalAid_Account WHERE patientIDNumber = @patientIDNumber", conn2);
                                        sqlCmd2.Parameters.AddWithValue("@patientIDNumber", label_displayID.Content.ToString());
                                        
                                        SqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();
                                        while (sqlReader2.Read())
                                        {
                                            amountRemaining = Convert.ToInt32(sqlReader2["amountRemaining"]);
                                           
                                        }


                                        cmd2.CommandText = "UPDATE Patient_MedicalAid_Account SET amountRemaining = @amountRemaining WHERE patientIDNumber = @patientIDNumber";
                                        cmd2.Parameters.AddWithValue("@amountRemaining", amountRemaining - Convert.ToInt32(textBox_Total.Text));
                                        cmd2.Parameters.AddWithValue("@patientIDNumber", label_displayID.Content.ToString());
                                        conn2.Close();
                                        conn2.Open();
                                        cmd2.ExecuteNonQuery();
                                        sqlReader2.Close();
                                        conn2.Close();

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString(), ex.ToString());
                                    }
                                }
                            }
                        }               


                            foreach (DataRow row in dt.Rows)
                            {
                                strBuilder.Append(row["Medication name"].ToString() + "  X" + row["Quantity"] + " // ");
                                int existingStock = 0;



                                using (SqlConnection conn = new SqlConnection(connection))
                                {
                                    try
                                    {



                                        SqlConnection con = new SqlConnection(connection);
                                        using (SqlCommand cmd = con.CreateCommand())
                                        {
                                            cmd.CommandText = "UPDATE Medication SET quantityInStock = @quantity WHERE medName = @medName";

                                            SqlCommand sqlCmd = new SqlCommand("SELECT quantityInStock FROM Medication WHERE medName = @medName", conn);
                                            sqlCmd.Parameters.AddWithValue("@medName", row["Medication name"]);
                                            conn.Open();
                                            SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                                            while (sqlReader.Read())
                                            {
                                                existingStock = Convert.ToInt32(sqlReader["quantityInStock"]);

                                            }

                                            cmd.Parameters.AddWithValue("@quantity", (existingStock - Convert.ToInt32(row["Quantity"])));
                                            cmd.Parameters.AddWithValue("@medName", row["Medication name"]);
                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            sqlReader.Close();
                                            con.Close();


                                        }


                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Medication not found", ex.ToString());
                                    }
                                }

                            }

                        descrip = strBuilder.ToString();
                        string date = DateTime.Now.ToString("MM/dd/yyyy");
                        string docName = textBox_DoctorName.Text.ToString();

                        if (label_SaleType.Content.ToString() == "Cash Sale")
                        {
                            saleType = "Cash";
                        }
                        else if (label_displaySaleType.Content.ToString() == "Medical Aid")
                        {
                            saleType = "Medical Aid";

                            
                        }
                        else
                        {
                            saleType = "Card";
                        }

                        int saleAmount = Convert.ToInt32(textBox_Total.Text);




                        DataAccess.SaleDA.NewSale(empID, patientID, DateTime.Parse(date), descrip, docName, saleType, saleAmount);
                        SalesReportExporting.ExportToInvoice(dt, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SaleInvoice", "INVOICE", patientName, descrip, saleAmount, vatAmount, subTotal);

                        // Open the pdf invoice.
                        System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SaleInvoice");

                        MessageBoxResult finalResult = System.Windows.MessageBox.Show("Sale successfully processed.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                        if (finalResult == MessageBoxResult.OK)
                        {
                            MainMenuWindow mainWindow = new MainMenuWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {

                    }





                }
                else
                {
                    MessageBox.Show("Please select sale type.", "Warning.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {

                MessageBox.Show("Tendered amount is less than total.", "Warning.", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
        }
    }
    }




