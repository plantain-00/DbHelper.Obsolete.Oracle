using System;
using System.Data;
using System.Data.OracleClient;

namespace DbHelper.Obsolete.Oracle
{
    [Obsolete]
    public static class OracleHelper
    {
        private static string connectionString;
        [Obsolete]
        public static void Connect(string s)
        {
            connectionString = s;
        }
        [Obsolete]
        public static void ExecuteNonQuery(string commandText, params OracleParameter[] parameters)
        {
            if (commandText == null)
            {
                throw new ArgumentNullException("commandText");
            }
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (OracleCommand command = new OracleCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        [Obsolete]
        public static object ExecuteScalar(string commandText, params OracleParameter[] parameters)
        {
            if (commandText == null)
            {
                throw new ArgumentNullException("commandText");
            }
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (OracleCommand command = new OracleCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }
        [Obsolete]
        public static DataSet ExecuteQuery(string commandText, params OracleParameter[] parameters)
        {
            if (commandText == null)
            {
                throw new ArgumentNullException("commandText");
            }
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                using (OracleCommand command = new OracleCommand(commandText, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    DataSet result = new DataSet();
                    OracleDataAdapter adapter = new OracleDataAdapter(commandText, connection)
                                                {
                                                    SelectCommand = command
                                                };
                    adapter.Fill(result, "ds");
                    return result;
                }
            }
        }
        [Obsolete]
        public static DataSet ExecuteQuery(string commandText, int page, int numberPerPage, params OracleParameter[] parameters)
        {
            if (commandText == null)
            {
                throw new ArgumentNullException("commandText");
            }
            if (page <= 0
                || numberPerPage <= 0)
            {
                throw new ArgumentException();
            }
            return ExecuteQuery(@"select * from (select a.*, ROWNUM rn from (" + commandText + ")a where ROWNUM <= " + page * numberPerPage + ") where rn >= " + ((page - 1) * numberPerPage + 1), parameters);
        }
        [Obsolete]
        public static int GetCount(string commandText, params OracleParameter[] parameters)
        {
            if (commandText == null)
            {
                throw new ArgumentNullException("commandText");
            }
            return Convert.ToInt32(ExecuteScalar(@"select count(*) from (" + commandText + ")", parameters));
        }
    }
}