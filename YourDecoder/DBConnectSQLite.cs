using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace YourDecoder
{
    /// <summary>
    /// Handle DB connections for SQLite database
    /// Source: http://www.dreamincode.net/forums/topic/157830-using-sqlite-with-c%23/#/
    /// </summary>
    class DBConnectSQLite
    {
        String dbConnectionString;
        public bool isDBFailed = false;
        public SQLiteConnection DBConnection;
        public string DBLocaSettingPath = "local.s3db";

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public DBConnectSQLite()
        {            
            dbConnectionString = String.Format("Data Source={0}", DBLocaSettingPath);
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public DBConnectSQLite(String inputFile)
        {
            dbConnectionString = String.Format("Data Source={0};Pooling=True;journal mode=WAL", inputFile);
        }

        public void OpenConnection()
        {
            if (this.DBConnection == null)
            {
                DBConnection = new SQLiteConnection(dbConnectionString);
            }
            if (this.DBConnection.State != ConnectionState.Open)
            {
                this.DBConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (this.DBConnection != null && this.DBConnection.State != ConnectionState.Closed)
            {
                this.DBConnection.Close();
            }
        }

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable SelectDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                SQLiteCommand mycommand = new SQLiteCommand(this.DBConnection);
                mycommand.CommandText = sql;
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                //cnn.Close();
            }
            catch (Exception ex)
            {                             
                isDBFailed = true;
                //ex.Message;
            }
            return dt;
        }

        /// <summary>
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {
            OpenConnection();
            SQLiteTransaction transaction = this.DBConnection.BeginTransaction();
            SQLiteCommand mycommand = new SQLiteCommand(this.DBConnection);
            //mycommand.Transaction = this.DBConnection.BeginTransaction();
            mycommand.Transaction = transaction;
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            transaction.Commit();            
            //cnn.Close();
            return rowsUpdated;
        }

        /// <summary>
        /// Generate update query
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dicData"></param>
        /// <param name="PKFieldName">Field name of Primary Key</param>
        /// <returns></returns>
        public string GenerateUpdateQuery(string tableName, Dictionary<string, string> dicData, string PKFieldName)
        {
            string query = "UPDATE " + tableName + " SET ";
            string value;
            foreach (string key in dicData.Keys)
            {
                if (!key.Equals(PKFieldName))
                {
                    value = this.EscapeMySql(dicData[key]);
                    query += key + "='" + value + "',";
                }
            }
            //Remove the last comma
            query = query.Remove(query.Length - 1);
            query += " WHERE " + PKFieldName + "='" + dicData[PKFieldName] + "'";
            return query;
        }

        /// <summary>
        /// Execute update query and return number of rows affected
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dicData"></param>
        /// <param name="PKFieldName">Field name of Primary Key</param>
        /// <returns></returns>
        public int Update(string tableName, Dictionary<string, string> dicData, string PKFieldName)
        {
            string query = this.GenerateUpdateQuery(tableName, dicData, PKFieldName);
            int rowsAffected = this.ExecuteNonQuery(query);
            return rowsAffected;
        }

        //Generate insert query
        public string GenerateInsertQuery(string tableName, Dictionary<string, string> dicData)
        {
            string query = "INSERT INTO " + tableName + " (";
            string value;
            List<string> keyList = new List<string>();
            foreach (string key in dicData.Keys)
            {
                query += key + ",";
                keyList.Add(key);
            }
            query = query.Remove(query.Length - 1); //Remove the last comma
            query += ") VALUES (";
            foreach (string key in keyList)
            {
                value = this.EscapeMySql(dicData[key]);
                query += "'" + value + "',";
            }
            query = query.Remove(query.Length - 1); //Remove the last comma
            query += ")";
            return query;
        }

        //Generate query and insert data
        public int Insert(string tableName, Dictionary<string, string> dicData)
        {
            string query = this.GenerateInsertQuery(tableName, dicData);
            int rowsAffected = this.ExecuteNonQuery(query);
            return rowsAffected;
        }

        //Espace MySQL query
        private string EscapeMySql(string value)
        {
            return value.Replace("'", @"''");
        }

        /// <summary>
        /// Insert if row does not exist, otherwise just update
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dicData"></param>
        /// <param name="PKFieldName">Column name of the primary key</param>
        /// <returns></returns>
        public int UpdateOrInsert(string tableName, Dictionary<string, string> dicData, string PKFieldName)
        {
            string pkValue = dicData[PKFieldName];
            string queryCheck = "SELECT " + PKFieldName + " FROM " + tableName +
                " WHERE " + PKFieldName + " = '" + pkValue + "'";
            DataTable data = this.SelectDataTable(queryCheck);
            if (data.Rows.Count <= 0)
            {
                //Need to insert
                return this.Insert(tableName, dicData);
            }
            else
            {
                //Just update
                return this.Update(tableName, dicData, PKFieldName);
            }
        }
    }
}
