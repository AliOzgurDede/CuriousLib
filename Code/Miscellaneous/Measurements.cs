using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;

namespace CuriousLib.Miscellaneous
{
    public static class Measurements
    {
        /// <summary>
        /// Returns the mean absolute forecast error of a DataSet
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <returns>double</returns>
        public static double MeanAbsoluteError(DataSet<double> dataSet1, DataSet<double> dataSet2)
        {
            if (dataSet1.Size == dataSet2.Size)
            {
                double MAE = 0;

                for (int i = 0; i < dataSet1.Size; i++)
                {
                    MAE += Math.Abs(dataSet1[i] - dataSet2[i]);
                }

                MAE /= dataSet1.Size;
                return MAE;
            }
            else
            {
                throw new Exception("DataSets are not at the same size");
            }
        }

        /// <summary>
        /// Returns the mean squared forecast error of a DataSet
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <returns>double</returns>
        public static double MeanSquaredError(DataSet<double> dataSet1, DataSet<double> dataSet2)
        {
            if (dataSet1.Size == dataSet2.Size)
            {
                double MSE = 0;

                for (int i = 0; i < dataSet1.Size; i++)
                {
                    MSE += Math.Pow(dataSet1[i] - dataSet2[i], 2);
                }

                MSE /= dataSet1.Size;
                return MSE;
            }
            else
            {
                throw new Exception("DataSets are not at the same size");
            }
        }

        /// <summary>
        /// Calculates covariance of two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <returns>double</returns>
        public static double Covariance(DataSet<double> dataSet1, DataSet<double> dataSet2)
        {
            if (dataSet1.Size == dataSet2.Size)
            {
                double covariance;
                double sumOfMultiples = 0;

                for (int i = 0; i < dataSet1.Size; i++)
                {
                    sumOfMultiples += (Convert.ToDouble(dataSet1[i]) - dataSet1.Mean) * (Convert.ToDouble(dataSet2[i]) - dataSet2.Mean);
                }

                covariance = sumOfMultiples / dataSet1.Size;
                return covariance;
            }
            else
            {
                throw new Exception("DataSets are not at the same size");
            }
        }

        /// <summary>
        /// Calculates coefficient of correlation between two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <returns>double</returns>
        public static double Correlation(DataSet<double> dataSet1, DataSet<double> dataSet2)
        {
            double correlation = Covariance(dataSet1, dataSet2) / (dataSet1.StandartDeviation * dataSet2.StandartDeviation);
            return correlation;
        }

        /// <summary>
        /// Calculates coefficient of determination (R Squared) between two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <returns>double</returns>
        public static double Rsquared(DataSet<double> dataSet1, DataSet<double> dataSet2)
        {
            double rs = Math.Pow(Correlation(dataSet1, dataSet2),2);
            return rs;
        }
    }
}
