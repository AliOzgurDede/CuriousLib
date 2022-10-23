using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuriousLib.Clustering
{
    public class DataPoint
    {
        /// <summary>
        /// Gets or sets the ID of a DataPoint
        /// </summary>
        public int ID
        {
            get; set; 
        }

        /// <summary>
        /// Gets or sets the X coordinate of a DataPoint
        /// </summary>
        public double CoordinateX
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Y coordinate of a DataPoint
        /// </summary>
        public double CoordinateY
        {
            get; set;
        }
    }
}
