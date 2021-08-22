using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Task4_Hr.DB_Manage
{
    public class Connection
    {
        public OracleConnection aOracleConnection { get; private set; }

        public Connection()
        {
            aOracleConnection = GetConnection();
        }
        public OracleConnection GetCon()
        {
            return aOracleConnection;
        }
        public OracleConnection GetConnection()
        {
            string db_username = "hr_user";
            string db_password = "hr_user";
            if (aOracleConnection != null)
            {
                if (aOracleConnection.State != ConnectionState.Open)
                    aOracleConnection.Open();
            }
            else
            {
                string ip = "localhost:1521/orcl";
                if (aOracleConnection == null)
                    aOracleConnection = new OracleConnection("data source=" + ip + ";password=" + db_password + ";persist security info=True;user id=" + db_username + ";pooling=true;");
                aOracleConnection.Open();
            }

            return aOracleConnection;
      
        }
        public OracleTransaction CreateTransaction()
        {
            return aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void Close(OracleConnection aOracleConnection)
        {
            if (aOracleConnection != null && aOracleConnection.State == ConnectionState.Open)
            {
                aOracleConnection.Close();
            }
        }
    }
}