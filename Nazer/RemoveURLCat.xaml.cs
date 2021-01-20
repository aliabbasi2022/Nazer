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
using System.Windows.Media.Imaging;//Provides types that are used to encode and decode bitmap images.
using System.Windows.Shapes;
using System.Data;

namespace UI
{
    /// <summary>
    /// Interaction logic for RemoveURLCat.xaml
    /// </summary>
    public partial class RemoveURLCat : Window
    {
        public RemoveURLCat()
        {
            InitializeComponent();
        }

        private void AllCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox Target = sender as ListBox;
            if(Target.SelectedIndex > -1)
            {
                RmoveBtn.IsEnabled = true;
            }
            else
            {
                RmoveBtn.IsEnabled = false;
            }
        }

        private void RmoveBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.SelectedURLCat = AllCat.SelectedItem.ToString();
            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From UsedColor where Name ='" + MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Color From URLCategury where Name ='" + Main.SelectedURLCat + "'" )+ "'");
            MainWindow.DataBaseAgent.ExequteWithCommand("Delete From URLCategury where Name ='" + Main.SelectedURLCat + "'");
            this.Close();
            MainWindow.MyMain.Show();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Image InfoImage0 = new System.Windows.Controls.Image();
            InfoImage0.Source = new BitmapImage(new Uri(@"pack://application:,,,/UI;component/Files/if_info-blog_46810.ico"));
            InfoImage0.Width = 30;
            InfoImage0.Height = 30;
            InfoImage0.VerticalAlignment = VerticalAlignment.Top;
            InfoImage0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoImage0.Margin = new Thickness(10, 0, 0, 0);
            GRID.Children.Add(InfoImage0);
            TextBlock InfoTb0 = new TextBlock();
            InfoTb0.Text = "To remove Catrgory Please Select One of Them in list and click Remove button";
            InfoTb0.FontSize = 16;
            InfoTb0.VerticalAlignment = VerticalAlignment.Top;
            InfoTb0.HorizontalAlignment = HorizontalAlignment.Left;
            InfoTb0.Foreground = new SolidColorBrush(Colors.Black);
            InfoTb0.Margin = new Thickness(30, 25, 0, 0);
            GRID.Children.Add(InfoTb0);
            foreach(DataRow Row in MainWindow.DS.Tables["URLCategury"].Rows)
            {
                AllCat.Items.Add(Row["Name"]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.MyMain.Show();
        }
    }
}
