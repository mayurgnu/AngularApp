using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AngularApp.HelperClass
{
    public class DatabaseHelper : IDisposable
    {
        /// <summary>
        /// To get and set sqlconnection object
        /// </summary>
        private SqlConnection objConnection;

        /// <summary>
        /// To get and set sqlcommand object
        /// </summary>
        private SqlCommand objCommand;

        /// <summary>
        /// To get and set sqlbulkcopy object
        /// </summary>
        public SqlBulkCopy bulkCopy;

        public bool HasError { get; set; } = false;

        /*public DatabaseHelper()
        {
            
            //objConnection = new SqlConnection(@"data source=192.168.9.113,1433;initial catalog=InchCapeDB;persist security info=True;user id=sa;password=CDI.1234;");
            objConnection = new SqlConnection(ConfigurationSettings.ConnectionStrings.Value.strDBConn.ToString());
            objCommand = new SqlCommand();
            objCommand.CommandTimeout = 120;
            objCommand.Connection = objConnection;
        }
        public DatabaseHelper(bool IsBulCopy = false, bool IsKeepIdentityBulCopy = false)
        {
            objConnection = new SqlConnection(ConfigurationSettings.ConnectionStrings.Value.strDBConn.ToString());
            objCommand = new SqlCommand();
            objCommand.CommandTimeout = 120;
            objCommand.Connection = objConnection;
            if (IsBulCopy)
            {
                if (IsKeepIdentityBulCopy)
                {
                    bulkCopy = new SqlBulkCopy(objConnection,SqlBulkCopyOptions.KeepIdentity,null);
                }
                else
                {
                    bulkCopy = new SqlBulkCopy(objConnection, SqlBulkCopyOptions.UseInternalTransaction,null);
                }
            }
        }*/
        /// <summary>
        /// Initializes SqlConnection object & SqlCommand object
        /// </summary>
        /// <param name="strDBConn">database connection string</param>
        public DatabaseHelper(string strDBConn)
        {
            objConnection = new SqlConnection(strDBConn);
            objCommand = new SqlCommand
            {
                CommandTimeout = 120,
                Connection = objConnection
            };
        }

        /// <summary>
        /// Initializes SqlConnection object & SqlCommand object
        /// </summary>
        /// <param name="strDBConn">connection string</param>
        /// <param name="IsBulCopy">Is operation being performed with bulk data or not</param>
        /// <param name="IsKeepIdentityBulCopy"></param>
        public DatabaseHelper(string strDBConn, bool IsBulCopy = false, bool IsKeepIdentityBulCopy = false)
        {
            objConnection = new SqlConnection(strDBConn);
            objCommand = new SqlCommand
            {
                CommandTimeout = 120,
                Connection = objConnection
            };
            if (IsBulCopy)
            {
                if (IsKeepIdentityBulCopy)
                {
                    bulkCopy = new SqlBulkCopy(objConnection, SqlBulkCopyOptions.KeepIdentity, null);
                }
                else
                {
                    bulkCopy = new SqlBulkCopy(objConnection, SqlBulkCopyOptions.UseInternalTransaction, null);
                }
            }
        }

        /// <summary>
        /// Adds parameter to sqlparameter object
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter name</param>
        /// <returns>Return sqlparameter object</returns>
        public SqlParameter AddParameter(string name, object value)
        {
            SqlParameter p = objCommand.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if ((value != null) && !(value is DBNull) && (value.ToString().Length > p.Size))
                    p.Size = -1;
            }
            return objCommand.Parameters.Add(p);
        }

        /// <summary>
        /// Adds parameter to sqlparameter object
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter name</param>
        /// <param name="type">parameter typr (InPut, Output or InPutOutput)</param>
        /// <returns>Return sqlparameter object</returns>
        public SqlParameter AddParameter(string name, object value, ParamType type)
        {
            SqlParameter p = objCommand.CreateParameter();
            if (type == ParamType.Output)
                p.Direction = ParameterDirection.Output;
            p.ParameterName = name;
            p.Value = value;
            if ((p.SqlDbType == SqlDbType.VarChar) || (p.SqlDbType == SqlDbType.NVarChar))
            {
                p.Size = (p.SqlDbType == SqlDbType.VarChar) ? 8000 : 4000;

                if ((value != null) && !(value is DBNull) && (value.ToString().Length > p.Size))
                    p.Size = -1;
            }
            return objCommand.Parameters.Add(p);
        }

        /// <summary>
        /// adds SqlParameter object to SqlCommand object
        /// </summary>
        /// <param name="parameter">Sqlparameter to be add in to SqlCommand object</param>
        /// <returns>Return sqlparameter object</returns>
        public SqlParameter AddParameter(SqlParameter parameter)
        {
            return objCommand.Parameters.Add(parameter);
        }

        /// <summary>
        /// Clears(Removes) all sqlparameters from sqlcommand object
        /// </summary>
        public void ClearParameters()
        {
            objCommand.Parameters.Clear();
        }
        //Command Property Of type SqlCommand
        public SqlCommand Command
        {
            get { return objCommand; }
        }

        /// <summary>
        /// Writes Bulk data conataine datatable dt object to server
        /// </summary>
        /// <param name="dt">Datatable containing bulk data</param>
        /// <returns>Returns true or false as per operation success or failure</returns>
        public bool ExecuteBulkCopy(DataTable dt)
        {
            bool result = false;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }

                bulkCopy.BulkCopyTimeout = 0;

                bulkCopy.WriteToServer(dt);

                result = true;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, "ExecuteBulkCopy");
            }
            finally
            {
                bulkCopy.Close();

                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Opens sqlconnection and begins sql transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
            objCommand.Transaction = objConnection.BeginTransaction();
        }

        /// <summary>
        /// Commits current sql transaction if all operations of that transaction are successfully executed
        /// </summary>
        public void CommitTransaction()
        {
            objCommand.Transaction.Commit();
            objConnection.Close();
        }

        /// <summary>
        /// Rollback current sql transaction if any single operations of that transaction failed to execute
        /// </summary>
        public void RollbackTransaction()
        {
            objCommand.Transaction.Rollback();
            objConnection.Close();
        }

        /// <summary>
        /// To call ExecuteNonQuery() method and returns integer value
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <returns>Rerurns object result</returns>
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text);
        }

        /// <summary>
        /// Executes SqlCommand.ExecuteNonQuery() method and returns integer value
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <param name="commandtype">Type of command (stored procedure or sql query)</param>
        /// <returns>Rerurns object result</returns>
        public int ExecuteNonQuery(string query, CommandType commandtype)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            int i = -1;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                    i = sx.Number;
                else
                    HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return i;
        }

        /// <summary>
        /// Executes SqlCommand.ExecuteNonQuery() method and result returned in outputparam(SqlParameter) will be returned as string value.
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <param name="commandtype">Type of command (stored procedure or sql query)</param>
        /// <param name="outputparam">Output parameter used in sql query/stored procedure</param>
        /// <returns>Rerurns object result</returns>
        public string ExecuteNonQuery(string query, CommandType commandtype, string outputparam)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            string outputParamValue = "";
            int i;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                i = objCommand.ExecuteNonQuery();
                outputParamValue = objCommand.Parameters[outputparam].Value.ToString();
            }
            catch (Exception ex)
            {
                SqlException sx = (SqlException)ex;
                if (sx.Number == 547)       // Foreign Key Error
                {
                    i = sx.Number;
                }
                else
                {
                    objCommand.Transaction.Rollback();
                    HandleExceptions(ex, query);
                }
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return outputParamValue;
        }

        /// <summary>
        /// To call ExecuteScalar() method to get object result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <returns>Rerurns object result</returns>
        public object ExecuteScalar(string query)
        {
            return ExecuteScalar(query, CommandType.Text);
        }

        /// <summary>
        /// To get object result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <param name="commandtype">Type of command (stored procedure or sql query)</param>
        /// <returns>Rerurns object result</returns>
        public object ExecuteScalar(string query, CommandType commandtype)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            object o = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                o = objCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return o;
        }

        /// <summary>
        /// To call ExecuteReader() method to get SqlDataReader object result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <returns>Rerurns sql data reader objectresult</returns>
        public SqlDataReader ExecuteReader(string query)
        {
            return ExecuteReader(query, CommandType.Text);
        }

        /// <summary>
        /// To get SqlDataReader object result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <param name="commandtype">Type of command (stored procedure or sql query)</param>
        /// <returns>Rerurns sql data reader object result</returns>
        public SqlDataReader ExecuteReader(string query, CommandType commandtype)
        {
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            SqlDataReader reader = null;
            try
            {
                if (objConnection.State == System.Data.ConnectionState.Closed)
                {
                    objConnection.Open();
                }
                reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return reader;
        }

        /// <summary>
        /// To call ExecuteDataSet() method to get Dataset result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <returns>Rerurns dataset result</returns>
        public DataSet ExecuteDataSet(string query)
        {
            return ExecuteDataSet(query, CommandType.Text);
        }

        /// <summary>
        /// To get Dataset result by executing sql query
        /// </summary>
        /// <param name="query">Sql query to be execute</param>
        /// <param name="commandtype">Type of command (stored procedure or sql query)</param>
        /// <returns>Rerurns dataset result</returns>
        public DataSet ExecuteDataSet(string query, CommandType commandtype)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            objCommand.CommandText = query;
            objCommand.CommandType = commandtype;
            adapter.SelectCommand = objCommand;
            DataSet ds = new DataSet();
            try
            {
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, query);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// To get datatable result by executing stored procedure
        /// </summary>
        /// <param name="Spname">Stored procedure to be execute</param>
        /// <param name="para">Sql parameters used in stored procedure</param>
        /// <returns>Rerurns datatable result</returns>
        public DataTable FetchDataTableBySP(string Spname, SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddRange(para);
            objCommand.CommandText = Spname;
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            try
            {
                objConnection.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// To get datatable result by executing sql query 
        /// </summary>
        /// <param name="Qry"> query to be execute</param>
        /// <param name="para">Sql parameters used in query</param>
        /// <returns>Rerurns datatable result</returns>
        public DataTable FetchDataTableByQuery(string Qry, SqlParameter[] para = null)
        {
            DataTable dt = new DataTable();
            objCommand.CommandType = CommandType.Text;
            if (para != null)
                objCommand.Parameters.AddRange(para);
            objCommand.CommandText = Qry;
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            try
            {
                objConnection.Open();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Qry);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// To get object result by executing sql query 
        /// </summary>
        /// <param name="Qry"> query to be execute</param>
        /// <param name="para">Sql parameters used in query</param>
        /// <returns>Rerurns object result</returns>
        public object FetchObjectByQuery(string Qry, SqlParameter[] para = null)
        {
            objCommand.CommandType = CommandType.Text;
            objCommand.Parameters.AddRange(para);
            objCommand.CommandText = Qry;
            object o = null;
            try
            {
                objConnection.Open();
                o = objCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Qry);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return o;
        }

        /// <summary>
        /// Get list of generic type T from databse
        /// </summary>
        /// <param name="Spname">Sql storedprocedure name to be execute</param>
        /// <param name="para">Array of sql parameter</param>
        /// <returns>Returns list of generic type T</returns>

        public List<T> FetchListBySP<T>(string Spname, SqlParameter[] para)
        {
            List<T> lst = new List<T>();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddRange(para);
            objCommand.CommandText = Spname;
            try
            {
                objConnection.Open();
                var dr = objCommand.ExecuteReader();
                while (dr.Read())
                {
                    T item = CommonFunctions.GetListItem<T>(dr);
                    lst.Add(item);
                }
                return lst;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Spname);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return lst;
        }

        /// <summary>
        /// Get list of generic type T from databse
        /// </summary>
        /// <param name="Qry">Sql query be execute</param>
        /// <param name="para">Array of sql parameter</param>
        /// <returns>Returns list of generic type T</returns>
        public List<T> FetchListByQuery<T>(string Qry, SqlParameter[] para = null)
        {
            List<T> lst = new List<T>();
            objCommand.CommandType = CommandType.Text;
            objCommand.Parameters.AddRange(para);
            objCommand.CommandText = Qry;
            try
            {
                objConnection.Open();
                var dr = objCommand.ExecuteReader();
                while (dr.Read())
                {
                    T item = CommonFunctions.GetListItem<T>(dr);
                    lst.Add(item);
                }
                return lst;
            }
            catch (Exception ex)
            {
                HandleExceptions(ex, Qry);
            }
            finally
            {
                //objCommand.Parameters.Clear();
                if (objCommand.Transaction == null)
                {
                    objConnection.Close();
                }
            }
            return lst;
        }

        /// <summary>
        /// Make call to WriteToLog() method for to log exception 
        /// </summary>
        /// <param name="ex">exception object</param>
        /// <param name="query">query name</param>
        private void HandleExceptions(Exception ex, string query)
        {
            HasError = true;
            WriteToLog(ex.Message, query);
        }

        /// <summary>
        /// Creates a txt file and writes log with exception and query being executed into that file. 
        /// </summary>
        /// <param name="msg">Error message</param>
        /// <param name="query">query to be execute</param>
        private void WriteToLog(string msg, string query)
        {
            try
            {
                string FileName = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                if (!Directory.Exists("Logs"))
                    Directory.CreateDirectory("Logs");
                if (!File.Exists("Logs/" + FileName + ".txt"))
                {
                    using (StreamWriter sw = File.CreateText("Logs/" + FileName + ".txt"))
                    {
                        sw.WriteLine("Date and Time : " + DateTime.Now.ToString() + " - " + msg + "||||Error in Query : " + query);
                        sw.Flush();
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("Logs/" + FileName + ".txt"))
                    {
                        sw.WriteLine("Date and Time : " + DateTime.Now.ToString() + " - " + msg + "||||Error in Query : " + query);
                        sw.Flush();
                        sw.Close();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Clears sql parameters from SqlCommand object closes SqlConnection
        /// and disposes both SqlCommand and SqlConnection object
        /// </summary>
        public void Dispose()
        {
            objCommand.Parameters.Clear();
            objConnection.Close();
            objConnection.Dispose();
            objCommand.Dispose();
        }

        /// <summary>
        /// To maintain sqlparamter type  
        /// </summary>
        public enum ParamType
        {
            Input, Output, InputOutput
        }
    }
}

