
using Npgsql;
using System;
using System.Configuration;
using System.Data;

namespace Usermngt.BL.DataUtility
{
    public abstract class BaseDataLayer
    {
        protected string ConnectionString => ConfigurationManager.ConnectionStrings["CASEMGMT"].ConnectionString;

        protected NpgsqlConnection GetSqlConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }

        protected NpgsqlCommand GetSqlCommand(string commandText, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, GetSqlConnection())
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        protected NpgsqlCommand GetSqlCommandWithTransaction(string commandText, NpgsqlTransaction transaction, CommandType commandType = CommandType.Text)
        {
            var sqlCommand = new NpgsqlCommand(commandText, transaction.Connection, transaction)
            {
                CommandType = commandType
            };
            return sqlCommand;
        }

        protected string GetString(NpgsqlDataReader reader, string columnName)
        {
            string stringValue = default(string);
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                stringValue = reader.GetString(reader.GetOrdinal(columnName));
            }
            return stringValue;
        }

        protected bool GetBoolean(NpgsqlDataReader reader, string columnName)
        {
            bool boolValue = default(bool);
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                boolValue = reader.GetBoolean(reader.GetOrdinal(columnName));
            }
            return boolValue;
        }

        protected string GetString(NpgsqlDataReader reader, string columnName, bool checkHasColumn)
        {
            string stringValue = default(string);
            if (HasColumn(reader, columnName) && !reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                stringValue = HasColumn(reader, columnName) ? reader.GetString(reader.GetOrdinal(columnName)) : default(string);
            }
            return stringValue;
        }

        protected int GetInt(NpgsqlDataReader reader, string columnName)
        {
            int intValue = default(int);
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                intValue = reader.GetInt32(reader.GetOrdinal(columnName));
            }
            return intValue;
        }

        protected long GetInt64(NpgsqlDataReader reader, string columnName)
        {
            long intValue = default(long);
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                intValue = reader.GetInt64(reader.GetOrdinal(columnName));
            }
            return intValue;
        }

        protected DateTime GetDateTime(NpgsqlDataReader reader, string columnName)
        {
            DateTime dateTimeValue = default(DateTime);
            if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
            {
                dateTimeValue = reader.GetDateTime(reader.GetOrdinal(columnName));
            }
            return dateTimeValue;
        }


        private bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;

        }
    }
}
