using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using CuriousLib.Clustering;
using System.Data;

namespace CuriousLib.Miscellaneous
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
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[ColumnIndex, i].Value != null)
                {
                    double value = double.Parse(dataGridView[ColumnIndex, i].Value.ToString());
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<string> dataset, DataGridView dataGridView, int ColumnIndex)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[ColumnIndex, i].Value != null)
                {
                    string value = dataGridView[ColumnIndex, i].Value.ToString();
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<T> dataset, DataGridView dataGridView, int ColumnIndex)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[ColumnIndex, i].Value != null)
                {
                    T value = (T)dataGridView[ColumnIndex, i].Value;
                    dataset.Add(value);
                }
            }
        }

        /// <summary>
        /// Generating data points for clustering from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="dataGridView"></param>
        /// <param name="CoordinateXColumnIndex"></param>
        /// <param name="CoordinateYColumnIndex"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromDataGridView(DataSet<DataPoint> dataPoints, DataGridView dataGridView, int CoordinateXColumnIndex, int CoordinateYColumnIndex)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[CoordinateXColumnIndex, i].Value != null && dataGridView[CoordinateYColumnIndex, i].Value != null)
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.ID = i;
                    dataPoint.CoordinateX = double.Parse(dataGridView[CoordinateXColumnIndex, i].Value.ToString());
                    dataPoint.CoordinateY = double.Parse(dataGridView[CoordinateYColumnIndex, i].Value.ToString());
                    dataPoints.Add(dataPoint);
                }
            }
        }

        /// <summary>
        /// Generating cluster centers for clustering from WinForms DataGridView Control
        /// </summary>
        /// <param name="clusterCenters"></param>
        /// <param name="dataGridView"></param>
        /// <param name="CoordinateXColumnIndex"></param>
        /// <param name="CoordinateYColumnIndex"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromDataGridView(DataSet<ClusterCenter> clusterCenters, DataGridView dataGridView, int CoordinateXColumnIndex, int CoordinateYColumnIndex)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView[CoordinateXColumnIndex, i].Value != null && dataGridView[CoordinateYColumnIndex, i].Value != null)
                {
                    ClusterCenter clusterCenter = new ClusterCenter();
                    clusterCenter.ID = i;
                    clusterCenter.CoordinateX = double.Parse(dataGridView[CoordinateXColumnIndex, i].Value.ToString());
                    clusterCenter.CoordinateY = double.Parse(dataGridView[CoordinateYColumnIndex, i].Value.ToString());
                    clusterCenters.Add(clusterCenter);
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
                double value = double.Parse(listbox.Items[i].ToString());
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
        /// Generating data points for clustering from SQL Server
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromSQL(DataSet<DataPoint> dataPoints, SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = queryString;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.FieldCount == 2)
            {
                int count = 0;

                while (sqlDataReader.Read())
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.ID = count;
                    dataPoint.CoordinateX = (double)sqlDataReader.GetValue(0);
                    dataPoint.CoordinateY = (double)sqlDataReader.GetValue(1);
                    dataPoints.Add(dataPoint);
                    count += 1;
                }
            }
            else
            {
                throw new Exception("SQL Query result is not two dimensional");
            }
        }

        /// <summary>
        /// Generating cluster centers for clustering from SQL Server
        /// </summary>
        /// <param name="clusterCenters"></param>
        /// <param name="sqlConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromSQL(DataSet<ClusterCenter> clusterCenters, SqlConnection sqlConnection, string queryString)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = queryString;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            if (sqlDataReader.FieldCount == 2)
            {
                int count = 0;

                while (sqlDataReader.Read())
                {
                    ClusterCenter clusterCenter = new ClusterCenter();
                    clusterCenter.ID = count;
                    clusterCenter.CoordinateX = (double)sqlDataReader.GetValue(0);
                    clusterCenter.CoordinateY = (double)sqlDataReader.GetValue(1);
                    clusterCenters.Add(clusterCenter);
                    count += 1;
                }
            }
            else
            {
                throw new Exception("SQL Query result is not two dimensional");
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

        /// <summary>
        /// Generating data points for clustering from MS Access Database
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="oleDbConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromAccessDB(DataSet<DataPoint> dataPoints, OleDbConnection oleDbConnection, string queryString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = oleDbConnection;
            oleDbCommand.CommandText = queryString;
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.FieldCount == 1)
            {
                int count = 0;

                while (oleDbDataReader.Read())
                {
                    DataPoint dataPoint = new DataPoint();
                    dataPoint.ID = count;
                    dataPoint.CoordinateX = (double)oleDbDataReader.GetValue(0);
                    dataPoint.CoordinateY = (double)oleDbDataReader.GetValue(1);
                    dataPoints.Add(dataPoint);
                    count += 1;
                }
            }
            else
            {
                throw new Exception("OleDb Query result is not two dimensional");
            }
        }

        /// <summary>
        /// Generating cluster centers for clustering from MS Access Database
        /// </summary>
        /// <param name="clusterCenters"></param>
        /// <param name="oleDbConnection"></param>
        /// <param name="queryString"></param>
        /// <exception cref="Exception"></exception>
        public static void GeneratingFromAccessDB(DataSet<ClusterCenter> clusterCenters, OleDbConnection oleDbConnection, string queryString)
        {
            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = oleDbConnection;
            oleDbCommand.CommandText = queryString;
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            if (oleDbDataReader.FieldCount == 1)
            {
                int count = 0;

                while (oleDbDataReader.Read())
                {
                    ClusterCenter clusterCenter = new ClusterCenter();
                    clusterCenter.ID = count;
                    clusterCenter.CoordinateX = (double)oleDbDataReader.GetValue(0);
                    clusterCenter.CoordinateY = (double)oleDbDataReader.GetValue(1);
                    clusterCenters.Add(clusterCenter);
                    count += 1;
                }
            }
            else
            {
                throw new Exception("OleDb Query result is not two dimensional");
            }
        }
    }
}
