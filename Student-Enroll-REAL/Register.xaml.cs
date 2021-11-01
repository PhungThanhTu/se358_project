using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Student_Enroll_REAL.SQLInteraction;
using System.Globalization;
using System.Threading;
using Student_Enroll_REAL.Authentication;

namespace Student_Enroll_REAL
{
    
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        DataRowView rowView;
        int sophieumoi = -1;


        public Register()
        {   
            InitializeComponent();
            FillGrid();
        }
        
        public void FillGrid()
        {

            //using (CheckLogin.Connection)

            //{
            //    CheckLogin.Connection.Open();
            //    string CmdString = "select so_phieu as 'Số phiếu',ho_va_ten as 'Họ và tên',ngay_sinh as 'Ngày sinh',khu_vuc as 'Khu vực',nam_tot_nghiep_th as 'Năm tốt nghiệp',he_th as 'Hệ trung học',dia_chi_bao_tin as 'Địa chỉ báo tin',noi_sinh as 'Nơi sinh' ,ho_khau as 'Hộ khẩu' ,dang_ky_thi as 'Đăng kí thi',ma_khoi as 'Mã khối',ten_nganh as 'Tên ngành',ten_truong as 'Tên trường' from PHIEU_DKDT,TRUONG,NGANH where PHIEU_DKDT.ma_truong = TRUONG.ma_truong and PHIEU_DKDT.ma_nganh = NGANH.ma_nganh";


            //    SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);




            //    DataTable dt = new DataTable("PHIEU_DKDT");


            //    sda.Fill(dt);

            //    RegisterGrid.ItemsSource = dt.DefaultView;



            //    CheckLogin.Connection.Close();

            //}
            UpdateGrid();
        }

        // check if admin has close registration phase, if registration closed, cannot add, edit or delete
        bool checkRegisterFinished()
        {
            string isFinished = SQLInteraction.ExecuteQuery.GetStringFromQuery("select trang_thai from TRANG_THAI_KI_THI");
            if (isFinished == "0")
                return false;
            else return true;
        }
        public void UpdateGrid()
        {
            //using (CheckLogin.Connection)

            //{
            //    CheckLogin.ConnectToSQL();
            //    CheckLogin.Connection.Open();
            //    string CmdString = "select so_phieu as 'Số phiếu',ho_va_ten as 'Họ và tên',ngay_sinh as 'Ngày sinh',khu_vuc as 'Khu vực',nam_tot_nghiep_th as 'Năm tốt nghiệp',he_th as 'Hệ trung học',dia_chi_bao_tin as 'Địa chỉ báo tin',noi_sinh as 'Nơi sinh' ,ho_khau as 'Hộ khẩu' ,dang_ky_thi as 'Đăng kí thi',ma_khoi as 'Mã khối',ten_nganh as 'Tên ngành',ten_truong as 'Tên trường' from PHIEU_DKDT,TRUONG,NGANH where PHIEU_DKDT.ma_truong = TRUONG.ma_truong and PHIEU_DKDT.ma_nganh = NGANH.ma_nganh";


            //    SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);




            //    DataTable dt = new DataTable("PHIEU_DKDT");

            //    sda.Fill(dt);

            //    RegisterGrid.ItemsSource = dt.DefaultView;

            //    CheckLogin.Connection.Close();

            //}
            DataTable gridDatabase = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_phieu as 'Số phiếu',ho_va_ten as 'Họ và tên',ngay_sinh as 'Ngày sinh',khu_vuc as 'Khu vực',nam_tot_nghiep_th as 'Năm tốt nghiệp',he_th as 'Hệ trung học',dia_chi_bao_tin as 'Địa chỉ báo tin',noi_sinh as 'Nơi sinh' ,ho_khau as 'Hộ khẩu' ,dang_ky_thi as 'Đăng kí thi',ma_khoi as 'Mã khối',ten_nganh as 'Tên ngành',ten_truong as 'Tên trường' from PHIEU_DKDT,TRUONG,NGANH where PHIEU_DKDT.ma_truong = TRUONG.ma_truong and PHIEU_DKDT.ma_nganh = NGANH.ma_nganh", "PHIEU_DKDT");
            RegisterGrid.ItemsSource = gridDatabase.DefaultView;
            Search();
        }

        public void ClearGrid()
        {
           
        }

    

