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

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for DevForm.xaml
    /// </summary>
    public partial class DevForm : Window
    {
        public DevForm()
        {
            
            InitializeComponent();

            string conStr = SQLInteraction.SQLConnector.ConnectionString;
            connectString.Text = conStr;
        }

        private void ResetPhases(object sender, RoutedEventArgs e)
        {
            SQLInteraction.ExecuteQuery.executeQuery("delete from DIEM_THI delete from DU_LIEU_THI_SINH update TRANG_THAI_KI_THI set TRANG_THAI = 0");
            this.Close();
        }

        private void SubmitConString(object sender, RoutedEventArgs e)
        {
            SQLInteraction.SQLConnector.ConnectionString = (connectString.Text).Trim();
            this.Close();
        }
    }
}
