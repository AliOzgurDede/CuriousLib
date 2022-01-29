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
    /// <summary>
    /// Primary data storing class of this library. Inherits from .NET List Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DataSet<T> : List<T>
    {
        #region Constructors

        /// <summary>
        /// Constructor of an instance of DataSet Class
        /// </summary>
        public DataSet()
        {
            this.Pattern = DataSetPattern.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the pattern characteristic of a DataSet
        /// </summary>
        public DataSetPattern Pattern
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;
            }
        }
        public enum DataSetPattern
        {
            Stationary,
            Trending,
            Seasonal,
            None
        }

        /// <summary>
        /// Gets the minimum value of a DataSet
        /// </summary>
        public double MinimumValue
        {
            get
            {
                return Convert.ToDouble(this.Min());
            }
        }

        /// <summary>
        /// Gets the maximum value of a DataSet
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return Convert.ToDouble(this.Max());
            }
        }

        /// <summary>
        /// Gets the range of a DataSet
        /// </summary>
        public double Range
        {
            get
            {
                return (this.MaximumValue - this.MinimumValue);
            }
        }

        /// <summary>
        /// Gets the size of a DataSet
        /// </summary>
        public double Size
        {
            get
            {
                return (Convert.ToDouble(this.Count));
            }
        }

        /// <summary>
        /// Gets the mean of a DataSet
        /// </summary>
        public double Mean
        {
            get
            {
                return (this.CalculateMean());
            }
        }

        /// <summary>
        /// Gets the standart deviation of a DataSet
        /// </summary>
        public double StandartDeviation
        {
            get
            {
                return (this.CalculateStandartDeviation());
            }
        }

        /// <summary>
        /// Gets the skewness of a DataSet
        /// </summary>
        public double Skewness
        {
            get
            {
                return(this.CalculateSkewness());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the standart Z value for a given X value from a DataSet
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public double CalculateZvalue(double X)
        {
            double Z;
            Z = (X - this.Mean) / this.StandartDeviation;
            return Z;
        }

        /// <summary>
        /// Smooths a DataSet for a given alpha parameter
        /// </summary>
        /// <param name="Alpha"></param>
        /// <returns>DataSet</returns>
        public DataSet<double> Smooth(double Alpha)
        {
            double NewItem;
            DataSet <double> SmoothedDataSet = new DataSet<double>();
            for (int i = 0; i < this.Size; i++)
            {
                if (i == 0)
                {
                    NewItem = Convert.ToDouble(this[i]);
                    SmoothedDataSet.Add(NewItem);
                }
                else
                {
                    NewItem = (Alpha * Convert.ToDouble(this[i - 1])) + ((1 - Alpha) * SmoothedDataSet[i - 1]);
                    SmoothedDataSet.Add(NewItem);
                }
            }
            return SmoothedDataSet;
        }

        #endregion

        #region Privates

        private DataSetPattern pattern;

        private double CalculateMean()
        {
            double mean;
            double total = 0;

            for (int i = 0; i < this.Count; i++)
            {
                total = total + Convert.ToDouble(this[i]);
            }

            mean = Math.Round(total / this.Count, 2);
            return mean;
        }

        private double CalculateStandartDeviation()
        {
            double standartDeviation;
            double sumOfSquares = 0;
            double mean = this.Mean;

            for (int i = 0; i < this.Size; i++)
            {
                sumOfSquares = sumOfSquares + Math.Pow(Convert.ToDouble(this[i]) - mean, 2);
            }

            standartDeviation = Math.Round(Math.Sqrt(sumOfSquares / this.Size), 2);
            return standartDeviation;
        }

        private double CalculateSkewness()
        {
            double skewness;
            double mean = this.Mean;
            double sumOfSquares = 0;
            double sumOfCubes = 0;

            for (int i = 0; i < this.Size; i++)
            {
                sumOfCubes = sumOfCubes + Math.Pow(Convert.ToDouble(this[i]) - mean, 3);
                sumOfSquares = sumOfSquares + Math.Pow(Convert.ToDouble(this[i]) - mean, 2);
            }

            sumOfCubes = sumOfCubes / this.Size;
            sumOfSquares = Math.Pow(Math.Sqrt(sumOfSquares / (this.Size - 1)), 3);
            skewness = sumOfCubes / sumOfSquares;
            skewness = Math.Round(skewness, 2);
            return skewness;
        }

        #endregion
    }
}
