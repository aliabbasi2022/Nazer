using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.SqlClient;
using System.IO;

namespace UI
{
    [RunInstaller(true)]
    public partial class ParentInstaller : System.Configuration.Install.Installer
    {
        System.Data.SqlClient.SqlConnection masterConnection = new System.Data.SqlClient.SqlConnection();
        public ParentInstaller()
        {
            InitializeComponent();
        }
        private string GetSql(string Name)
        {

            try
            {
                // Gets the current assembly.
                Assembly Asm = Assembly.GetExecutingAssembly();

                // Resources are named using a fully qualified name.
                Stream strm = Asm.GetManifestResourceStream(Asm.GetName().Name + "." + Name);

                // Reads the contents of the embedded file.
                StreamReader reader = new StreamReader(strm);
                return reader.ReadToEnd();

            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("In GetSQL: " + ex.Message);
                throw ex;
            }
        }

        private void ExecuteSql(string DatabaseName, string Sql)
        {
            System.Data.SqlClient.SqlCommand Command = new System.Data.SqlClient.SqlCommand(Sql, masterConnection);

            // Initialize the connection, open it, and set it to the "master" database
            masterConnection.ConnectionString = Properties.Settings.Default.masterConnectionString;
            Command.Connection.Open();
            Command.Connection.ChangeDatabase(DatabaseName);
            try
            {
                Command.ExecuteNonQuery();
            }
            finally
            {
                // Closing the connection should be done in a Finally block
                Command.Connection.Close();
            }
        }

        protected void AddDBTable(string strDBName)
        {
            try
            {
                // Creates the database.
                ExecuteSql("master", "CREATE DATABASE " + strDBName);

                // Creates the tables.
                ExecuteSql(strDBName, GetSql("Script.sql"));

            }
            catch (Exception ex)
            {
                // Reports any errors and abort.
                //Interaction.MsgBox("In exception handler: " + ex.Message);
                throw ex;
            }
        }


        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            AddDBTable("ParentDB");
        }

    }
}
