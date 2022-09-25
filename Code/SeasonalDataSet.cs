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
    public class SeasonalDataSet<T> : DataSet<T>
    {
        #region Constructors

        /// <summary>
        /// Constructor of an instance of SeasonalDataSet Class
        /// </summary>
        public SeasonalDataSet()
        {
            this.Pattern = DataSetPattern.Seasonal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of seasons in a SeasonalDataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public int NumberOfSeasons
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    return numberOfSeasons;
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
            set
            {
                if (this.Pattern == DataSet<T>.DataSetPattern.Seasonal)
                {
                    if (this.Size % value == 0)
                    {
                        numberOfSeasons = value;
                    }
                    else
                    {
                        throw new Exception("The remainder value when DataSet Size divided by value is not equal to zero");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }
        private int numberOfSeasons;

        /// <summary>
        /// Gets the size of each season in a SeasonalDataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public int SeasonSize
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    return (this.Size / this.NumberOfSeasons);
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }

        /// <summary>
        /// Gets the seasonal factors of a SeasonalDataSet
        /// </summary>
        public double[] SeasonalFactors
        {
            get
            {
                if (this.Pattern == DataSet<T>.DataSetPattern.Seasonal)
                {
                    return this.GetSeasonalFactors();
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Conducts point estimation of independent variable X considering seasonality
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public double SeasonalEstimate(int X)
        {
            double Estimate;

            Estimate = this.Mean * this.SeasonalFactors[X % this.SeasonSize];

            return Estimate;
        }

        /// <summary>
        /// Conducts point estimation of independent variable X considering seasonality and trend
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public double SeasonalEstimateWithTrend(int X)
        {
            double Estimate = 0;

            TrendingDataSet<double> DeseasonalizedSetWithTrend = new TrendingDataSet<double>();

            this.Deseasonalize();

            for (int i = 0; i < this.Size; i++)
            {
                DeseasonalizedSetWithTrend.Add(Convert.ToDouble(this[i]));
            }

            Estimate = DeseasonalizedSetWithTrend.LinearRegression(X);
            Estimate *= this.SeasonalFactors[X % this.SeasonSize];

            return Estimate;
        }

        /// <summary>
        /// Removes the seasonal effect from a SeasonalDataSet
        /// </summary>
        /// <returns></returns>
        public DataSet<double> Deseasonalize()
        {
            DataSet<double> deseasonalizedDataSet = new DataSet<double>();

            for (int i = 0; i < this.Size; i++)
            {
                deseasonalizedDataSet.Add(Convert.ToDouble(this[i]) / this.SeasonalFactors[i % this.SeasonSize]);
            }

            return deseasonalizedDataSet;
        }

        #endregion

        #region Privates

        /// <summary>
        /// Calculates seasonal factors for each items in cycle
        /// </summary>
        /// <returns></returns>
        private double[] GetSeasonalFactors()
        {
            double[] seasonalFactors = new double[this.SeasonSize];

            double[,] seasonMatrix = new double[this.SeasonSize, this.NumberOfSeasons];

            for (int i = 0; i < this.Size; i++)
            {
                seasonMatrix[i % this.SeasonSize, i / this.SeasonSize] = Convert.ToDouble(this[i]) / this.Mean;
            }

            for (int i = 0; i < this.SeasonSize; i++)
            {
                for (int j = 0; j < this.NumberOfSeasons; j++)
                {
                    seasonalFactors[i] += seasonMatrix[i, j];
                }
                seasonalFactors[i] /= this.NumberOfSeasons;
            }

            return seasonalFactors;
        }

        #endregion
    }
}
