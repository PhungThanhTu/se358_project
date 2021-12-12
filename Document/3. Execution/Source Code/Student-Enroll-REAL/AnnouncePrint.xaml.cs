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
    /// Interaction logic for AnnouncePrint.xaml
    /// </summary>
    public partial class AnnouncePrint : UserControl
    {
        public AnnouncePrint()
        {
            InitializeComponent();
        }
        public AnnouncePrint(string hoTen, string ngaySinh, string diaChiBaoTin, string diaDiemThi, string ngayThi, string noiSinh, string soBaoDanh, string soPhongThi, string lePhiThi) : this()

        {

            txtHo_ten.Text = hoTen;
            txtNgay_Sinh.Text = ngaySinh;
            txtDia_chi_bao_tin.Text = diaChiBaoTin;
            txtDia_diem_thi.Text = diaDiemThi;
            txtNgay_thi.Text = ngayThi;
            txtNoi_sinh.Text = noiSinh;
            txtSo_bao_danh.Text = soBaoDanh;
            txtSo_phong_thi.Text = soPhongThi;
            txtLe_phi_thi.Text = lePhiThi;
        }

    }
}
