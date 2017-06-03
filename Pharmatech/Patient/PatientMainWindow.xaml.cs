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

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {

        
        


        public PatientMainWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();

            


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
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            Grid_MedicalAidSelect.Visibility = Visibility.Hidden;
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            
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

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            Grid_PatientMain.Visibility = Visibility.Hidden;
            Grid_YesNoSelect.Visibility = Visibility.Visible;


            //string firstName = textBox_FirstName.Text;
            //string surname = textBox_Surname.Text;
            //string contactNo = textBox_ContactNumber.Text;
            //string email = textBox_Email.Text;
            //string address1 = textBox_AddressLine1.Text;
            //string address2 = textBox_AddressLine2.Text;


            if (textBox_FirstName.Text.Length < 2)
            {
                string firstName = "First Name";
            }

            else if (textBox_Surname.Text.Length < 2)
            {

            }

                
        



        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            if (label_PatientWindowType.Content.ToString() == "Update Patient"|| label_PatientWindowType.Content.ToString() == "Update Patient"|| label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                Grid_PatientMain.Visibility = Visibility.Hidden;
                Grid_SelectPatient.Visibility = Visibility.Visible;
            }
           
            else
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.ShowDialog();
                this.Close();
            }
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            
        }

       
        private void button_YesSelect_Click(object sender, RoutedEventArgs e)
        {
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            Grid_MedicalAidSelect.Visibility = Visibility.Visible;
        }

        private void button_NoSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_nextSelectPatient_Click(object sender, RoutedEventArgs e)
        {
            Grid_SelectPatient.Visibility = Visibility.Hidden;
            Grid_PatientMain.Visibility = Visibility.Visible;
        }

       

        

        private void button_AddPatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();            
            patientMainWindow.label_PatientWindowType.Content = "Add Patient";
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Hidden;
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_updatePatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Update Patient";
            patientMainWindow.ShowDialog();
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

        private void button_removePatient_Click_1(object sender, RoutedEventArgs e)
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

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();
            this.Hide();
            reportsMainWindow.ShowDialog();
        }
    }

}

