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
using System.Data;
using System.Data.SqlClient;

namespace UI
{
    /// <summary>
    /// Interaction logic for EnterURLCat.xaml
    /// </summary>
    public partial class EnterURLCat : Window
    {
        DataSet DS = new DataSet();
        bool ISDomain = false;
        string Cat;
        int CatIndex;
        public EnterURLCat()
        {
            InitializeComponent();
        }

        private void G1_Loaded(object sender, RoutedEventArgs e)
        {
            DLable.Content = "Category Name :";
            MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID ='" + Main.ChildUser + "'", ref DS, "URLCategury");
            foreach ( DataRow Row in DS.Tables["URLCategury"].Rows)
            {
                AllUsedURLCat.Items.Add(Row["Name"].ToString());
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ISDomain == false)
            {
                if ((int)MainWindow.DataBaseAgent.ExequteWithCommandScaler("Select Count(*) From URLCategury where ChildID='" + Main.ChildUser + "'And Name='" + DataTX.Text + "'") == 0)
                {
                    if(DS.Tables.Contains("URLCategury") == false)
                    {
                        MainWindow.DataBaseAgent.SelectData("URLCategury", ref DS, "*", "URLCategury", Main.ChildUser, "ChildID");
                    }
                    DataRow NewRow = DS.Tables["URLCategury"].NewRow();
                    NewRow["Name"] = DataTX.Text;
                    NewRow["ID"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fffff");
                    NewRow["ChildID"] = Main.ChildUser;
                    NewRow["Color"] = Main.SelectColor();
                    AllUsedURLCat.Items.Add(NewRow["Name"].ToString());
                    DS.Tables["URLCategury"].Rows.Add(NewRow);
                    MainWindow.DataBaseAgent.InsertData(DS.Tables["URLCategury"]);
                    DataTX.Text = "";

                }
                else
                {
                    MessageBox.Show("Enter unique Name");
                }
            }
            else
            {
                DS.Tables["URLCategury"].Rows[0]["URls"] += (DataTX.Text + ",");
                MainWindow.DataBaseAgent.UpdateData(DS.Tables["URLCategury"]);
                AllUsedURLCat.Items.Add(DataTX.Text);
                DataTX.Text = "";
            }
            
            
        }

        private void AddURLBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if(ISDomain == false)
            {
                if(DS.Tables.Contains("URLCategury"))
                {
                    DS.Tables.Remove("URLCategury");
                }
                AllUsedURLCat.Items.Clear();
                AddURLBtn.Content = "Cancel";
                AddURLBtn.IsEnabled = true;
                DataTX.Text = "";
                DLable.Content = "Domain :";
                MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID ='" + Main.ChildUser + "' And Name ='" + Cat + "'", ref DS, "URLCategury");
                foreach (DataRow var in DS.Tables["URLCategury"].Rows)
                {
                    string[] Data = var["URLs"].ToString().Split(',');
                    for (int i = 0; i < Data.Length; i++)
                    {
                        if (Data[i] != "")
                        {
                            AllUsedURLCat.Items.Add(Data[i]);
                        }

                    }

                }
                
            }
            else
            {
                AllUsedURLCat.Items.Clear();
                DS.Tables.Remove("URLCategury");
                DLable.Content = "Category Name :";
                AddURLBtn.Content = "Edit Domain";
                MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID ='" + Main.ChildUser + "'", ref DS, "URLCategury");
                foreach (DataRow Row in DS.Tables["URLCategury"].Rows)
                {
                    AllUsedURLCat.Items.Add(Row["Name"].ToString());
                }
                DS.Tables.Remove("URLCategury");
            }
            ISDomain = !ISDomain;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void AllUsedURLCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AllUsedURLCat.SelectedIndex >  -1)
            {
                AddURLBtn.IsEnabled = true;
                Cat = AllUsedURLCat.SelectedItem.ToString();
            }
            
        }

        private void RemoveBTn_Click(object sender, RoutedEventArgs e)
        {
            if(ISDomain == false)
            {
                if(DS.Tables.Contains("URLCategury") == false)
                {
                    MainWindow.DataBaseAgent.SelectDataWithCommand("Select * From URLCategury where ChildID ='" + Main.ChildUser + "'", ref DS, "URLCategury");
                }
                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From UsedColor where Name='" + DS.Tables["URLCategury"].Rows[AllUsedURLCat.SelectedIndex]["Color"].ToString() + "'");
                MainWindow.DataBaseAgent.ExequteWithCommand("Delete From URLCategury where Name='" + AllUsedURLCat.SelectedItem + "'");
                DS.Tables["URLCategury"].Rows.RemoveAt(AllUsedURLCat.SelectedIndex);
                MainWindow.DataBaseAgent.UpdateData(DS.Tables["URLCategury"]);
                AllUsedURLCat.Items.RemoveAt(AllUsedURLCat.SelectedIndex);
            }
            else
            {
                DS.Tables["URLCategury"].Rows[0]["URLs"] = 
                    DS.Tables["URLCategury"].Rows[0]["URLs"].ToString().Replace((AllUsedURLCat.SelectedItem.ToString() +
                    ","),"");
                MainWindow.DataBaseAgent.UpdateData(DS.Tables["URLCategury"]);
                AllUsedURLCat.Items.RemoveAt(AllUsedURLCat.SelectedIndex);
            }
        }
    }
}
