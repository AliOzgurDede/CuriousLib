using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuriousLib
{
    /// <summary>
    /// Class for generating DataSet collection members from GUI controls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Generators<T>
    {
        /// <summary>
        /// Generating collection members from WinForms DataGridView Control
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="dataGridView"></param>
        public static void GeneratingFromDataGridView(DataSet<double> dataset, DataGridView dataGridView)
        {
            int rows = dataGridView.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    double value = Double.Parse(dataGridView[0, i].Value.ToString());
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
    }
}
