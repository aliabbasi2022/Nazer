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
    /// Interaction logic for EnterAppInCat.xaml
    /// </summary>
    public partial class EnterAppInCat : Window
    {
        Main.ColorList AllApps;
        public string ChildID;
        int SelectedIndex = -1;
        public EnterAppInCat(string ChildID)
        {
            this.ChildID = ChildID;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CatName.Text = Main.SelectedCat;
            Main.InstalledAppsSemaphore.WaitOne();
            MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", Main.ChildUser, "ChildID");
            //foreach (System.Data.DataRow Row in MainWindow.DS.Tables["InstalledApps"].Rows)
            //{
            //    AppsList.Items.Add(Row["DisplayName"].ToString());
            //}
            AllApps = new Main.ColorList();
            //ListPinters.Add(&CategoryAppAppList);
            Size AppItemSize = new Size(0, 25);
            Size AppListSize = new Size(250, 0);
            double[] AllAppsColumnWidth = { 250 };
            string[] AllAppsHeadersName = { "Apps Name" };
            string[] AllAppsColumnsName = { "DisplayName" };
            MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", ChildID, "ChildID");
            Border AllAppborder = AllApps.Draw(AllAppsColumnWidth, 30, AllAppsHeadersName, null, AppListSize, AppItemSize, Color.FromArgb(255, 238, 241, 245), Colors.Transparent,
                Colors.White, Colors.Black, Color.FromArgb(255, 42, 180, 192), Colors.LightGray, Colors.Transparent, MainWindow.DS.Tables["InstalledApps"],
                new Thickness(20, 0, 0, 0), new Thickness(0), 12, 16, AllAppsColumnsName, AllApp_SelectionChanged, "AppsList", 150);
            AllAppborder.VerticalAlignment = VerticalAlignment.Top;
            MainWindow.DS.Tables.Remove("InstalledApps");
            Main.InstalledAppsSemaphore.Release();
            AllAppborder.HorizontalAlignment = HorizontalAlignment.Left;
            EnterAppPageSt.Children.Add(AllAppborder);
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            stackPanel.Margin = new Thickness(30, 10, 20, 0);
            ((Grid)AddBtn.Parent).Children.Remove(AddBtn);
            ((Grid)CancleBtn.Parent).Children.Remove(CancleBtn);
            AddBtn.IsEnabled = false;
            stackPanel.Children.Add(AddBtn);
            stackPanel.Children.Add(CancleBtn);
            EnterAppPageSt.Children.Add(stackPanel);
        }

        private void AllApp_SelectionChanged(object obj)
        {
            SelectedIndex = AllApps.SelectedIndex;
            if(SelectedIndex > -1)
            {
                AddBtn.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Main.SelectedApp = AppsList.SelectedItem.ToString();
            this.Close();
            MainWindow.MyMain.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.SelectedApp = "";
            this.Close();
            MainWindow.MyMain.Show();
        }
    }
}
