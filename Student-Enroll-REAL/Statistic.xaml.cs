using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {
        public Statistic()
        {
            InitializeComponent();
            AddItemToCombobox();
            SubjectCbx.SelectedIndex = 0;
            UpdateStatistic();
        }

        ComboBoxItem addNewCBbox(int value, string displayText)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = displayText;
            item.Tag = value;
            return item;

        }

        void AddItemToCombobox()
        {
            DataTable subjectDataset = new DataTable();
            subjectDataset = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ma_mon,ten_mon from MON_THI", "MON_THI");

            foreach (DataRow row in subjectDataset.Rows)
            {
                string displayText = row["ten_mon"].ToString();
                int taG = int.Parse(row["ma_mon"].ToString());

                SubjectCbx.Items.Add(addNewCBbox(taG, displayText));
            }
        }

        private void cbXSubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = SubjectCbx.SelectedItem as ComboBoxItem;
            string selectedMaMon = selectedItem.Tag.ToString();

            tbMaxNameSbj.Text = SQLInteraction.ExecuteQuery.GetStringFromQuery("select ho_va_ten from DU_LIEU_THI_SINH,DIEM_THI where DU_LIEU_THI_SINH.so_bao_danh = DIEM_THI.so_bao_danh\n"+

                                                "and ma_mon = " + selectedMaMon + "\n" +

                                                "and diem = (select max(diem) from DIEM_THI where ma_mon = "+selectedMaMon+")");
            
        }

        void UpdateStatistic()
        {
            string countStudent = SQLInteraction.ExecuteQuery.GetStringFromQuery("select count(*) from DU_LIEU_THI_SINH");
            string countPassStudent = SQLInteraction.ExecuteQuery.GetStringFromQuery("select count(*) from DU_LIEU_THI_SINH where trung_tuyen = 1");
            string nameMaxStudent = SQLInteraction.ExecuteQuery.GetStringFromQuery("select ho_va_ten from DU_LIEU_THI_SINH where tong_diem = (select max(tong_diem) from DU_LIEU_THI_SINH)");
            tbSum.Text = countStudent;
            tbMax.Text = countPassStudent;
            tbMaxName.Text = nameMaxStudent;
            float Ratio = float.Parse(countPassStudent) / float.Parse(countStudent);
            float Percent = Ratio * 100.0f;
            tbRatio.Text = Percent.ToString() + " %";
        }
    }
}
