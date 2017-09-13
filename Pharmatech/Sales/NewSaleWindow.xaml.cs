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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BusinessObject;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    /// 
    public partial class NewSaleWindow : Window
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;
        StringBuilder sqlBuilder = new StringBuilder(500);
        StringBuilder sqlBuilderAllergy = new StringBuilder(500);
        StringBuilder sqlBuilderPatient = new StringBuilder(500);
        List<SqlParameter> cParameters = new List<SqlParameter>();
        List<SqlParameter> pParameters = new List<SqlParameter>();
        
        
        DataTable dt = new DataTable();

        int sum = 0;
        
        public List<Sale> products = new List<Sale>();
        public List<Patient> patientDetails = new List<Patient>();
        int _count = 0;


        


        public NewSaleWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            label_SaleWindowType.Content = "New Sale";
            Grid_Sale.Visibility = Visibility.Hidden;
            Grid_sales.Visibility = Visibility.Hidden;
            Grid_patientSelect.Visibility = Visibility.Visible;
            arrowHidden_True();



            comboBox_Quantity.Items.Add("1");
            comboBox_Quantity.Items.Add("2");
            comboBox_Quantity.Items.Add("3");
            comboBox_Quantity.Items.Add("4");
            comboBox_Quantity.Items.Add("5");
            comboBox_Quantity.Items.Add("15");
            comboBox_Quantity.SelectedIndex = 1;




            // Populate combobox with medicine pulled from the database.            
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    
                    SqlCommand sqlCmd = new SqlCommand("SELECT MedID, MedName FROM Medication ORDER BY MedName", conn);
                    conn.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {


                        comboBox_select_Item.Items.Add(new { MedName = sqlReader["MedName"].ToString(), MedID = sqlReader["MedID"].ToString() });
                        //comboBox_select_Item.Items.Add(sqlReader["MedID"].ToString());
                        //comboBox_select_Item.SelectedValuePath = sqlReader["MedID"].ToString();

                    }
                    sqlReader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
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
                image_arrow_Copy.Visibility = Visibility.Visible;
                image_arrow_Copy1.Visibility = Visibility.Visible;
                image_arrow_Copy2.Visibility = Visibility.Visible;

                MessageBoxResult result = MessageBox.Show("Patient Details are displayed" + Environment.NewLine + "Select items to add to the sale."
                + Environment.NewLine + "Selected medication item details are displayed" + Environment.NewLine + "Click 'Next Button' to proceed to final sale window", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
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

        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_Sale.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            Grid_saleTypeSelect.Visibility = Visibility.Hidden;
            Grid_AddInstruction.Visibility = Visibility.Hidden;

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

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            Grid_patientSelect.Visibility = Visibility.Hidden;
            Grid_Sale.Visibility = Visibility.Visible;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_cashSaleSelect_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;
            SaleFinalWindow saleFinalWindow = new SaleFinalWindow();
            saleFinalWindow.dataGrid_saleItems.ItemsSource = dt.DefaultView;
            saleFinalWindow.label_displayName.Content = this.label_DisplayPatientName.Content.ToString();
            saleFinalWindow.label_displayID.Content = this.label_DisplayPatientID.Content.ToString();
            saleFinalWindow.button_CardSale.Visibility = Visibility.Visible;
            saleFinalWindow.button_CashSale.Visibility = Visibility.Visible;

            for (int i = 0; i < dataGrid_saleItems.Items.Count - 1; i++)
            {
                sum += (int.Parse((dataGrid_saleItems.Columns[5].GetCellContent(dataGrid_saleItems.Items[i]) as TextBlock).Text));
            }
            
            saleFinalWindow.textBox_Total.Text = sum.ToString();

            double vatDisplay;
            double subTotal = 0;          
            vatDisplay = Math.Round(((sum / 1.14) - sum) * -1 , 2);           
            subTotal = sum - vatDisplay;
            saleFinalWindow.textBox_SubTotal.Text = subTotal.ToString();
            saleFinalWindow.textBox_VatDisplay.Text = vatDisplay.ToString();

            sum = 0;
            saleFinalWindow.dt = this.dt;
            saleFinalWindow.Show();
            this.Hide();
        }

        private void button_medicalAidSaleSelect_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;
            SaleFinalWindow saleFinalWindow = new SaleFinalWindow();
            saleFinalWindow.dataGrid_saleItems.ItemsSource = dt.DefaultView;
            saleFinalWindow.label_displayName.Content = this.label_DisplayPatientName.Content.ToString();
            saleFinalWindow.label_displayID.Content = this.label_DisplayPatientID.Content.ToString();
            saleFinalWindow.button_CardSale.Visibility = Visibility.Hidden;
            saleFinalWindow.button_CashSale.Visibility = Visibility.Hidden;

            for (int i = 0; i < dataGrid_saleItems.Items.Count - 1; i++)
            {
                sum += (int.Parse((dataGrid_saleItems.Columns[5].GetCellContent(dataGrid_saleItems.Items[i]) as TextBlock).Text));
            }

            saleFinalWindow.textBox_Total.Text = sum.ToString();

            double vatDisplay;
            double subTotal = 0;
            vatDisplay = Math.Round(((sum / 1.14) - sum) * -1, 2);
            subTotal = sum - vatDisplay;
            saleFinalWindow.textBox_SubTotal.Text = subTotal.ToString();
            saleFinalWindow.textBox_VatDisplay.Text = vatDisplay.ToString();
            saleFinalWindow.label_displaySaleType.Content = "Medical Aid";

            sum = 0;
            saleFinalWindow.dt = this.dt;
            saleFinalWindow.Show();
            this.Hide();
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_saleItems.HasItems)
            {
                gridHidden_True();
                Grid_saleTypeSelect.Visibility = Visibility.Visible;

                for (int i = 0; i < dataGrid_saleItems.Items.Count - 1; i++)
                {
                    sum += (int.Parse((dataGrid_saleItems.Columns[5].GetCellContent(dataGrid_saleItems.Items[i]) as TextBlock).Text));
                }
                using (SqlConnection conn2 = new SqlConnection(connection))
                {
                    try
                    {
                        SqlCommand sqlCmd2 = new SqlCommand("SELECT amountRemaining FROM Patient_MedicalAid_Account WHERE patientIDNumber = @patientIDNumber", conn2);
                        sqlCmd2.Parameters.AddWithValue("@patientIDNumber", label_DisplayPatientID.Content.ToString());
                        conn2.Open();
                        SqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();
                        if (sqlReader2.HasRows)
                        {
                            int amountRemaining = 0;
                            while (sqlReader2.Read())
                            {
                                amountRemaining = Convert.ToInt32(sqlReader2["amountRemaining"]);

                            }

                            if (amountRemaining < sum)
                            {
                                gridHidden_True();
                                Grid_saleTypeSelect.Visibility = Visibility.Visible;
                                button_medicalAidSaleSelect.IsEnabled = false;
                                MessageBox.Show("Patient does not have enough Medical Aid funds, proceed with cash sale.", "Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                            }
                            else
                            {
                                gridHidden_True();
                                Grid_saleTypeSelect.Visibility = Visibility.Visible;
                            }
                            sqlReader2.Close();
                            conn2.Close();
                            sum = 0;
                        }

                        else
                        {
                            gridHidden_True();
                            Grid_saleTypeSelect.Visibility = Visibility.Visible;
                            button_medicalAidSaleSelect.IsEnabled = false;
                        }

                        
                    }

                    catch
                    {
                        MessageBox.Show("Database error", "Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    }
                    

                }
            }
            else
            {
                MessageBox.Show("You have not selected medication for this patient.", "Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            NewSaleWindow newSaleWindow = new NewSaleWindow();
            newSaleWindow.Show();
            this.Close();
            
        }

        private void button_AddInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_AddInstruction.Visibility = Visibility.Visible;
        }

        private void button_ProceedInstructions_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;

        }

        private void button_addItem_Click(object sender, RoutedEventArgs e)
        {

            
            if (!string.IsNullOrEmpty(comboBox_select_Item.Text.ToString()))
            {

                using (SqlConnection conn = new SqlConnection(connection))
                {
                    try
                    {
                        
                        SqlCommand sqlCmd2 = new SqlCommand("SELECT allergyName, allergyDescription FROM Allergies A INNER JOIN PatientAllergies PA ON A.allergyID = PA.allergyID WHERE PA.allergyID = (SELECT allergyID FROM Medication_Allergies MA WHERE MA.medID = @medID) AND PA.patientIDNumber = @patientIDNumber ", conn);
                        sqlCmd2.Parameters.AddWithValue("@patientIDNumber", label_DisplayPatientID.Content);
                        sqlCmd2.Parameters.AddWithValue("@medID", comboBox_select_Item.SelectedValue);
                        conn.Open();
                        Allergies allergies = new Allergies();
                        SqlDataReader sqlReader = sqlCmd2.ExecuteReader();                        

                        
                            while (sqlReader.Read())
                            {
                                allergies.allergyName = Convert.ToString(sqlReader["allergyName"]);
                                allergies.allergyDescription = Convert.ToString(sqlReader["allergyDescription"]);
                           
                            

                        }

                            if(!string.IsNullOrEmpty(allergies.allergyName))
                        {

                            MessageBoxResult dialogResult = System.Windows.MessageBox.Show(label_DisplayPatientName.Content.ToString() +" "+"is allergic to" +" "+allergies.allergyName+ " " +"which is in " + comboBox_select_Item.Text.ToString()+" would you still like to add this medication to the sale?" , "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (dialogResult == MessageBoxResult.Yes)
                            {
                                FillSalesItemGrid();
                            }
                            else
                            {
                                comboBox_select_Item.SelectedIndex = 0;
                                comboBox_Quantity.SelectedIndex = 0;
                            }
                        }
                            else
                        {
                            FillSalesItemGrid();
                        }

                        
                        sqlReader.Close();
                        conn.Close();                  
                        


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), ex.ToString());
                    }
                }




            }
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

        private void FillSalesItemGrid()
        {

            List<string> product = new List<string>();

            using (SqlConnection con = new SqlConnection(conn))
            {
                dt.Clear();                
                dt.AcceptChanges();
                con.Open();
                
                

                if (!string.IsNullOrEmpty(comboBox_select_Item.Text.ToString()))
                {
                    sqlBuilder.Append("SELECT medName, scheduleLevel, description, salePrice, quantityInStock FROM Medication WHERE MedName = @medName");
                    cParameters.Add(new SqlParameter("@medName", comboBox_select_Item.Text.ToString()));
                }

                SqlCommand cmd = new SqlCommand(sqlBuilder.ToString(), con);
                if (cParameters.Count != 0)
                {
                    cmd.Parameters.AddRange(cParameters.ToArray());
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader reader = cmd.ExecuteReader();

                Sale sale = new Sale();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                      //  sale.medID = (Convert.ToInt32(reader["medID"]));
                        sale.medName = (Convert.ToString(reader["medName"]));
                        sale.scheduleLevel = (Convert.ToString(reader["scheduleLevel"]));
                        sale.description = (Convert.ToString(reader["description"]));
                        sale.salePrice = (Convert.ToInt32(reader["salePrice"]));
                        sale.quantity = Convert.ToInt32(comboBox_Quantity.Text.ToString());
                        sale.quantityInStock = Convert.ToInt32(reader["quantityInStock"]);

                        if (sale.quantityInStock > sale.quantity)
                        {
                            products.Add(sale);
                        }
                        else
                        {
                            MessageBox.Show("Not enough " + sale.medName + " in stock. Only "+sale.quantityInStock.ToString() + "available.","Warning");
                            
                        }
                        
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                if (_count == 0)
                {
                   // dt.Columns.Add("Medication ID");
                    dt.Columns.Add("Medication name");
                    dt.Columns.Add("Schedule");
                    dt.Columns.Add("Description");
                    dt.Columns.Add("Sale Price");
                    dt.Columns.Add("Quantity");
                    dt.Columns.Add("Total Price");
                }

                
                
                foreach (var item in products)
                {
                    var row = dt.NewRow();
                 //   row["Medication ID"] = item.medID;
                    row["Medication name"] = item.medName;
                    row["Schedule"] = item.scheduleLevel;
                    row["Description"] = item.description;
                    row["Sale Price"] = item.salePrice;
                    row["Quantity"] = item.quantity;
                    row["Total Price"] = item.salePrice * item.quantity;
                    dt.Rows.Add(row);
                    //dt.Rows.InsertAt(row, _count);

                }
                reader.Close();
              
                dataGrid_saleItems.AutoGenerateColumns = true;
                // Finally bind the datasource to datagridview.
                //dataGrid_saleItems.ItemsSource = dt.DefaultView;             
                dataGrid_saleItems.ItemsSource = this.dt.AsDataView();

                _count++;
                sqlBuilder.Clear();
                cParameters.Clear();
                product.Clear();
                dt.AcceptChanges();

            }
        }

        private void dataGrid_saleItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_removeItem_Click(object sender, RoutedEventArgs e)
        {
            while (dataGrid_saleItems.SelectedItems.Count >= 1)
            {
                DataRowView drv = (DataRowView)dataGrid_saleItems.SelectedItem;
                drv.Row.Delete();


                dt.AcceptChanges();
                dataGrid_saleItems.ItemsSource = dt.DefaultView;






            }

        }

        private void button_ProceedToSale_Click(object sender, RoutedEventArgs e)
        {
                  
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                if (!string.IsNullOrEmpty(textBox_patientID.Text))
                {
                    sqlBuilderPatient.Append("SELECT patientIDNumber, firstName, lastName, contactNumber, contactNumber, email, physicalAddress1, physicalAddress2 FROM Patient WHERE patientIDNumber = @patientIDNum");
                    pParameters.Add(new SqlParameter("patientIDNum", textBox_patientID.Text));
                }

                SqlCommand cmdPatient = new SqlCommand(sqlBuilderPatient.ToString(), con);
                if (pParameters.Count != 0)
                {
                    cmdPatient.Parameters.AddRange(pParameters.ToArray());
                }

                SqlDataAdapter da = new SqlDataAdapter(cmdPatient);
                SqlDataReader reader = cmdPatient.ExecuteReader();

                Patient patient = new Patient();

                //add sql query to the class Patient

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        patient.patientIDnumber = (Convert.ToString(reader["patientIDNumber"]));
                        patient.fName = (Convert.ToString(reader["firstName"]));
                        patient.lName = (Convert.ToString(reader["lastName"]));
                        patient.contactNumber = (Convert.ToString(reader["contactNumber"]));
                        patient.email = (Convert.ToString(reader["email"]));
                        patient.address1 = (Convert.ToString(reader["physicalAddress1"]));
                        patient.address2 = (Convert.ToString(reader["physicalAddress2"]));
                        patientDetails.Add(patient);
                        
                    }

                    foreach (var details in patientDetails)
                    {
                        label_DisplayPatientName.Content = details.fName + " " + details.lName;
                        label_DisplayPatientID.Content = details.patientIDnumber;
                    }
                    Grid_Sale.Visibility = Visibility.Visible;
                    Grid_patientSelect.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("The patient ID number entered is not currently on the system.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    NewSaleWindow newSaleWindow = new NewSaleWindow();
                    newSaleWindow.Show();
                    this.Close();
                {
                        
                    }

                }

                con.Close();

            }
            
            
        }


        
    }

}




