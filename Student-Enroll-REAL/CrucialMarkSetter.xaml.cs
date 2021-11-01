using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for CrucialMarkSetter.xaml
    /// </summary>
    public partial class CrucialMarkSetter : Window
    {
        
        public CrucialMarkSetter()
        {
            InitializeComponent();
            AddItemToCombobox();
            cbxSpeciality.SelectedIndex = 0;
        }

        ComboBoxItem addNewCBbox(int value, string displayText)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = value.ToString() + " - " + displayText;
            item.Tag = value;
            return item;

        }

        void AddItemToCombobox()
        {
            DataTable spectDataset = new DataTable();
            spectDataset = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ma_nganh,ten_nganh from NGANH", "NGANH");

            foreach (DataRow row in spectDataset.Rows)
            {
                string displayText = row["ten_nganh"].ToString();
                int taG = int.Parse(row["ma_nganh"].ToString());

                cbxSpeciality.Items.Add(addNewCBbox(taG, displayText));
            }
        }



        private void txtBxScore_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // only positive float can be entered
            Regex numbeRegex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled =
                !numbeRegex.IsMatch(
                    txtBxScore.Text.Insert(
                       txtBxScore.SelectionStart, e.Text));
            txtBxScore.Text = txtBxScore.Text.Trim();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtBxScore.Text))
            {
                MessageBox.Show("Please enter value");
                return;
            }

            float diem_chuan = float.Parse(txtBxScore.Text);
            if(diem_chuan > 30)
            {
                MessageBox.Show("Benchmark couldn't be greater than 30");
                return;
            }
            ComboBoxItem item = cbxSpeciality.SelectedItem as ComboBoxItem;
            string ma_nganh = item.Tag.ToString();
            SQLInteraction.ExecuteQuery.executeQuery("update NGANH set diem_chuan = "
                + diem_chuan.ToString() +" where ma_nganh = " + ma_nganh) ;
            MessageBox.Show(" Benchmark set for " + ma_nganh.ToString());
        }

        private void cbxSpeciality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = cbxSpeciality.SelectedItem as ComboBoxItem;
            string ma_nganh = item.Tag.ToString();

            DataTable spectDataset = new DataTable();
            spectDataset = SQLInteraction.ExecuteQuery.SqlDataTableFromQuery("select ma_nganh,diem_chuan from NGANH where ma_nganh = " + ma_nganh, "NGANH");
            if(spectDataset.Rows[0]["diem_chuan"] == null)
            {
                txtBxScore.Text = "0";
            }
            else
            txtBxScore.Text = spectDataset.Rows[0]["diem_chuan"].ToString();
        }
    }
}
