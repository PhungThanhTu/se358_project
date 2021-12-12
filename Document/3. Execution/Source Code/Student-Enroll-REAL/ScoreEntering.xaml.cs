using System;
using System.Collections.Generic;
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
    /// Interaction logic for ScoreEntering.xaml
    /// </summary>
    public partial class ScoreEntering : Window
    {
        int so_phach;
        public delegate void updateGrid();
        public updateGrid updategrid;
        public ScoreEntering(int so_phach)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.so_phach = so_phach;
            this.Title = "ENTER SCORE FOR TEST ID " + so_phach.ToString();
            InitializeComponent();
        }

        private void txtNhapDiem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // only positive float can be entered
            Regex numbeRegex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled =
                !numbeRegex.IsMatch(
                    txtNhapDiem.Text.Insert(
                       txtNhapDiem.SelectionStart, e.Text));
            txtNhapDiem.Text = txtNhapDiem.Text.Trim();

        }

        void actSubmit()
        {
            if (string.IsNullOrEmpty(txtNhapDiem.Text))
            {
                MessageBox.Show("Please set score");
                return;
            }
            float diem = float.Parse(txtNhapDiem.Text);

            if (diem > 10)
            {
                MessageBox.Show("Maximum score is 10");
                return;
            }

            SQLInteraction.ExecuteQuery.executeQuery("update DIEM_THI " +
            "set diem =" + diem.ToString() + ", da_cham_diem = 1 " +
            "where so_phach = " + so_phach.ToString());
            updategrid();


            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            actSubmit();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                actSubmit();
            }
        }
    }
}
