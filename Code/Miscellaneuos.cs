using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace CuriousLib
{
    public static class Miscellaneous
    {
        /// <summary>
        /// Returns the mean absolute forecast error of a DataSet
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="forecastedSet"></param>
        /// <returns>double</returns>
        public static double CalculateMeanAbsoluteError(DataSet<double> dataSet, DataSet<double> forecastedSet)
        {
            double MAE = 0;

            for (int i = 0; i < dataSet.Size; i++)
            {
                MAE += Math.Abs(dataSet[i] - forecastedSet[i]);
            }

            MAE /= dataSet.Size;
            return MAE;
        }

        /// <summary>
        /// Returns the mean squared forecast error of a DataSet
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="forecastedSet"></param>
        /// <returns>double</returns>
        public static double CalculateMeanSquaredError(DataSet<double> dataSet, DataSet<double> forecastedSet)
        {
            double MSE = 0;

            for (int i = 0; i < dataSet.Size; i++)
            {
                MSE += Math.Pow(dataSet[i] - forecastedSet[i], 2);
            }

            MSE /= dataSet.Size;
            return MSE;
        }

        /// <summary>
        /// Calculates covariance of two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <param name="ReferenceInterval"></param>
        /// <returns>double</returns>
        public static double Covariance(DataSet<double> dataSet1, DataSet<double> dataSet2, int ReferenceInterval)
        {
            double covariance;
            double sumOfMultiples = 0;

            for (int i = 0; i < ReferenceInterval; i++)
            {
                sumOfMultiples += ((Convert.ToDouble(dataSet1[i]) - dataSet1.Mean) * (Convert.ToDouble(dataSet2[i]) - dataSet2.Mean));
            }

            covariance = sumOfMultiples / ReferenceInterval;
            return covariance;
        }

        /// <summary>
        /// Calculates coefficient of correlation between two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <param name="ReferenceInterval"></param>
        /// <returns>double</returns>
        public static double Correlation(DataSet<double> dataSet1, DataSet<double> dataSet2, int ReferenceInterval)
        {
            double correlation = Covariance(dataSet1, dataSet2, ReferenceInterval) / (dataSet1.StandartDeviation * dataSet1.StandartDeviation);
            return correlation;
        }

        /// <summary>
        /// Calculates coefficient of determination (R Squared) between two DataSets
        /// </summary>
        /// <param name="dataSet1"></param>
        /// <param name="dataSet2"></param>
        /// <param name="ReferenceInterval"></param>
        /// <returns>double</returns>
        public static double Rsquared(DataSet<double> dataSet1, DataSet<double> dataSet2, int ReferenceInterval)
        {
            double rs = Correlation(dataSet1, dataSet2, ReferenceInterval);
            return rs;
        }
    }
}
