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
    public class TrendingDataSet<T> : DataSet<T>
    {
        #region Constructors

        /// <summary>
        /// Constructor of an instance of TrendingDataSet Class
        /// </summary>
        public TrendingDataSet()
        {
            this.Pattern = DataSetPattern.Trending;
        }

        #endregion

        # region Properties

        /// <summary>
        /// Gets the slope of a TrendingDataSet
        /// </summary>
        /// <remarks> DataSet is required to have trending pattern </remarks>
        public double Slope
        {
            get
            {
                if (this.Pattern == DataSetPattern.Trending)
                {
                    return this.GetSlope();
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Trending pattern expected");
                }
            }
        }

        /// <summary>
        /// Gets the intercept of a TrendingDataSet
        /// </summary>
        /// <remarks> DataSet is required to have trending pattern </remarks>
        public double Intercept
        {
            get
            {
                if (this.Pattern == DataSetPattern.Trending)
                {
                    return this.GetIntercept();
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Trending pattern expected");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Conducts point estimation of independent variable X using linear regression line
        /// </summary>
        /// <remarks> DataSet is required to have trending pattern </remarks>
        /// <param name="X"></param>
        /// <returns> Estimated Value: Y </returns>
        public double LinearRegression(int X)
        {
            if (this.Pattern == DataSetPattern.Trending)
            {
                double Y;
                Y = this.Intercept + (this.Slope * X);
                return Y;
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Trending pattern expected");
            }
        }

        /// <summary>
        /// Conducts point estimation of independent variable X using Holt's Method
        /// </summary>
        /// <param name="Alpha"></param>
        /// <param name="Beta"></param>
        /// <param name="X"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public double HoltsMethod(double Alpha, double Beta, int X)
        {
            if (this.Pattern == DataSet<T>.DataSetPattern.Trending)
            {
                if (Alpha > 0 && Alpha < 1 && Beta > 0 && Beta < 1)
                {
                    double Forecast = 0;
                    // Setup for smoothed intercepts
                    DataSet<double> SmoothedIntercepts = new DataSet<double>();
                    double InitialSmoothedIntercept = Convert.ToDouble(this[0]);
                    SmoothedIntercepts.Add(InitialSmoothedIntercept);
                    double CurrentSmoothedIntercept;
                    // Setup for smoothed slopes
                    DataSet<double> SmoothedSlopes = new DataSet<double>();
                    double InitialSmoothedSlope = Convert.ToDouble(this[1]) - Convert.ToDouble(this[0]);
                    SmoothedSlopes.Add(InitialSmoothedSlope);
                    double CurrentSmoothedSlope;

                    for (int i = 1; i < this.Size; i++)
                    {
                        CurrentSmoothedIntercept = Alpha * Convert.ToDouble(this[i]) + ((1 - Alpha) * (SmoothedIntercepts[i - 1] + SmoothedSlopes[i - 1]));
                        SmoothedIntercepts.Add(CurrentSmoothedIntercept);
                        CurrentSmoothedSlope = Beta * (SmoothedIntercepts[i] - SmoothedIntercepts[i - 1]) + ((1 - Beta) * SmoothedSlopes[i - 1]);
                        SmoothedSlopes.Add(CurrentSmoothedSlope);

                        if (X <= this.Size)
                        {
                            if (i == X - 1)
                            {
                                Forecast = CurrentSmoothedIntercept + CurrentSmoothedSlope;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (i == this.Size - 1)
                            {
                                Forecast = CurrentSmoothedIntercept + ((X - this.Size) * CurrentSmoothedSlope);
                            }
                        }
                    }

                    return Forecast;
                }
                else
                {
                    throw new Exception("Invalid Alpha or Beta value");
                }
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Trending pattern expected");
            }
        }

        #endregion

        #region Privates

        /// <summary>
        /// Calculates the slope of a TrendingDataSet
        /// </summary>
        /// <returns>Double</returns>
        private double GetSlope()
        {
            double slope = 0;
            DataSet<double> TimeAxis = new DataSet<double>();
            DataSet<double> OriginalDataSet = new DataSet<double>();

            for (int i = 0; i < this.Size; i++)
            {
                TimeAxis.Add(i + 1);
            }

            for (int i = 0; i < this.Size; i++)
            {
                OriginalDataSet.Add(Convert.ToDouble(this[i]));
            }

            slope = Miscellaneous.Correlation(OriginalDataSet, TimeAxis) * OriginalDataSet.StandartDeviation / TimeAxis.StandartDeviation;
            return slope;
        }

        /// <summary>
        /// Calculates the intercept of a TrendingDataSet
        /// </summary>
        /// <returns>Double</returns>
        private double GetIntercept()
        {
            double intercept;
            DataSet<double> TimeAxis = new DataSet<double>();
            DataSet<double> OriginalDataSet = new DataSet<double>();

            for (int i = 0; i < this.Size; i++)
            {
                TimeAxis.Add(i + 1);
            }

            for (int i = 0; i < this.Size; i++)
            {
                OriginalDataSet.Add(Convert.ToDouble(this[i]));
            }

            intercept = OriginalDataSet.Mean - (this.Slope * TimeAxis.Mean);
            return intercept;
        }

        #endregion
    }
}
