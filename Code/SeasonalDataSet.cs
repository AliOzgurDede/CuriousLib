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
        /// Gets or sets the number of seasons in a seasonal DataSet
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
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    if (this.Count % value == 0)
                    {
                        numberOfSeasons = value;
                    }
                    else
                    {
                        throw new Exception("DataSetSize = 0(mod NumberOfSeasons) should be satisfied");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of each season in a seasonal DataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public int SeasonSize
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    return seasonSize;
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
            set
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    if (this.Count % value == 0)
                    {
                        seasonSize = value;
                    }
                    else
                    {
                        throw new Exception("DataSetSize = 0(mod SeasonSize) should be satisfied");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }

        /// <summary>
        /// Gets the seasonal factors of a seasonal DataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public double[] SeasonalFactors
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    try
                    {
                        return seasonalFactors = this.GetSeasonalFactors();
                    }
                    catch
                    {
                        throw new Exception("Unassigned properties");
                    }
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
        /// Conducts point estimation of selected index using seasonal factors
        /// </summary>
        /// <param name="Index"></param>
        /// <returns> Estimated Value </returns>
        public double SeasonalEstimate(int Index)
        {
            double seasonalEstimate;
            seasonalEstimate = average * SeasonalFactors[Index % this.SeasonSize];
            return seasonalEstimate;
        }

        #endregion

        #region Privates

        private double[] seasonalFactors;
        private int seasonSize;
        private int numberOfSeasons;
        private double average;
        private double[] GetSeasonalFactors()
        {
            double[] averageRatios = new double[this.SeasonSize];
            average = 0;

            for (int i = 0; i < this.Count; i++)
            {
                average = average + Convert.ToDouble(this[i]);
            }
            average = Math.Round(average / this.Count, 2);

            for (int i = 0; i < this.SeasonSize; i++)
            {
                for (int j = i; j < this.Count; j += this.SeasonSize)
                {
                    averageRatios[i] += (Convert.ToDouble(this[j]) / average);
                }

                averageRatios[i] /= this.NumberOfSeasons;
            }

            return averageRatios;
        }

        #endregion
    }
}
