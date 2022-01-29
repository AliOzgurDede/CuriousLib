using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuriousLib
{
    /// <summary>
    /// Primary data storing class of this library. Inherits from .NET List Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StationaryDataSet<T> : DataSet<T>
    {
        #region Constructors

        /// <summary>
        /// Constructor of an instance of StationaryDataSet Class
        /// </summary>
        public StationaryDataSet()
        {
            this.Pattern = DataSetPattern.Stationary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Conducts point estimation of selected index using n-step moving averages 
        /// </summary>
        /// <remarks> DataSet is required to have stationary pattern </remarks>
        /// <param name="Step"></param>
        /// <returns> Estimated Value </returns>
        public double MovingAverages(int Step)
        {
            if (this.Pattern == DataSetPattern.Stationary)
            {
                double Forecast = 0;
                for (int i = 1; i <= Step; i++)
                {
                    Forecast += Convert.ToDouble(this[Convert.ToInt32(this.Size) - i]);
                }
                return Forecast;
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Stationary pattern expected");
            }
        }

        /// <summary>
        /// Conducts point estimation of selected index using simple exponential smoothing with the alpha parameter
        /// </summary>
        /// <remarks> DataSet is required to have stationary pattern </remarks>
        /// <param name="Alpha"></param>
        /// <returns> Estimated Value</returns>
        public double ExponentialSmoothing(double Alpha)
        {
            if (this.Pattern == DataSetPattern.Stationary)
            {
                if (Alpha < 1 && Alpha > 0)
                {
                    double Forecast = 0;
                    for (int i = 0; i < this.Size; i++)
                    {
                        Forecast += Alpha * Math.Pow((1 - Alpha), i) * Convert.ToDouble(this[Convert.ToInt32(this.Size) - 1 - i]);
                    }
                    return Forecast;
                }
                else
                {
                    throw new Exception("Invalid Alpha Value");
                }
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Stationary pattern expected");
            }
        }
    }

    #endregion
}
