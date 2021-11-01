using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Student_Enroll_REAL.SQLInteraction;
using Student_Enroll_REAL.Authentication;

namespace Student_Enroll_REAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lgin_Click(object sender, RoutedEventArgs e)
        {


            Login();
            Password.Focus();
            Password.SelectAll();
            
        }

        void EnterApp()
        {
            string exam_status = SQLInteraction.ExecuteQuery.GetStringFromQuery("select trang_thai from TRANG_THAI_KI_THI");
            if(exam_status == "0")
            {
                Register registerwindow = new Register();
                this.Close();
                registerwindow.Show();
            }
            else
            {
                CandidateList candidateListWindow = new CandidateList();
                this.Close();
                candidateListWindow.Show();
            }
        }

        void Login()
        {
            CheckLogin.ConnectToSQL();

            string username = Username.Text;
            string password = Password.Password;

            User.Login_status = CheckLogin.checkLogin(username, password);
            //User.Login_status = "user";

            switch (User.Login_status)
            {
                case "admin":
                    {
                        EnterApp();
                        break;
                    }

                case "user":
                    {
                        EnterApp(); 
                        break;
                    }
                case "error":
                    {
                        MessageBox.Show("SQL error");
                        break;
                    }
                case "failed":
                    {
                        MessageBox.Show("Authentication Invalid");
                        break;
                    }
                case "test":
                    {
                        CandidateList candidateListWindow = new CandidateList();
                        this.Close();
                        candidateListWindow.Show();
                        break;
                    }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }    
        }
        private void DevClick(object sender, RoutedEventArgs e)
        {
            DevForm newform = new DevForm();
            newform.ShowDialog();
        }
    }
}
