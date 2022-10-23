using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuriousLib.Clustering
{
    public class Iteration
    {
        /// <summary>
        /// Gets or sets the ID of an iteration
        /// </summary>
        public int ID
        {
            get; set; 
        }

        /// <summary>
        /// Tentative cluster structure belongs to an iteration
        /// </summary>
        public struct TentativeCluster
        {
            public int ClusterID;
            public double CentroidX;
            public double CentroidY;
            public DataSet<DataPoint> ClusterMembers;

            public TentativeCluster(int id, double x, double y)
            {
                this.ClusterID = id;
                this.CentroidX = x;
                this.CentroidY = y;
                ClusterMembers = new DataSet<DataPoint>();
            }
        }

        /// <summary>
        /// Collection that stores cluster assignments of an iteration
        /// </summary>
        public DataSet<TentativeCluster> ClustersOfIteration = new DataSet<TentativeCluster>();

    }
}