        void LogOut()
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

       


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRegisterFinished())
            {

                if (rowView == null)
                {
                    MessageBox.Show("Nothing to edit");
                    return;
                }
                RegisterEditing editForm = new RegisterEditing((int)rowView[0], 0);
                editForm.uptdelegate = UpdateGrid;
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Registering has finished, cannot modify this database, please re-login");
                LogOut();
            }
           
        }

        private void DataGridRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow selectedRow = (DataGridRow)sender;

            selectedRow.IsSelected = true;
            rowView = RegisterGrid.SelectedItem as DataRowView;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRegisterFinished())
            {
                #region tim so phieu moi

                string CmdString = "select max(so_phieu) as phieumoi from PHIEU_DKDT";

                CheckLogin.ConnectToSQL();

                SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("PHIEU_DKDT");

                sda.Fill(dt);

                if (dt.Rows[0]["phieumoi"] is System.DBNull) sophieumoi = 1;
                else sophieumoi = (int)dt.Rows[0]["phieumoi"] + 1;

                #endregion

                RegisterEditing editForm = new RegisterEditing(sophieumoi, 1);
                editForm.uptdelegate = UpdateGrid;
                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Registering has finished, cannot modify this database, please re-login");
                LogOut();
            }

        }

        void DeleteSelectedRow()
        {
            try
            {
                string deletequery = "DELETE FROM PHIEU_DKDT WHERE so_phieu = " + rowView[0].ToString();
                ExecuteQuery.executeQuery(deletequery);
            }
            catch
            {
                MessageBox.Show("Finished");
            }
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRegisterFinished())
            {
                if (rowView == null)
                {
                    MessageBox.Show("Nothing to delete");
                    return;
                }
                MessageBoxResult Result = MessageBox.Show("ARE YOU SURE ABOUT THIS ?", "REALLY ?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.Yes)
                {
                    // delete
                    DeleteSelectedRow();
                    UpdateGrid();
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Registering has finished, cannot modify this database, please re-login");
                LogOut();
            }
           
                
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        private void RegisterGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string searchText = TxtSearch.Text;
            DataTable searchresult = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_phieu as 'Serial Number',ho_va_ten as 'Name',ngay_sinh as 'Day of birth',khu_vuc as 'Area',nam_tot_nghiep_th as 'Graduation year',he_th as 'High school certificate',dia_chi_bao_tin as 'Address',noi_sinh as 'Birthplace' ,ho_khau as 'Household' ,dang_ky_thi as 'Register for',ma_khoi as 'Grade ID',ten_nganh as 'Major',ten_truong as 'University' from PHIEU_DKDT,TRUONG,NGANH where PHIEU_DKDT.ma_truong = TRUONG.ma_truong and PHIEU_DKDT.ma_nganh = NGANH.ma_nganh and ho_va_ten LIKE N'%" + searchText + "%'", "PHIEU_DKDT");

            RegisterGrid.ItemsSource = searchresult.DefaultView;
        }


        private void FinishRegistering_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRegisterFinished())
            {
                if (User.Login_status == "user")
                {
                    MessageBox.Show("You must be admin to use this feature.", "Admin permission required");
                    return;
                }
                if (User.Login_status == "admin")
                {
                    if (DPNgayThi.SelectedDate == null)
                    {
                        MessageBox.Show("Please pick an exam day first");
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("This cannot be undone, are you sure to close registering phase?", "REGISTERING PHASE CLOSING", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            //do no stuff
                            return;
                        }
                        else
                        {
                            try
                            {
                                //do yes stuff
                                DateTime NgayThi = new DateTime();
                                NgayThi = (DateTime)DPNgayThi.SelectedDate;
                                string strNgayThi = NgayThi.ToString("yyyy-MM-dd");
                                SQLInteraction.ExecuteQuery.executeQuery("TRUNCATE TABLE ngay_thi" + "\n" +
                                                                          "INSERT INTO ngay_thi values ('" + strNgayThi + "')");
                                SQLInteraction.ExecuteQuery.executeQuery("EXEC insertThiSinh\n" +
                                                                          "update TRANG_THAI_KI_THI set trang_thai = 1");

                                CandidateList window = new CandidateList();
                                window.Show();
                                this.Close();

                            }
                            catch
                            {
                                CandidateList window = new CandidateList();
                                window.Show();
                                this.Close();
                            }
                          
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Registering has finished, cannot modify this database, please re-login");
                LogOut();
            }
         
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }
    }
}

