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
    /// Interaction logic for CandidateList.xaml
    /// </summary>
    public partial class CandidateList : Window
    {

        public CandidateList()
        {
            InitializeComponent();
            CandidateListDatagrid content = new CandidateListDatagrid();
            Holder.Content = content;
        }

        Style SelectingStyle()
        {
            // to selected style
            return FindResource("MyButtonStyleChosen") as Style;
        }
        Style IdlingStyle()
        {   // to idel style ( when selecting another )
            return FindResource("MyButtonStyleIdle") as Style;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            this.Close();
        }

        private void BtnCandidateList_Click(object sender, RoutedEventArgs e)
        {
            #region style changing
            BtnCandidateList.Style = SelectingStyle();
            BtnScoring.Style = IdlingStyle();
            BtnStatistic.Style = IdlingStyle();
            #endregion

            CandidateListDatagrid content = new CandidateListDatagrid();
            Holder.Content = content;
        }

        private void BtnScoring_Click(object sender, RoutedEventArgs e)
        {
            #region style changing
            BtnCandidateList.Style = IdlingStyle();
            BtnScoring.Style =  SelectingStyle();
            BtnStatistic.Style = IdlingStyle();
            #endregion

            ScoringDatagrid content = new ScoringDatagrid();
            Holder.Content = content;
        }

        private void BtnStatistic_Click(object sender, RoutedEventArgs e)
        {
            #region style changing
            BtnCandidateList.Style = IdlingStyle();
            BtnScoring.Style = IdlingStyle();
            BtnStatistic.Style = SelectingStyle();
            #endregion

            Statistic content = new Statistic();
            Holder.Content = content;
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            newWindow.Show();
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
