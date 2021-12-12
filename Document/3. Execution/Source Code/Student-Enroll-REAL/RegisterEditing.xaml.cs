using Student_Enroll_REAL.SQLInteraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for RegisterEditing.xaml
    /// </summary>
    public partial class RegisterEditing : Window
    {
        public delegate void UpdateGrid();

       public  UpdateGrid uptdelegate;

        

        int sophieu;
        string hovaten;
        DateTime ngaysinh;
        string khuvuc;
        int namtotnghiep;
        string heth;
        string diachibaotin;
        string noisinh;
        string hokhau;
        string dangkythi;
        string makhoi;
        int matruong;
        int manganh;
        int madoituong;

        int editMode;
        
        public RegisterEditing(int sophieu,int editMode)
        {
            InitializeComponent();
            this.sophieu = sophieu;
            this.editMode = editMode;
            if(editMode == 0) FillForm();
            else
            {
                so_phieu.Text = sophieu.ToString();
            }

        }

        void FillForm()
        {
            string CmdString = "select * from PHIEU_DKDT where so_phieu = " + sophieu.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);




            DataTable dt = new DataTable("PHIEU_DKDT");

            sda.Fill(dt);
            if(dt.Rows[0]["so_phieu"] == null)
            {
                MessageBox.Show("Nothing to edit");
                return;
            }
            sophieu = (int)dt.Rows[0]["so_phieu"];
            hovaten = dt.Rows[0]["ho_va_ten"].ToString();
            ngaysinh = (DateTime)dt.Rows[0]["ngay_sinh"];
            khuvuc = dt.Rows[0]["khu_vuc"].ToString();
            namtotnghiep = (int)dt.Rows[0]["nam_tot_nghiep_th"];
            heth = dt.Rows[0]["he_th"].ToString();
            diachibaotin = dt.Rows[0]["dia_chi_bao_tin"].ToString();
            noisinh = dt.Rows[0]["noi_sinh"].ToString();
            hokhau = dt.Rows[0]["ho_khau"].ToString();
            dangkythi = dt.Rows[0]["dang_ky_thi"].ToString();
            makhoi = dt.Rows[0]["ma_khoi"].ToString();
            matruong = (int)dt.Rows[0]["ma_truong"];
            manganh = (int)dt.Rows[0]["ma_nganh"];
            madoituong = (int)dt.Rows[0]["ma_doi_tuong"];

            so_phieu.Text = sophieu.ToString();
            ho_va_ten.Text = hovaten.ToString();
            ngay_sinh.SelectedDate = ngaysinh;
            khu_vuc.Text = khuvuc;
            nam_tot_nghiep_th.Text = namtotnghiep.ToString();

            if (heth == "KCB")
                he_th.SelectedIndex = 0;
            else he_th.SelectedIndex = 1;

            dia_chi_bao_tin.Text = diachibaotin;
            noi_sinh.Text = noisinh;
            ho_khau.Text = hokhau;
            if (dangkythi == "KCB")
                dang_ky_thi.SelectedIndex = 0;
            else dang_ky_thi.SelectedIndex = 1;

            ma_khoi.Text = makhoi;
            ma_truong.Text = matruong.ToString();
            ma_nganh.Text = manganh.ToString();
            ma_doi_tuong.Text = madoituong.ToString();


        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (editMode == 0)// edit
                SubmitEdit();

            if (editMode == 1) // add
                SubmitAdd();

            uptdelegate();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

   

        void SubmitEdit()
        {


            hovaten = ho_va_ten.Text;
            ngaysinh = (DateTime)ngay_sinh.SelectedDate;
            string strNgS = ngaysinh.ToString("yyyy-MM-dd");
            khuvuc = khu_vuc.Text;
            namtotnghiep = int.Parse(nam_tot_nghiep_th.Text);
            if (he_th.SelectedIndex == 0) heth = "KCB";
            else heth = "CB";

            diachibaotin = dia_chi_bao_tin.Text;
            noisinh = noi_sinh.Text;
            hokhau = ho_khau.Text;
            if (dang_ky_thi.SelectedIndex == 0) dangkythi = "KCB";
            else dangkythi = "CB";

            makhoi = ma_khoi.Text;
            #region check valid makhoi
            int makhoivalid = 0;
            string CmdString = "SELECT 1 FROM KHOI WHERE ma_khoi = '" + makhoi + "'";

            CheckLogin.ConnectToSQL();

            SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("KHOI");

            sda.Fill(dt);

            if (dt.Rows.Count != 0) makhoivalid = 1;
            #endregion

            matruong = int.Parse(ma_truong.Text);
            #region check valid matruong
            string test = "";
            int matruongvalid = 0;
            string CmdString1 = "SELECT 1 FROM TRUONG WHERE ma_truong = " + matruong.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd1 = new SqlCommand(CmdString1, CheckLogin.Connection);

            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);

            DataTable dt1 = new DataTable("TRUONG");

            sda1.Fill(dt1);

            if (dt1.Rows.Count != 0) matruongvalid = 1;

            #endregion

            manganh = int.Parse(ma_nganh.Text);
            #region check valid manganh
            int manganhvalid = 0;

            string CmdString2 = "SELECT * FROM NGANH WHERE ma_nganh = " + manganh.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd2 = new SqlCommand(CmdString2, CheckLogin.Connection);

            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);

            DataTable dt2 = new DataTable("NGANH");

            sda2.Fill(dt2);

            if (dt2.Rows.Count != 0) manganhvalid = 1;
            #endregion

            madoituong = int.Parse(ma_doi_tuong.Text);
            #region check valid ma doi tuong
            int madoituongvalid = 0;

            string CmdString3 = "SELECT 1 FROM DOI_TUONG_DU_THI WHERE ma_doi_tuong = " + madoituong.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd3 = new SqlCommand(CmdString3, CheckLogin.Connection);

            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);

            DataTable dt3 = new DataTable("NGANH");

            sda3.Fill(dt3);

            if (dt3.Rows.Count != 0) madoituongvalid = 1;

            #endregion


            if (matruongvalid == 0)
            { MessageBox.Show("Wrong University ID " + test); return; }
            if (manganhvalid == 0)
            { MessageBox.Show("Wrong major ID"); return; }
            if (makhoivalid == 0)
            { MessageBox.Show("Wrong Grade ID"); return; }
            if(madoituongvalid == 0)
            {
                MessageBox.Show("Wrong Student Classify"); return;
            }
            if(string.IsNullOrEmpty(hovaten))
            {
                MessageBox.Show("Please set Candidate's Name"); return;
            }
            if(string.IsNullOrEmpty(noisinh))
            {
                MessageBox.Show("Please set Candidate's Birthplace"); return;
            }
            if(string.IsNullOrEmpty(khuvuc))
            { MessageBox.Show("Please set Candidate's Area"); return; }
            if(string.IsNullOrEmpty(nam_tot_nghiep_th.Text))
            {
                MessageBox.Show("Please set Candidate's Graduation year"); return;
            }

            if(string.IsNullOrEmpty(diachibaotin))
            {
                MessageBox.Show("Please set Candidate's Adress"); return;
            }
            if(string.IsNullOrEmpty(hokhau))
            {
                MessageBox.Show("Please set Candidate's Household"); return;
            }
            ExecuteQuery.executeQuery("UPDATE PHIEU_DKDT " +
                "SET ho_va_ten = N'" + hovaten + "'," +
                "ngay_sinh = '" + strNgS + "'," +
                "khu_vuc = " + khuvuc + "," +
                "nam_tot_nghiep_th =" + namtotnghiep.ToString() + "," +
                "he_th = '" + heth + "'," +
                "dia_chi_bao_tin = N'" + diachibaotin + "'," +
                "noi_sinh = N'" + noisinh + "'," +
                "ho_khau = N'" + hokhau + "'," +
                "dang_ky_thi = '" + dangkythi + "'," +
                "ma_khoi = '" + makhoi + "'," +
                "ma_truong = " + matruong.ToString() + "," +
                "ma_nganh = " + manganh.ToString()+ "," +
                "ma_doi_tuong = " + madoituong.ToString() +
                " WHERE so_phieu =" + sophieu.ToString());

            this.Close();


        }
        void SubmitAdd()
        {
            #region check valid data
            if (string.IsNullOrEmpty(ho_va_ten.Text))
            {
                MessageBox.Show("Chưa nhập họ tên"); return;
            }
            if (string.IsNullOrEmpty(noi_sinh.Text))
            {
                MessageBox.Show("Chưa nhập nơi sinh"); return;
            }
            if (string.IsNullOrEmpty(khu_vuc.Text))
            { MessageBox.Show("Chưa nhập khu vực"); return; }
            if (string.IsNullOrEmpty(nam_tot_nghiep_th.Text))
            {
                MessageBox.Show("Chưa nhập năm tốt nghiệp"); return;
            }
            if (ngay_sinh.SelectedDate == null)
            {
                MessageBox.Show("Chưa nhập ngày sinh"); return;
            }
            if (string.IsNullOrEmpty(dia_chi_bao_tin.Text))
            {
                MessageBox.Show("Chưa nhập địa chỉ báo tin"); return;
            }
            if (string.IsNullOrEmpty(ho_khau.Text))
            {
                MessageBox.Show("Chưa nhập hộ khẩu"); return;
            }
            if(string.IsNullOrEmpty(ma_truong.Text))
            {
                MessageBox.Show("Chưa nhập mã trường"); return;
            }
            if(string.IsNullOrEmpty(ma_nganh.Text))
            {
                MessageBox.Show("Chưa nhập mã ngành"); return;
            }
            if(string.IsNullOrEmpty(ma_khoi.Text))
            {
                MessageBox.Show("Chưa nhập mã khối"); return;
            }
            #endregion

            hovaten = ho_va_ten.Text;
            
            if(ngay_sinh.SelectedDate != null)
                ngaysinh = (DateTime)ngay_sinh.SelectedDate;
            string strNgS = ngaysinh.ToString("yyyy-MM-dd");
            khuvuc = khu_vuc.Text;
            namtotnghiep = int.Parse(nam_tot_nghiep_th.Text);
            if (he_th.SelectedIndex == 0) heth = "KCB";
            else heth = "CB";

            diachibaotin = dia_chi_bao_tin.Text;
            noisinh = noi_sinh.Text;
            hokhau = ho_khau.Text;
            if (dang_ky_thi.SelectedIndex == 0) dangkythi = "KCB";
            else dangkythi = "CB";

            makhoi = ma_khoi.Text;
            #region check valid makhoi
            int makhoivalid = 0;
            string CmdString = "SELECT 1 FROM KHOI WHERE ma_khoi = '" + makhoi + "'";

            CheckLogin.ConnectToSQL();

            SqlCommand cmd = new SqlCommand(CmdString, CheckLogin.Connection);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable("KHOI");

            sda.Fill(dt);

            if (dt.Rows.Count != 0) makhoivalid = 1;
            #endregion

            matruong = int.Parse(ma_truong.Text);
            #region check valid matruong
            string test = "";
            int matruongvalid = 0;
            string CmdString1 = "SELECT 1 FROM TRUONG WHERE ma_truong = " + matruong.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd1 = new SqlCommand(CmdString1, CheckLogin.Connection);

            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);

            DataTable dt1 = new DataTable("TRUONG");

            sda1.Fill(dt1);

            if (dt1.Rows.Count != 0) matruongvalid = 1;

            #endregion

            manganh = int.Parse(ma_nganh.Text);
            #region check valid manganh
            int manganhvalid = 0;

            string CmdString2 = "SELECT 1 FROM NGANH WHERE ma_nganh = " + manganh.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd2 = new SqlCommand(CmdString2, CheckLogin.Connection);

            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);

            DataTable dt2 = new DataTable("NGANH");

            sda2.Fill(dt2);

            if (dt2.Rows.Count != 0) manganhvalid = 1;
            #endregion

            madoituong = int.Parse(ma_doi_tuong.Text);
            #region check valid ma doi tuong
            int madoituongvalid = 0;

            string CmdString3 = "SELECT 1 FROM DOI_TUONG_DU_THI WHERE ma_doi_tuong = " + madoituong.ToString();

            CheckLogin.ConnectToSQL();

            SqlCommand cmd3 = new SqlCommand(CmdString3, CheckLogin.Connection);

            SqlDataAdapter sda3 = new SqlDataAdapter(cmd3);

            DataTable dt3 = new DataTable("NGANH");

            sda3.Fill(dt3);

            if (dt3.Rows.Count != 0) madoituongvalid = 1;

            #endregion

            if (matruongvalid == 0)
            { MessageBox.Show("Wrong University ID " + test); return; }
            if (manganhvalid == 0)
            { MessageBox.Show("Wrong Major ID"); return; }
            if (makhoivalid == 0)
            { MessageBox.Show("Wrong Grade ID"); return; }

            if (madoituongvalid == 0)
            {
                MessageBox.Show("Sai mã đối tượng"); return;
            }

            try
            {
                
                ExecuteQuery.executeQuery("INSERT INTO PHIEU_DKDT VALUES (" + sophieu.ToString() + "," +
                "N'" + hovaten + "'," +
                "'" + strNgS + "'," +
                "'" + khuvuc + "'," +
                "'" + namtotnghiep.ToString() + "'," +
                "'" + heth + "'," +
                "N'" + diachibaotin + "'," +
                "N'" + noisinh + "'," +
                "N'" + hokhau + "'," +
                "'" + dangkythi + "'," +
                "'" + makhoi + "'," +
                "" + matruong.ToString() + "," +
                "" + manganh.ToString() + "," +
                "" + madoituong.ToString() + ")");

                this.Close();
            }
            catch
            {
                this.sophieu = int.Parse(SQLInteraction.ExecuteQuery.GetStringFromQuery("select max(so_phieu) as phieumoi from PHIEU_DKDT"));

                ExecuteQuery.executeQuery("INSERT INTO PHIEU_DKDT VALUES (" + sophieu.ToString() + "," +
                "N'" + hovaten + "'," +
                "'" + strNgS + "'," +
                "'" + khuvuc + "'," +
                "'" + namtotnghiep.ToString() + "'," +
                "'" + heth + "'," +
                "N'" + diachibaotin + "'," +
                "N'" + noisinh + "'," +
                "N'" + hokhau + "'," +
                "'" + dangkythi + "'," +
                "'" + makhoi + "'," +
                "" + matruong.ToString() + "," +
                "" + manganh.ToString() + "," +
                "" + madoituong.ToString() + ")");


                MessageBox.Show(" Số phiếu đã bị thay đổi do có người nhập từ trước ");
                this.Close();
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (editMode == 0)// edit
                    SubmitEdit();

                if (editMode == 1) // add
                    SubmitAdd();

                uptdelegate();
            }
        }
    }
}
