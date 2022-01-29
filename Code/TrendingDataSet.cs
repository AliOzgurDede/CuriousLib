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
        /// Gets the slope of a trending DataSet
        /// </summary>
        /// <remarks> DataSet is required to have trending pattern </remarks>
        public double Slope
        {
            get
            {
                if (this.Pattern == DataSetPattern.Trending)
                {
                    return slope = this.GetSlope();
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Trending pattern expected");
                }
            }
        }
        
        /// <summary>
        /// Gets the intercept of a trending DataSet
        /// </summary>
        /// <remarks> DataSet is required to have trending pattern </remarks>
        public double Intercept
        {
            get
            {
                if (this.Pattern == DataSetPattern.Trending)
                {
                    return intercept = this.GetIntercept();
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
        /// <param name="x"></param>
        /// <returns> Estimated Value: Y </returns>
        public double LinearRegression(double X)
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

        #endregion

        #region Privates

        private double slope;
        private double intercept;
        private int[] TimeAxis;
        private double GetSlope()
        {
            double _slope;
            int N = this.Count;
            TimeAxis = new int[N];
            for (int i = 0; i < N; i++)
            {
                TimeAxis[i] = i + 1;
            }

            double xBar = 0;
            double ttl = 0;
            for (int i = 0; i < N; i++)
            {
                ttl = ttl + TimeAxis[i];
            }
            xBar = ttl / N;
            xBar = Math.Round(xBar, 2);
            double yBar = 0;
            double Total = 0;
            for (int i = 0; i < N; i++)
            {
                Total = Total + Convert.ToDouble(this[i]);
            }
            yBar = Total / N;
            yBar = Math.Round(yBar, 2);

            double sumOfXcrossY = 0;
            for (int i = 0; i < N; i++)
            {
                sumOfXcrossY = sumOfXcrossY + (Convert.ToDouble(this[i]) * TimeAxis[i]);
            }

            double sumOfXsquare = 0;
            for (int i = 0; i < N; i++)
            {
                sumOfXsquare = sumOfXsquare + (Math.Pow(TimeAxis[i], 2));
            }

            _slope = (sumOfXcrossY - (N * xBar * yBar)) / (sumOfXsquare - (N * xBar * xBar));
            return _slope;
        }
        private double GetIntercept()
        {
            double _intercept;
            int N = this.Count;
            TimeAxis = new int[N];
            for (int i = 0; i < N; i++)
            {
                TimeAxis[i] = i + 1;
            }

            double xBar = 0;
            double ttl = 0;
            for (int i = 0; i < N; i++)
            {
                ttl = ttl + TimeAxis[i];
            }
            xBar = ttl / N;
            xBar = Math.Round(xBar, 2);
            double yBar = 0;
            double Total = 0;
            for (int i = 0; i < N; i++)
            {
                Total = Total + Convert.ToDouble(this[i]);
            }
            yBar = Total / N;
            yBar = Math.Round(yBar, 2);

            _intercept = yBar - (this.Slope * xBar);
            return _intercept;
        }

        #endregion
    }
}
