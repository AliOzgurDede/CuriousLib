using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return (this.CalculateSkewness());
            }
        }

        /// <summary>
        /// Gets whether a dataset is normal distributed or not
        /// </summary>
        public bool IsNormal
        {
            get
            {
                return this.TestForNormality();
            }
        }

        /// <summary>
        /// Gets whether a dataset is uniformly distributed or not
        /// </summary>
        public bool IsUniform
        {
            get
            {
                return this.TestForUniformity();
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
            DataSet<double> SmoothedDataSet = new DataSet<double>();
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

        /// <summary>
        /// Conducts hypothesis testing for data normality
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns>bool</returns>
        private bool TestForNormality()
        {
            // defining test parameters
            bool Hypothesis;
            int DegreeOfFreedom = 5;
            double CriticalValue = ChiSquareTable[DegreeOfFreedom];
            double ChiSquareValue = 0;
            // calculating expected frequencies
            double[] ExpectedFrequency = new double[6];
            ExpectedFrequency[0] = 2.1 / 100 * this.Size;
            ExpectedFrequency[1] = 13.6 / 100 * this.Size;
            ExpectedFrequency[2] = 34.1 / 100 * this.Size;
            ExpectedFrequency[3] = 34.1 / 100 * this.Size;
            ExpectedFrequency[4] = 13.6 / 100 * this.Size;
            ExpectedFrequency[5] = 2.1 / 100 * this.Size;
            // gathering observed frequencies
            double[] ObservedFrequency = new double[6];
            ObservedFrequency[0] = 0;
            ObservedFrequency[1] = 0;
            ObservedFrequency[2] = 0;
            ObservedFrequency[3] = 0;
            ObservedFrequency[4] = 0;
            ObservedFrequency[5] = 0;

            for (int i = 0; i < this.Size; i++)
            {
                double Z = this.CalculateZvalue(Convert.ToDouble(this[i]));

                if (Z <= -2)
                {
                    ObservedFrequency[0] = ObservedFrequency[0] + 1;
                }
                else if (Z > -2 && Z <= -1)
                {
                    ObservedFrequency[1] = ObservedFrequency[1] + 1;
                }
                else if (Z > -1 && Z <= 0)
                {
                    ObservedFrequency[2] = ObservedFrequency[2] + 1;
                }
                else if (Z > 0 && Z <= 1)
                {
                    ObservedFrequency[3] = ObservedFrequency[3] + 1;
                }
                else if (Z > 1 && Z <= 2)
                {
                    ObservedFrequency[4] = ObservedFrequency[4] + 1;
                }
                else if (Z > 2)
                {
                    ObservedFrequency[5] = ObservedFrequency[5] + 1;
                }
            }
            // calculating chi square parameter
            for (int i = 0; i < 6; i++)
            {
                ChiSquareValue = ChiSquareValue + Math.Pow(ObservedFrequency[i] - ExpectedFrequency[i], 2) / ExpectedFrequency[i];
            }
            // comparing the calculated chi square parameter and critical chi square parameter
            if (ChiSquareValue <= CriticalValue)
            {
                Hypothesis = true;
            }
            else
            {
                Hypothesis = false;
            }
            return Hypothesis;
        }

        /// <summary>
        /// Conducts hypothesis testing for data uniformity
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns>bool</returns>
        private bool TestForUniformity()
        {
            // defining test parameters
            bool Hypothesis;
            int DegreeOfFreedom = 5;
            double CriticalValue = ChiSquareTable[DegreeOfFreedom];
            double ChiSquareValue = 0;
            // calculating expected frequencies
            double[] ExpectedFrequency = new double[6];
            ExpectedFrequency[0] = 16.6 / 100 * this.Size;
            ExpectedFrequency[1] = 16.6 / 100 * this.Size;
            ExpectedFrequency[2] = 16.6 / 100 * this.Size;
            ExpectedFrequency[3] = 16.6 / 100 * this.Size;
            ExpectedFrequency[4] = 16.6 / 100 * this.Size;
            ExpectedFrequency[5] = 16.6 / 100 * this.Size;
            // gathering observed frequencies
            double[] ObservedFrequency = new double[6];
            ObservedFrequency[0] = 0;
            ObservedFrequency[1] = 0;
            ObservedFrequency[2] = 0;
            ObservedFrequency[3] = 0;
            ObservedFrequency[4] = 0;
            ObservedFrequency[5] = 0;

            for (int i = 0; i < this.Size; i++)
            {
                double Z = this.CalculateZvalue(Convert.ToDouble(this[i]));

                if (Z <= -2)
                {
                    ObservedFrequency[0] = ObservedFrequency[0] + 1;
                }
                else if (Z > -2 && Z <= -1)
                {
                    ObservedFrequency[1] = ObservedFrequency[1] + 1;
                }
                else if (Z > -1 && Z <= 0)
                {
                    ObservedFrequency[2] = ObservedFrequency[2] + 1;
                }
                else if (Z > 0 && Z <= 1)
                {
                    ObservedFrequency[3] = ObservedFrequency[3] + 1;
                }
                else if (Z > 1 && Z <= 2)
                {
                    ObservedFrequency[4] = ObservedFrequency[4] + 1;
                }
                else if (Z > 2)
                {
                    ObservedFrequency[5] = ObservedFrequency[5] + 1;
                }
            }
            // calculating chi square parameter
            for (int i = 0; i < 6; i++)
            {
                ChiSquareValue = ChiSquareValue + Math.Pow(ObservedFrequency[i] - ExpectedFrequency[i], 2) / ExpectedFrequency[i];
            }
            // comparing the calculated chi square parameter and critical chi square parameter
            if (ChiSquareValue <= CriticalValue)
            {
                Hypothesis = true;
            }
            else
            {
                Hypothesis = false;
            }
            return Hypothesis;
        }

        private Dictionary<double, double> ChiSquareTable = new Dictionary<double, double>();
        private void GeneratingChiSquareTable()
        {
            // key=s.d. value=chisquare
            // significance degree = %1
            ChiSquareTable.Add(1, 6.635);
            ChiSquareTable.Add(2, 9.210);
            ChiSquareTable.Add(3, 11.345);
            ChiSquareTable.Add(4, 13.227);
            ChiSquareTable.Add(5, 15.086);
            ChiSquareTable.Add(6, 16.812);
            ChiSquareTable.Add(7, 18.475);
            ChiSquareTable.Add(8, 20.090);
            ChiSquareTable.Add(9, 21.666);
            ChiSquareTable.Add(10, 23.209);
            ChiSquareTable.Add(11, 24.725);
            ChiSquareTable.Add(12, 26.217);
            ChiSquareTable.Add(13, 27.688);
            ChiSquareTable.Add(14, 29.141);
            ChiSquareTable.Add(15, 30.578);
            ChiSquareTable.Add(16, 32.000);
            ChiSquareTable.Add(17, 33.409);
            ChiSquareTable.Add(18, 34.805);
            ChiSquareTable.Add(19, 36.191);
            ChiSquareTable.Add(20, 37.566);
            ChiSquareTable.Add(21, 38.932);
            ChiSquareTable.Add(22, 40.289);
            ChiSquareTable.Add(23, 41.638);
            ChiSquareTable.Add(24, 42.980);
            ChiSquareTable.Add(25, 44.314);
            ChiSquareTable.Add(26, 45.642);
            ChiSquareTable.Add(27, 46.963);
            ChiSquareTable.Add(28, 48.378);
            ChiSquareTable.Add(29, 49.588);
            ChiSquareTable.Add(30, 50.892);
        }

        #endregion
    }
}
