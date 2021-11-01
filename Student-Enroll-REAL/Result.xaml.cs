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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : UserControl
    {

        public Result()
        {
            InitializeComponent();
        }
        public Result(string hoTen, string ngaySinh, string diaChiBaoTin, string noiSinh, string soBaoDanh,string mon1, string diemMon1,string mon2, string diemMon2,string mon3, string diemMon3, string tongDiem, string trungTuyen) : this()

        {

            txtHo_ten.Text = hoTen;
            txtNgay_Sinh.Text = ngaySinh;
            txtDia_chi_bao_tin.Text = diaChiBaoTin;
            txtNoi_sinh.Text = noiSinh;
            txtSo_bao_danh.Text = soBaoDanh;
            txtMon1.Text = mon1;
            txtMon2.Text = mon2;
            txtMon3.Text = mon3;
            txtDiemMon1.Text = diemMon1;
            txtDiemMon2.Text = diemMon2;
            txtDiemMon3.Text = diemMon3;
            txtTongDiem.Text = tongDiem;
            txtTrungTuyen.Text = trungTuyen;

        }


    }
}
