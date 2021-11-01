using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for CandidateListDatagrid.xaml
    /// </summary>
    public partial class CandidateListDatagrid : Page
    {
        DataRowView rowView;
       


        public CandidateListDatagrid()
        {
            InitializeComponent();
            UpdateGrid();
        }

        bool isScoringFinished()
        {
            string isFinished = SQLInteraction.ExecuteQuery.GetStringFromQuery("select trang_thai from TRANG_THAI_KI_THI");

            if (isFinished == "2")
                return true;
            else return false;
        }

        void UpdateGrid()
        {                                                                       
            DataTable table  = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_bao_danh as 'ID',ho_va_ten as 'Name', ngay_sinh as 'Day of birth',"+
                                                                                 "noi_sinh as 'Birthplace', dia_chi_bao_tin as 'Adress', le_phi_thi as 'Fee', tong_diem as 'Score',"+
                                                                                 "trung_tuyen as 'Passed', dia_diem as 'Exam place', so_phong as 'Room', ngay_thi as 'Exam day',ten_nganh as 'Major'  from DU_LIEU_THI_SINH, PHONG_THI, NGAY_THI,NGANH where DU_LIEU_THI_SINH.ma_phong_thi = PHONG_THI.ma_phong_thi and DU_LIEU_THI_SINH.ma_nganh = NGANH.ma_nganh", "DU_LIEU_THI_SINH");

            CandidateGrid.ItemsSource = table.DefaultView;
        }

        private void ExportData_Click(object sender, RoutedEventArgs e)
        {
            if(rowView != null)
            {
                if(!isScoringFinished()) ExportAnnounce();
                else ExportResult();

            }
            

        }


        void ExportAnnounce()
        {
            

            DataTable table = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_bao_danh,ho_va_ten,ngay_sinh,noi_sinh,dia_chi_bao_tin,dia_diem,so_phong,le_phi_thi,ngay_thi from DU_LIEU_THI_SINH, PHONG_THI, NGAY_THI,NGANH where DU_LIEU_THI_SINH.ma_phong_thi = PHONG_THI.ma_phong_thi and DU_LIEU_THI_SINH.ma_nganh = NGANH.ma_nganh and so_bao_danh = " + rowView[0].ToString(), "testtable");

        
            string so_bao_danh = table.Rows[0]["so_bao_danh"].ToString();
            string ho_va_ten = table.Rows[0]["ho_va_ten"].ToString();
            DateTime ngay_sinh = (DateTime)table.Rows[0]["ngay_sinh"];
            string strNgaySinh = ngay_sinh.ToString("dd/MM/yyyy");
            string noi_sinh = table.Rows[0]["noi_sinh"].ToString();
            string dia_chi_bao_tin = table.Rows[0]["dia_chi_bao_tin"].ToString();
            string dia_diem_thi = table.Rows[0]["dia_diem"].ToString();
            string so_phong_thi = table.Rows[0]["so_phong"].ToString();
            string le_phi_thi = table.Rows[0]["le_phi_thi"].ToString();
            DateTime ngay_thi = (DateTime)table.Rows[0]["ngay_thi"];
            string strNgayThi = ngay_thi.ToString("dd/MM/yyyy");

           




            #region Print
            // print dialog
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true)
                return;

            // create fixed document
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            // create a page
            FixedPage page1 = new FixedPage();
            page1.Width = doc.DocumentPaginator.PageSize.Width;
            page1.Height = doc.DocumentPaginator.PageSize.Height;

            AnnouncePrint page1content = new AnnouncePrint(ho_va_ten, strNgaySinh, dia_chi_bao_tin, dia_diem_thi, strNgayThi, noi_sinh, so_bao_danh, so_phong_thi, le_phi_thi);
            page1content.Width = printDialog.PrintableAreaWidth;
            page1content.Height = printDialog.PrintableAreaHeight/2;
            
            //page1content.Margin = new Thickness(190);
            page1.Children.Add(page1content);

            // add the page to the document
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page1);
            doc.Pages.Add(page1Content);

            // test function for page 2

           

            // and print
            printDialog.PrintDocument(doc.DocumentPaginator, "My first document");

            #endregion

        }


        void ExportResult()
        {

            DataTable CandidateInfo = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ho_va_ten, so_bao_danh, ngay_sinh, dia_chi_bao_tin,noi_sinh, tong_diem, trung_tuyen from DU_LIEU_THI_SINH where so_bao_danh = " + rowView[0].ToString(), "DU_LIEU_1_THI_SINH");

            string ho_ten = CandidateInfo.Rows[0]["ho_va_ten"].ToString();
            DateTime NS = (DateTime)CandidateInfo.Rows[0]["ngay_sinh"];
            string ngay_sinh = NS.ToString("dd/MM/yyyy");
            string dia_chi_bao_tin = CandidateInfo.Rows[0]["dia_chi_bao_tin"].ToString();
            string noi_sinh = CandidateInfo.Rows[0]["noi_sinh"].ToString();
            string so_bao_danh = CandidateInfo.Rows[0]["so_bao_danh"].ToString();
            DataTable diem_thi = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ten_mon,diem from DIEM_THI,MON_THI where DIEM_THI.ma_mon = MON_THI.ma_mon and so_bao_danh = " + so_bao_danh, "DIEM_THI");
            string mon1 = diem_thi.Rows[0]["ten_mon"].ToString() + ": "; string diem1 = diem_thi.Rows[0]["diem"].ToString();
            string mon2 = diem_thi.Rows[1]["ten_mon"].ToString() + ": "; string diem2 = diem_thi.Rows[1]["diem"].ToString();
            string mon3 = diem_thi.Rows[2]["ten_mon"].ToString() + ": "; string diem3 = diem_thi.Rows[2]["diem"].ToString();
            string tong_diem = CandidateInfo.Rows[0]["tong_diem"].ToString();
            string trung_tuyen;
            if (CandidateInfo.Rows[0]["trung_tuyen"] != null) trung_tuyen = CandidateInfo.Rows[0]["trung_tuyen"].ToString();
            else trung_tuyen = "Chưa có kết quả";
            if(string.IsNullOrEmpty(trung_tuyen)) trung_tuyen = "Chưa có kết quả";
            if (trung_tuyen == "True") trung_tuyen = "Đã trúng tuyển";
            if (trung_tuyen == "False") trung_tuyen = "Không trúng tuyển";

            #region Print
            // print dialog
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true)
                return;

            // create fixed document
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            // create a page
            FixedPage page1 = new FixedPage();
            page1.Width = doc.DocumentPaginator.PageSize.Width;
            page1.Height = doc.DocumentPaginator.PageSize.Height;

            Result page1content = new Result(ho_ten,ngay_sinh,dia_chi_bao_tin,noi_sinh,so_bao_danh,mon1,diem1,mon2,diem2,mon3,diem3,tong_diem,trung_tuyen);
            page1content.Width = printDialog.PrintableAreaWidth;
            page1content.Height = printDialog.PrintableAreaHeight / 2;

            //page1content.Margin = new Thickness(190);
            page1.Children.Add(page1content);

            // add the page to the document
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page1);
            doc.Pages.Add(page1Content);

            // test function for page 2



            // and print
            printDialog.PrintDocument(doc.DocumentPaginator, "My first document");

            #endregion

        }

        void ExportAllResult()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true)
                return;
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            DataTable CandidateInfo = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ho_va_ten, so_bao_danh, ngay_sinh, dia_chi_bao_tin,noi_sinh, tong_diem, trung_tuyen from DU_LIEU_THI_SINH", "DU_LIEU_1_THI_SINH");

            for(int i = 0; i < CandidateInfo.Rows.Count; i++)
            {
                string ho_ten = CandidateInfo.Rows[i]["ho_va_ten"].ToString();
                DateTime NS = (DateTime)CandidateInfo.Rows[i]["ngay_sinh"];
                string ngay_sinh = NS.ToString("dd/MM/yyyy");
                string dia_chi_bao_tin = CandidateInfo.Rows[i]["dia_chi_bao_tin"].ToString();
                string noi_sinh = CandidateInfo.Rows[i]["noi_sinh"].ToString();
                string so_bao_danh = CandidateInfo.Rows[i]["so_bao_danh"].ToString();
                DataTable diem_thi = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ten_mon,diem from DIEM_THI,MON_THI where DIEM_THI.ma_mon = MON_THI.ma_mon and so_bao_danh = " + so_bao_danh, "DIEM_THI");
                string mon1 = diem_thi.Rows[0]["ten_mon"].ToString() + ": "; string diem1 = diem_thi.Rows[0]["diem"].ToString();
                string mon2 = diem_thi.Rows[1]["ten_mon"].ToString() + ": "; string diem2 = diem_thi.Rows[1]["diem"].ToString();
                string mon3 = diem_thi.Rows[2]["ten_mon"].ToString() + ": "; string diem3 = diem_thi.Rows[2]["diem"].ToString();
                string tong_diem = CandidateInfo.Rows[i]["tong_diem"].ToString();
                string trung_tuyen;
                if (CandidateInfo.Rows[i]["trung_tuyen"] != null) trung_tuyen = CandidateInfo.Rows[i]["trung_tuyen"].ToString();
                else trung_tuyen = "Chưa có kết quả";
                if (string.IsNullOrEmpty(trung_tuyen)) trung_tuyen = "Chưa có kết quả";
                if (trung_tuyen == "True") trung_tuyen = "Đã trúng tuyển";
                if (trung_tuyen == "False") trung_tuyen = "Không trúng tuyển";


                FixedPage page1 = new FixedPage();
                page1.Width = doc.DocumentPaginator.PageSize.Width;
                page1.Height = doc.DocumentPaginator.PageSize.Height;

                Result page1content = new Result(ho_ten, ngay_sinh, dia_chi_bao_tin, noi_sinh, so_bao_danh, mon1, diem1, mon2, diem2, mon3, diem3, tong_diem, trung_tuyen);
                page1content.Width = printDialog.PrintableAreaWidth;
                page1content.Height = printDialog.PrintableAreaHeight / 2;

                //page1content.Margin = new Thickness(190);
                page1.Children.Add(page1content);

                // add the page to the document
                PageContent page1Content = new PageContent();
                ((IAddChild)page1Content).AddChild(page1);
                doc.Pages.Add(page1Content);
                // add blank table to avoid two-side-paper
                FixedPage blankPage = new FixedPage();
                blankPage.Width = doc.DocumentPaginator.PageSize.Width;
                blankPage.Height = doc.DocumentPaginator.PageSize.Height;
                PageContent blankcontent = new PageContent();
                ((IAddChild)blankcontent).AddChild(blankPage);
                doc.Pages.Add(blankcontent);
            }
            // and print
            printDialog.PrintDocument(doc.DocumentPaginator, "My first document");
        }

        private void DataGridRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow selectedRow = (DataGridRow)sender;

            selectedRow.IsSelected = true;
            rowView = CandidateGrid.SelectedItem as DataRowView;
        }

        private void RegisterGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void ExportAll(object sender, RoutedEventArgs e) {
            if (!isScoringFinished()) ExportAllAnnounce();
            else ExportAllResult();


        }
        private void ExportAllAnnounce()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true)
                return;

            // create fixed document
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            DataTable table = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_bao_danh,ho_va_ten,ngay_sinh,noi_sinh,dia_chi_bao_tin,dia_diem,so_phong,le_phi_thi,ngay_thi from DU_LIEU_THI_SINH, PHONG_THI, NGAY_THI,NGANH where DU_LIEU_THI_SINH.ma_phong_thi = PHONG_THI.ma_phong_thi and DU_LIEU_THI_SINH.ma_nganh = NGANH.ma_nganh", "DU_LIEU_THI_SINH");

            for (int i = 0; i < table.Rows.Count;i++)
            {
                
                string so_bao_danh = table.Rows[i]["so_bao_danh"].ToString();
                string ho_va_ten = table.Rows[i]["ho_va_ten"].ToString();
                DateTime ngay_sinh = (DateTime)table.Rows[i]["ngay_sinh"];
                string strNgaySinh = ngay_sinh.ToString("dd/MM/yyyy");
                string noi_sinh = table.Rows[i]["noi_sinh"].ToString();
                string dia_chi_bao_tin = table.Rows[i]["dia_chi_bao_tin"].ToString();
                string dia_diem_thi = table.Rows[i]["dia_diem"].ToString();
                string so_phong_thi = table.Rows[i]["so_phong"].ToString();
                string le_phi_thi = table.Rows[i]["le_phi_thi"].ToString();
                DateTime ngay_thi = (DateTime)table.Rows[i]["ngay_thi"];
                string strNgayThi = ngay_thi.ToString("dd/MM/yyyy");


                // create a page
                FixedPage page1 = new FixedPage();
                page1.Width = doc.DocumentPaginator.PageSize.Width;
                page1.Height = doc.DocumentPaginator.PageSize.Height;

                AnnouncePrint page1content = new AnnouncePrint(ho_va_ten, strNgaySinh, dia_chi_bao_tin, dia_diem_thi, strNgayThi, noi_sinh, so_bao_danh, so_phong_thi, le_phi_thi);
                page1content.Width = printDialog.PrintableAreaWidth;
                page1content.Height = printDialog.PrintableAreaHeight / 2;

                //page1content.Margin = new Thickness(190);
                page1.Children.Add(page1content);

                // add the page to the document
                PageContent page1Content = new PageContent();
                ((IAddChild)page1Content).AddChild(page1);
                doc.Pages.Add(page1Content);
                // add blank table to avoid two-side-paper
                FixedPage blankPage = new FixedPage();
                blankPage.Width = doc.DocumentPaginator.PageSize.Width;
                blankPage.Height = doc.DocumentPaginator.PageSize.Height;
                PageContent blankcontent = new PageContent();
                ((IAddChild)blankcontent).AddChild(blankPage);
                doc.Pages.Add(blankcontent);


            }
            // and print
            printDialog.PrintDocument(doc.DocumentPaginator, "My first document");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = TxtSearchBox.Text;
            DataTable searchresult = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_bao_danh as 'Số báo danh', ho_va_ten as 'Họ và tên', ngay_sinh as 'Ngày sinh', "+
                                                                                 "noi_sinh as 'Nơi sinh', dia_chi_bao_tin as 'Địa chỉ báo tin', le_phi_thi as 'Lệ phí thi', tong_diem as 'Tổng điểm'," +
                                                                                 "trung_tuyen as 'Trúng tuyển', dia_diem as 'Địa điểm thi', so_phong as 'Số phòng thi', ngay_thi as 'Ngày thi',ten_nganh as 'Ngành thi'  from DU_LIEU_THI_SINH, PHONG_THI, NGAY_THI,NGANH where DU_LIEU_THI_SINH.ma_phong_thi = PHONG_THI.ma_phong_thi and DU_LIEU_THI_SINH.ma_nganh = NGANH.ma_nganh and ho_va_ten LIKE N'%" + searchText + "%'", "PHIEU_DKDT");
            CandidateGrid.ItemsSource = searchresult.DefaultView;
        }
    }
}
