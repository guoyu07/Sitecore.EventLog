using SitecoreEventLog.Website.Configuration;
using SitecoreEventLog.Website.DataAccess.Extension;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SitecoreEventLog.Website.DataAccess
{
    public class SitecoreEventLogDatabaseCommand : IDisposable
    {
        
        public SitecoreEventLogDatabaseCommand(string connectionStringKey = null)
        {
            if (string.IsNullOrEmpty(connectionStringKey))
            {
                connectionStringKey = Settings.ConnectionString;
            }

            Connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionStringKey].ToString());
            Command = new SqlCommand();
        }

        private SqlConnection Connection { get; set; }
        private SqlCommand Command { get; set; }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }

            if (Command != null)
            {
                Command.Dispose();
                Command = null;
            }
        }

        private void Open()
        {
            if (Connection.State != ConnectionState.Open)

                Connection.Open();
        }

        public void AddParameter(string key, object value)
        {
            Command.Parameters.Add(new SqlParameter(key, value));
        }

        private void Clear()
        {
            if (Command.Parameters.Count != 0)
                Command.Parameters.Clear();
        }

        public List<T> ExecuteStoredProcedureQuery<T>(string fStoredProcedure)
        {
            Open();
            Command.Connection = Connection;
            Command.CommandText = fStoredProcedure;
            Command.CommandType = CommandType.StoredProcedure;

            var dataset = new DataSet();
            var adapter = new SqlDataAdapter(Command);
            adapter.Fill(dataset);

            var result = dataset.Tables[0].ToClassList<T>();
            Clear();
            return result;
        }

        public object ExecuteStoredProcedureScaler(string fStoredProcedure)
        {
            Open();
            Command.Connection = Connection;
            Command.CommandText = fStoredProcedure;
            Command.CommandType = CommandType.StoredProcedure;

            var result = Command.ExecuteScalar();
            Clear();
            return result;
        }
    }
}