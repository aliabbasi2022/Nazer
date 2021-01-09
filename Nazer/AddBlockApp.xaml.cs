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
    /// Interaction logic for AddBlockApp.xaml
    /// </summary>
    public partial class AddBlockApp : Window
    {
        Main.ColorList AllApps;
        public string ChildID;
        public AddBlockApp()
        {
            InitializeComponent();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow.MyMain.Show();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            BlockApp Data = new BlockApp();
            //InstalledApp inst =(InstalledApp)AllAppsList.SelectedItem;
            string []SelectedData = AllApps.GetDataAt(AllApps.SelectedIndex);
            Data.AppName = SelectedData[0];
            Data.Act = ActType.SelectedIndex.ToString();
            Data.Category = "";
            Data.ID = DateTime.Now.ToString("MM/dd/yyyy Hh:Mm:ss.fff");
            Main.InstalledAppsSemaphore.WaitOne();
            Data.AppID = MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select AppID From InstalledApps where DisplayName ='" + Data.AppName + "'").ToString();
            Main.InstalledAppsSemaphore.Release();
            Main.SelctedToAdd = Data;
            this.Close();
            MainWindow.MyMain.Show();
        }

       

        private void AllAppsList_Loaded(object sender, RoutedEventArgs e)
        {
            //GridView InstalledAppGV = new GridView();
            //InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Name", Width = 130, DisplayMemberBinding = new Binding("DisplayName") });
            //InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Installed Date", Width = 150, DisplayMemberBinding = new Binding("InstallDate") });
            //InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Version", Width = 100, DisplayMemberBinding = new Binding("DisplayVersion") });
            //InstalledAppGV.Columns.Add(new GridViewColumn { Header = "Publisher", Width = 100, DisplayMemberBinding = new Binding("Publisher") });
            //AllAppsList.View = InstalledAppGV;
            //MainWindow.DataBaseAgent.SelectData("InstalledApps", ref MainWindow.DS, "*", "InstalledApps", Main.ChildUser, "ChildID");
            //foreach (System.Data.DataRow Row in MainWindow.DS.Tables["InstalledApps"].Rows)
            //{
            //    AllAppsList.Items.Add(new InstalledApp
            //    {
            //        DisplayName = Row["DisplayName"].ToString(),
            //        DisplayVersion = Row["DisplayVersion"].ToString(),
            //        InstallDate = Row["InstallDate"].ToString(),
            //        Publisher = Row["Publisher"].ToString()
            //    });
            //}
            ActType.Items.Add("Close");
            ActType.Items.Add("Close with Show Message");
            ActType.SelectedIndex = 0;
            MainWindow.DS.Tables.Remove("InstalledApps");
        }

        private void PageGrid_Loaded(object sender, RoutedEventArgs e)
        {
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
                new Thickness(30, 130, 10, 10), new Thickness(0), 12, 16, AllAppsColumnsName, AllApp_SelectionChanged, "AppsList", 150);
            AllAppborder.VerticalAlignment = VerticalAlignment.Top;
            MainWindow.DS.Tables.Remove("InstalledApps");
            Main.InstalledAppsSemaphore.Release();
            AllAppborder.HorizontalAlignment = HorizontalAlignment.Left;
            PageGrid.Children.Add(AllAppborder);
        }

        private void AllApp_SelectionChanged(object obj)
        {
            if (AllApps.SelectedIndex > -1)
            {
                AddBtn.IsEnabled = true;
            }
            else
            {
                AddBtn.IsEnabled = false;
            }
        }
    }
}
