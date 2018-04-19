using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Transactions;
using Npgsql;

namespace WComm
{
    internal class DataFactoryPostgreSQL : DataFactory
    {
        public DataFactoryPostgreSQL(string url, string providerType, string keyValue)
        {
          
                this._conString = keyValue;
        }

        public DataFactoryPostgreSQL(Transaction strans)
        {
            this._trans = strans;

        }

        public override DataSet getDataSet(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(this._conString);
            DataSet _ds = new DataSet();

            try
            {
                conn.Open();

                DataSet ds = new DataSet();

                NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn);
                objAdapter.Fill(ds, "a");
            }
            catch (Npgsql.NpgsqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed && this._trans == null)
                    conn.Close();
            }
            return _ds;
        }

        public override DataTable getDataTable(string sql)
        {

            NpgsqlConnection conn = new NpgsqlConnection(this._conString);
            DataTable _dt = new DataTable();

            DataSet ds = new DataSet();


            try
            {
                if (this._trans == null)
                {
                    conn.Open();

                    NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter(sql, conn);
                    objAdapter.Fill(ds, "a");

                   
                }
                else
                {
                    Npgsql.NpgsqlCommand objCommand = new NpgsqlCommand();



                    //objCommand.Connection = conn;
                    objCommand.Transaction = _trans.ITransaction as Npgsql.NpgsqlTransaction;
                    objCommand.Connection = objCommand.Transaction.Connection;
                    objCommand.CommandText = sql;

                    NpgsqlDataAdapter objAdapter = new NpgsqlDataAdapter();
                    objAdapter.SelectCommand = objCommand;


                    objAdapter.Fill(ds, "a");

                }

                _dt = ds.Tables[0];

            }
            catch (Npgsql.NpgsqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed && this._trans == null)
                    conn.Close();
            }
            return _dt;
        }

        public override int execSql(string sql)
        {
            NpgsqlConnection conn = new NpgsqlConnection(this._conString);

            int retVal = 0;

          
            try
            {
                if (this._trans == null)
                {

                    conn.Open();

                    NpgsqlCommand objCommand = new NpgsqlCommand(sql, conn);

                    retVal = objCommand.ExecuteNonQuery();

                    
                }
                else
                {
                    Npgsql.NpgsqlCommand objCommand = new NpgsqlCommand();
                    
                    objCommand.Transaction = _trans.ITransaction as Npgsql.NpgsqlTransaction;
                    objCommand.Connection = objCommand.Transaction.Connection;
                    objCommand.CommandText = sql;

                    retVal = objCommand.ExecuteNonQuery();

                }


            }
            catch (Npgsql.NpgsqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed && this._trans == null)
                    conn.Close();
            }
            return retVal;
        }

       

    }


    /// <summary>
    /// Database exception class.  This creates a special class that inherits from 
    /// System.Exception and holds information about the exception and additional
    /// information from the DataAccess class as to what, when, and other information
    /// that will be handy in debugging.
    /// </summary>
    internal class PostgreSQLException : System.Exception
    {
        /// <summary>
        /// DBException constructor:  Must be created with additional details
        /// and an underlying exception.
        /// </summary>
        /// <param name="details">Class specific information describing events leanding up to the exception.</param>
        /// <param name="innerException">System exception thrown.</param>
        public PostgreSQLException(string details, Exception innerException)
        {
            //Details of the exception
            _details = details;
            //System exeception thrown
            _inner = innerException;
            //When the exception was thrown.
            _occurred = System.DateTime.Now;

            //			int intCount = ((System.Data.OleDb.OleDbException)_inner).Errors.Count-1;
            //			ErrorCode=((System.Data.OleDb.OleDbException)_inner).Errors[intCount].NativeError;
            //			if(ErrorCode == 50000)
            //			{
            //				ErrorCode = int.Parse(((System.Data.OleDb.OleDbException)_inner).Errors[intCount].Message);
            //			}
            ErrorCode = -1;
        }

        public PostgreSQLException(string details, SqlException innerException)
        {
            //Details of the exception
            _details = details;
            //System exeception thrown
            _inner = innerException;
            //When the exception was thrown.
            _occurred = System.DateTime.Now;

            //			int intCount = ((System.Data.OleDb.OleDbException)_inner).Errors.Count-1;
            //			ErrorCode=((System.Data.OleDb.OleDbException)_inner).Errors[intCount].NativeError;
            //			if(ErrorCode == 50000)
            //			{
            //				ErrorCode = int.Parse(((System.Data.OleDb.OleDbException)_inner).Errors[intCount].Message);
            //			}
            ErrorCode = innerException.Number;
        }

        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns>Compiled information on the exception that was thrown.</returns>
        public override string ToString()
        {
            string retValue = "Exception occurred at: " + _occurred.ToString()
                + "\nDetails: " + _details + "\nInner Exception: " + _inner.ToString();
            return retValue;
        }
        /// <summary>Private data member holding the information about the system exception that was thrown.</summary>
        private Exception _inner;
        /// <summary>Private data member that holds additional information about what happened in the class before the exception was thrown.</summary>
        private string _details;
        /// <summary>Private data member that holds information as to when the exception was thrown.</summary>
        private System.DateTime _occurred;
        public int ErrorCode;
    }
}