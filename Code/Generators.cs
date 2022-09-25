using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CuriousLib
{
    /// <summary>
    /// Class to generate DataSet collection members
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Generators<T>
    {
        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<double> dataset, DataGridView dataGridView, int ColumnIndex)
        {
            int rows = dataGridView.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    double value = Double.Parse(dataGridView[ColumnIndex, i].Value.ToString());
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<string> dataset, DataGridView dataGridView)
        {
            int rows = dataGridView.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    string value = dataGridView[0, i].Value.ToString();
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<T> dataset, DataGridView dataGridView)
        {
            int rows = dataGridView.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    T value = (T)dataGridView[0, i].Value;
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating collection members from WinForms ListBox Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="listbox"></param>
        public static void GeneratingFromListBox(DataSet<double> dataset, ListBox listbox)
        {
            int items = listbox.Items.Count;

            for (int i = 0; i < items; i++)
            {
                double value = Double.Parse(listbox.Items[i].ToString());
                dataset.Add(value);
            }
        }

        /// <summary>
        /// Generating collection members from WinForms ListBox Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="listbox"></param>
        public static void GeneratingFromListBox(DataSet<string> dataset, ListBox listbox)
        {
            int items = listbox.Items.Count;

            for (int i = 0; i < items; i++)
            {
                string value = listbox.Items[i].ToString();
                dataset.Add(value);
            }
        }

        /// <summary>
        /// Generating collection members from WinForms ListBox Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="listbox"></param>
        public static void GeneratingFromListBox(DataSet<T> dataset, ListBox listbox)
        {
            int items = listbox.Items.Count;

            for (int i = 0; i < items; i++)
            {
                T value = (T)listbox.Items[i];
                dataset.Add(value);
            }
        }

        /// <summary>
        /// Generating collection members from SQL Server
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromSQL(DataSet<double> dataset, SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = queryString;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            
            if (sqlDataReader.FieldCount == 1)
            {
                while (sqlDataReader.Read())
                {
                    double value = (double)sqlDataReader.GetValue(0);
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("SQL Query result is not one dimensional");
            }
        }

        /// <summary>
        /// Generating collection members from SQL Server
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromSQL(DataSet<string> dataset, SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = queryString;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.FieldCount == 1)
            {
                while (sqlDataReader.Read())
                {
                    string value = sqlDataReader.GetValue(0).ToString();
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("SQL Query result is not one dimensional");
            }
        }

        /// <summary>
        /// Generating collection members from SQL Server
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromSQL(DataSet<T> dataset, SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = queryString;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.FieldCount == 1)
            {
                while (sqlDataReader.Read())
                {
                    T value = (T)sqlDataReader.GetValue(0);
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("SQL Query result is not one dimensional");
            }
        }

        /// <summary>
        /// Generating collection members from MS Access Database
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="oleDbConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromAccessDB(DataSet<double> dataset, OleDbConnection oleDbConnection, string queryString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = oleDbConnection;
            oleDbCommand.CommandText = queryString;
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.FieldCount == 1)
            {
                while (oleDbDataReader.Read())
                {
                    double value = (double)oleDbDataReader.GetValue(0);
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("OleDb Query result is not one dimensional");
            }
        }

        /// <summary>
        /// Generating collection members from MS Access Database
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="oleDbConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromAccessDB(DataSet<string> dataset, OleDbConnection oleDbConnection, string queryString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = oleDbConnection;
            oleDbCommand.CommandText = queryString;
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.FieldCount == 1)
            {
                while (oleDbDataReader.Read())
                {
                    string value = oleDbDataReader.GetValue(0).ToString();
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("OleDb Query result is not one dimensional");
            }
        }

        /// <summary>
        /// Generating collection members from MS Access Database
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="oleDbConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromAccessDB(DataSet<T> dataset, OleDbConnection oleDbConnection, string queryString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = oleDbConnection;
            oleDbCommand.CommandText = queryString;
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.FieldCount == 1)
            {
                while (oleDbDataReader.Read())
                {
                    T value = (T)oleDbDataReader.GetValue(0);
                    dataset.Add(value);
                }
            }
            else
            {
                throw new Exception("OleDb Query result is not one dimensional");
            }
        }
    }
}
