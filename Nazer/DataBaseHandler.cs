using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;

namespace UI
{
    public class DataBaseHandler
    {
        private List<SqlConnection> Connectios;
        private int NumberOfConnections;
        private int ConnectiosIndex;
        private string connectionString;
        Semaphore SM;
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }
        public int ConnectionsIndex
        {
            get
            {
                SM.WaitOne();
                ConnectiosIndex++;
                if ((ConnectiosIndex >= NumberOfConnections))
                {
                    ConnectiosIndex = 0;
                }
                SM.Release();
                return ConnectiosIndex;
            }
            set
            {
                //ConnectiosIndex = value;
                SM.WaitOne();
                if ((value > NumberOfConnections ) || (ConnectiosIndex >= NumberOfConnections -1 ))
                {
                    ConnectiosIndex = 0;
                }
                SM.Release();
            }
        }
        public bool CheckDataBase(string Conn)
        {
            try
            {
                SqlConnection Connect = new SqlConnection(Conn);
                Connect.Open();
                return true;
            }
            catch(Exception E)
            {
                return false;
            }
        }
        public DataBaseHandler(string Conn , int NumberOfConnections)
        {
            connectionString = Conn;
            SM = new Semaphore(1, 1);
            Connectios = new List<SqlConnection>();
            ConnectiosIndex = -1;
            this.NumberOfConnections = NumberOfConnections;
            for(int i = 0; i < NumberOfConnections; i++)
            {
                SqlConnection Connection = new SqlConnection(Conn);
                Connectios.Add(Connection);
            }
        }
        /// <summary>
        /// Update a Row in Table with generate query
        /// </summary>
        /// <param name="Table"> table that have specific Row to update that </param> 
        /// <param name="Row"> Row that must be Update </param>
        /// <param name="RToL"> if have Data that Must Write Right to Left Must be true</param>
        /// <returns></returns>
        public bool UpdateTable(DataTable Table, DataRow Row, bool RToL)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Connectios[ConnectionsIndex];
                Command.CommandText = ("Update " + Table.TableName + " Set ");
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    if (Row[i].ToString() != "")
                    {
                        DataColumn Column = Table.Columns[i];
                        if (RToL == true)
                        {
                            Command.CommandText += (Column.ColumnName + " = N'" + Row[i].ToString() + "', ");
                        }
                        else
                        {
                            Command.CommandText += (Column.ColumnName + " = '" + Row[i].ToString() + "', ");
                        }
                    }
                }
                Command.CommandText = Command.CommandText.Remove(Command.CommandText.Length - 2, 1);
                DataColumn PColumn = Table.PrimaryKey[0];
                Command.CommandText = Command.CommandText.Remove(Command.CommandText.Length - 1);
                Command.CommandText += (" Where " + PColumn.ColumnName + " = '" + Row[0] + "' and ChildID = N'" + Row[2] + "'");
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception E)
            {
                return false;
            }

        }
        /// <summary>
        /// insert a Row into Table with generating query
        /// </summary>
        /// <param name="Table">table that must insert Row into it </param>
        /// <param name="Row">Row that must be inert into Table</param>
        /// <param name="RToL">f have Data that Must Write Right to Left Must be true</param>
        /// <param name="All">if true All Column have valid Data if false just Notnull Column have valid Data</param>
        /// <returns></returns>
        public bool InsertData(DataTable Table, DataRow Row, bool RToL, bool All)
        {
            try
            {
                SqlCommand Command = new SqlCommand();
                Command.Connection = Connectios[ConnectionsIndex];
                if (All == true)
                {
                    Command.CommandText += ("insert into " + Table.TableName + " VALUES (");
                    for (int i = 0; i < Table.Columns.Count; i++)
                    {
                        if (RToL == true)
                        {
                            Command.CommandText += ("N'" + Row[i].ToString() + "',");
                        }
                        else
                        {
                            Command.CommandText += ("'" + Row[i].ToString() + "',");
                        }

                    }
                }
                else
                {
                    Command.CommandText += ("insert into " + Table.TableName + " (");
                    for (int i = 0; i < Table.Columns.Count; i++)
                    {
                        if (Row[i].ToString() != "")
                        {
                            DataColumn InsertColumn = (DataColumn)Row[i];
                            Command.CommandText += (InsertColumn.ColumnName + ",");
                        }

                    }
                    Command.CommandText = Command.CommandText.Replace(Command.CommandText[Command.CommandText.Length - 1], ')');
                    Command.CommandText += "Set (";
                    for (int i = 0; i < Table.Columns.Count; i++)
                    {
                        if (Row[i].ToString() != "")
                        {
                            if (RToL == true)
                            {
                                Command.CommandText += "N'" + Row[i].ToString() + "',";
                            }
                            else
                            {
                                Command.CommandText += "'" + Row[i].ToString() + "',";
                            }
                        }
                    }
                }
                Command.CommandText = Command.CommandText.Remove(Command.CommandText.Length - 1);
                Command.CommandText += ")";
                Command.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// insert a Row into Table with generating query
        /// </summary>
        /// <param name="Table">table that must insert Row into it </param>
        /// <param name="Row">Row that must be inert into Table</param>
        /// <param name="RToL">f have Data that Must Write Right to Left Must be true</param>
        /// <param name="All">if true All Column have valid Data if false just Notnull Column have valid Data</param>
        /// <returns></returns>
        public bool InsertData(DataTable Table)
        {
            try
            {

                SqlCommand FreeCommand = new SqlCommand();
                FreeCommand.Connection = Connectios[ConnectionsIndex];
                FreeCommand.CommandText = (" Select  * From " + Table.TableName);
                SqlDataAdapter DA = new SqlDataAdapter(FreeCommand);
                SqlCommandBuilder Command = new SqlCommandBuilder(DA);
                DA.InsertCommand = Command.GetInsertCommand(true);
                //DA.UpdateCommand = Command.GetUpdateCommand(true);
                DA.Update(Table);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool UpdateData(DataTable Table)
        {
            try
            {

                SqlCommand FreeCommand = new SqlCommand();
                FreeCommand.Connection = Connectios[ConnectionsIndex];
                FreeCommand.CommandText = ("Select * From " + Table.TableName);
                SqlDataAdapter DA = new SqlDataAdapter(FreeCommand);
                SqlCommandBuilder Command = new SqlCommandBuilder(DA);
                //DA.InsertCommand = Command.GetInsertCommand(true);
                //DA.DeleteCommand = Command.GetDeleteCommand(true);
                DA.UpdateCommand = Command.GetUpdateCommand(true);
                DA.Update(Table);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Copy a lot of Data That are in same Table 
        /// </summary>
        /// <param name="DestinationTable">Destination Table Name That Data Should Store on This Table</param>
        /// <param name="SourceTable">Data Table object That Has a lot of Rows </param>
        public void InsertBulkData(string DestinationTable , DataTable SourceTable)
        {
            using (SqlBulkCopy Bulk = new SqlBulkCopy(Connectios[ConnectionsIndex]))
            {
                Bulk.DestinationTableName = DestinationTable;
                Bulk.WriteToServer(SourceTable);
            }
                

        }
        /// <summary>
        /// Fill Data Set with specific Table
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set </param>
        /// <param name="DS">Data Set Taht must be fill with Data </param>
        public void FillDataSet(string TableName, ref DataSet DS)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            SqlDataAdapter DA = new SqlDataAdapter("Select * From " + TableName, Connectios[Index]);
            DA.Fill(DS, TableName);
        }
        /// <summary>
        /// Fill Data Set with specific Row(s)
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set and search in it</param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="PrimaryKey">Primary Key Column Name </param>
        /// <param name="Target"> Data that must equal with Primary Key</param>
        /// <param name="SelecttedDataTable"> Name for new Table that have selected Row(s)</param>
        public void FillDataSet(string TableName, ref DataSet DS, DataColumn PrimaryKey, string Target, string SelecttedDataTable)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            SqlDataAdapter DA = new SqlDataAdapter("Select * From " + TableName + " where " + PrimaryKey + " = '" + Target + "'", Connectios[Index]);
            DA.Fill(DS, SelecttedDataTable);
        }
        /// <summary>
        /// Fill Data Set with Specific Data
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set and search in it</param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="SelecttedDataTable">Name for new Table that have selected Row(s)</param>
        public void SelectData(string TableName, ref DataSet DS, string SelecttedDataTable)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }

            SqlDataAdapter DA = new SqlDataAdapter("Select * From " + TableName, Connectios[Index]);
            DA.Fill(DS, SelecttedDataTable);
        }
        /// <summary>
        /// Fill Data Set with Specific Data from Specific Columns 
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set and search in it</param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="ColumnsName">the string that have Columns Name To Select Data From Them</param>
        /// <param name="SelecttedDataTable">Name for new Table that have selected Row(s)</param>
        public void SelectData(string TableName, ref DataSet DS, string ColumnsName, string SelecttedDataTable)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            SqlDataAdapter DA = new SqlDataAdapter("Select " + ColumnsName + " from " + TableName, Connectios[Index]);
            DA.Fill(DS, SelecttedDataTable);
        }
        /// <summary>
        /// Fill Data Set with Specific Data from Specific Columns and have Conditin
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set and search in it</param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="ColumnsName">the string that have Columns Name To Select Data From Them</param>
        /// <param name="SelecttedDataTable">Name for new Table that have selected Row(s)</param>
        /// <param name="Target">Data that must equal with Specific Column Data</param>
        /// <param name="ConditinColumn">Name of Column that Must Filter Data</param>
        public void SelectData(string TableName, ref DataSet DS, string ColumnsName, string SelecttedDataTable, string Target, string ConditinColumn)
        {
            try
            {
                int Index = ConnectionsIndex;
                if (Connectios[Index].State != ConnectionState.Open)
                {
                    Connectios[Index].Open();
                }
                SqlDataAdapter DA = new SqlDataAdapter("Select " + ColumnsName + " from " + TableName + " where " + ConditinColumn + " = " + "'" + Target + "'", Connectios[Index]);
                DA.Fill(DS, SelecttedDataTable);
            }
            catch(Exception E)
            {

            }
            
        }
        /// <summary>
        /// Fill Data Set with Specific Data from Specific Columns and have Conditin And Option for Sort And Grouping
        /// </summary>
        /// <param name="TableName">table name that must use to fill Data Set and search in it</param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="ColumnsName">the string that have Columns Name To Select Data From Them</param>
        /// <param name="SelectedDataTable">Name for new Table that have selected Row(s)</param>
        /// <param name="Target">Data that must equal with Specific Column Data</param>
        /// <param name="ConditinColumn">Name of Column that Must Filter Data</param>
        /// <param name="GroupBy">Name Of Column that you Want to Grouping Data with them</param>
        /// <param name="OrderBy">Name Of Column that you Want to Sort Data with them</param>
        public void SelectData(string TableName, ref DataSet DS, string ColumnsName, string SelectedDataTable, string Target, string ConditinColumn,
            string GroupBy, string OrderBy)
        {
            int Index = ConnectionsIndex;
            string Command = "Select ";
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            if (ColumnsName == "")
            {
                Command += (" * From " + TableName + " ");
            }
            else
            {
                Command += (ColumnsName + " From " + TableName + " ");
            }
            if (GroupBy != "")
            {
                Command += (" Group By " + GroupBy);
            }
            if (OrderBy != "")
            {
                Command += (" Order By " + OrderBy);
            }
            if (ConditinColumn != "")
            {
                Command += ("where" + ConditinColumn + " = '" + Target + "'");
            }
            SqlDataAdapter DA = new SqlDataAdapter(Command, Connectios[Index]);
            DA.Fill(DS, SelectedDataTable);
        }
        /// <summary>
        /// Exequte Query with Specific Command
        /// </summary>
        /// <param name="Command">Query that yiu want to exequte </param>
        /// <param name="DS">Data Set Taht must be fill with selected Data</param>
        /// <param name="SelectedDataTable">Name for new Table that have selected Row(s)</param>
        public void SelectDataWithCommand(string Command, ref DataSet DS , string SelectedDataTable)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            SqlDataAdapter DA = new SqlDataAdapter(Command, Connectios[Index]);
            DA.Fill(DS, SelectedDataTable);
        }
        public void ExequteWithCommand(string Command)
        {
            try
            {
                int Index = ConnectionsIndex;
                if (Connectios[Index].State != ConnectionState.Open)
                {
                    Connectios[Index].Open();
                }
                SqlCommand Commands = new SqlCommand();
                Commands.Connection = Connectios[Index];
                Commands.CommandText = Command;
                Commands.ExecuteNonQuery();
            }
            catch(Exception E)
            {
                string D = Command;
            }
           
        }
        public object ExequteWithCommandScaler(string Command)
        {
            int Index = ConnectionsIndex;
            if (Connectios[Index].State != ConnectionState.Open)
            {
                Connectios[Index].Open();
            }
            SqlCommand Commands = new SqlCommand();
            Commands.Connection = Connectios[Index];
            Commands.CommandText = Command;
            //object DSs = Commands.ExecuteScalar();
            return (Commands.ExecuteScalar());
        }
    }
}
