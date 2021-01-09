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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for EnterAppCat.xaml
    /// </summary>
    public partial class EnterAppCat : Window
    {
        public EnterAppCat()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool IsUsed = false;
            MainWindow.DataBaseAgent.SelectData("AppCategory", ref MainWindow.DS, "*", "AppCategory", Main.ChildUser, "ChildID");
            foreach (System.Data.DataRow Row in MainWindow.DS.Tables["AppCategory"].Rows)
            {
                if(Row["Name"].ToString() == CatNameTxt.Text)
                {
                    IsUsed = true;
                    break;
                }
            }
            MainWindow.DS.Tables.Remove("AppCategory");
            if(IsUsed == false)
            {
                Main.NewCat = CatNameTxt.Text;
                this.Hide();
                MainWindow.MyMain.Show();
            }
            else
            {
                Status.Text = "Can Not Add dublicate Data";
            }
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.NewCat = "";
            this.Hide();
            MainWindow.MyMain.Show();
        }
    }
}
