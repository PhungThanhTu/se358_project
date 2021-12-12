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
using Student_Enroll_REAL.Authentication;
using Student_Enroll_REAL.SQLInteraction;

namespace Student_Enroll_REAL
{
    
    /// <summary>
    /// Interaction logic for ScoringDatagrid.xaml
    /// </summary>
    public partial class ScoringDatagrid : Page
    {
        
        public ScoringDatagrid()
        {
            InitializeComponent();
            AddItemToCombobox();
            SubjectCbx.SelectedIndex = 0;
        }

        
        

        ComboBoxItem addNewCBbox(int value, string displayText)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = displayText;
            item.Tag = value;
            return item;
            
        }

        void UpdateGrid()
        {
            ComboBoxItem selectedItem = SubjectCbx.SelectedItem as ComboBoxItem;
            string selectedMaMon = selectedItem.Tag.ToString();
            DataTable scoringTable = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select so_phach as 'Test ID',diem as 'Score'" +
                "from DIEM_THI WHERE ma_mon = " + selectedMaMon, "DIEM_THI");
            ScoreDtGrid.ItemsSource = scoringTable.DefaultView;
        }
       
        private void SubjectCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGrid();
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

        private void DataGridRow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }



        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
                DataGridRow selectedRow = (DataGridRow)sender;

                selectedRow.IsSelected = true;
                DataRowView rowView = ScoreDtGrid.SelectedItem as DataRowView;
                int so_phach = (int)rowView[0];

                ScoreEntering editWindow = new ScoreEntering(so_phach);
                
                editWindow.updategrid = UpdateGrid;
                editWindow.ShowDialog();
            
            


        }

      

        private void SetMark_Click(object sender, RoutedEventArgs e)
        {   
            if(User.Login_status != "admin")
            {
                MessageBox.Show("Admin permission required, please login as admin to use this function");
                return;
            }
            CrucialMarkSetter newwindow = new CrucialMarkSetter();
            newwindow.Show();
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(User.Login_status != "admin")
            {
                MessageBox.Show("Admin permission required");
                return;
            }
            
            int numTest = int.Parse(SQLInteraction.ExecuteQuery.GetStringFromQuery("select count(*) from DIEM_THI"));

            int numScoredTest = int.Parse(SQLInteraction.ExecuteQuery.GetStringFromQuery("select count(*) from DIEM_THI where da_cham_diem = 1"));

            if(numTest == numScoredTest)
            {
                SQLInteraction.ExecuteQuery.executeQuery("update TRANG_THAI_KI_THI set trang_thai = 2");
                MessageBox.Show("Scoring phase has ended, now the export data button in scoring UI will print result paper" +
                    "instead of announce paper, you can still change score of candidates freely","SCORING PHASE ENDED");
            }
            else
            {
                MessageBox.Show("Please score all test before confirm score");
            }

            
        }

    }
}
