using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuriousLib.Clustering
{
    public static class Kmeans
    {
        /// <summary>
        /// Algorithm stopping property
        /// </summary>
        private static bool StopAlgorithm
        {
            get; set;
        }

        /// <summary>
        /// Collection that stores data point cluster center assignments
        /// </summary>
        private static Dictionary<DataPoint, ClusterCenter> Assignments = new Dictionary<DataPoint, ClusterCenter>();

        /// <summary>
        /// Collection that stores iterations
        /// </summary>
        private static DataSet<Iteration> Iterations = new DataSet<Iteration>();

        /// <summary>
        /// Executes K Means Clustering Algoritm
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="clusterCenters"></param>
        /// <exception cref="Exception"></exception>
        public static void RunAlgorithm(DataSet<DataPoint> dataPoints, DataSet<ClusterCenter> clusterCenters)
        {
            try
            {
                StopAlgorithm = false;
                Assignments.Clear();
                Iterations.Clear();

                while (StopAlgorithm == false)
                {
                    AssignmentStep(dataPoints, clusterCenters);
                    UpdateStep(dataPoints, clusterCenters);
                    SaveIteration(dataPoints, clusterCenters);
                }
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
        }

        /// <summary>
        /// Calculates the euclidean distance between data point and cluster center
        /// </summary>
        /// <param name="point"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private static double EuclideanDistance(DataPoint point, ClusterCenter center)
        {
            double distance;
            distance = Math.Sqrt((Math.Pow((point.CoordinateX - center.CoordinateX), 2)) + (Math.Pow((point.CoordinateY - center.CoordinateY), 2)));
            return distance;
        }

        /// <summary>
        /// Executes assignment step of K Means Clustering Algorithm
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="clusterCenters"></param>
        private static void AssignmentStep(DataSet<DataPoint> dataPoints, DataSet<ClusterCenter> clusterCenters)
        {
            DataSet<double> DistanceRecording = new DataSet<double>();
            Assignments.Clear();

            for (int i = 0; i < dataPoints.Size; i++)
            {
                DistanceRecording.Clear();

                for (int j = 0; j < clusterCenters.Size; j++)
                {
                    DistanceRecording.Add(EuclideanDistance(dataPoints[i], clusterCenters[j]));
                }

                double MinVal = DistanceRecording.Min();
                int index = DistanceRecording.IndexOf(MinVal);
                Assignments.Add(dataPoints[i], clusterCenters[index]);
            }
        }

        /// <summary>
        /// Executes update step of K Means Clustering Algorithm
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="clusterCenters"></param>
        private static void UpdateStep(DataSet<DataPoint> dataPoints, DataSet<ClusterCenter> clusterCenters)
        {
            int NumberOfCentersNotChanged = 0;

            for (int i = 0; i < clusterCenters.Size; i++)
            {
                double NewCentroidX = 0;
                double NewCentroidY = 0;

                var ClusterMembers =
                        from a in Assignments
                        where a.Value.ID == clusterCenters[i].ID
                        select a.Key;

                foreach (var member in ClusterMembers)
                {
                    NewCentroidX += member.CoordinateX;
                    NewCentroidY += member.CoordinateY;
                }

                NewCentroidX /= ClusterMembers.Count();
                NewCentroidY /= ClusterMembers.Count();

                if (NewCentroidX == clusterCenters[i].CoordinateX && NewCentroidY == clusterCenters[i].CoordinateY)
                {
                    NumberOfCentersNotChanged += 1;
                }
                else
                {
                    ClusterCenter center = clusterCenters[i];
                    center.ID = i;
                    center.CoordinateX = NewCentroidX;
                    center.CoordinateY = NewCentroidY;
                    clusterCenters[i] = center;
                }
            }

            if (NumberOfCentersNotChanged == clusterCenters.Size)
            {
                StopAlgorithm = true;
            }
            else
            {
                StopAlgorithm = false;
            }
        }

        /// <summary>
        /// Saves an iteration
        /// </summary>
        /// <param name="dataPoints"></param>
        /// <param name="clusterCenters"></param>
        private static void SaveIteration(DataSet<DataPoint> dataPoints, DataSet<ClusterCenter> clusterCenters)
        {
            Iteration iteration = new Iteration();

            for (int i = 0; i < clusterCenters.Size; i++)
            {
                Iteration.TentativeCluster tentativeCluster = new Iteration.TentativeCluster(clusterCenters[i].ID, clusterCenters[i].CoordinateX, clusterCenters[i].CoordinateY);

                var members =
                    from a in Assignments
                    where a.Value.ID == clusterCenters[i].ID
                    select a.Key;

                foreach ( var member in members)
                {
                    tentativeCluster.ClusterMembers.Add(member);
                }

                iteration.ClustersOfIteration.Add(tentativeCluster);
            }

            Iterations.Add(iteration);
        }

        /// <summary>
        /// Generates an algorithm iteration report to a specified file path
        /// </summary>
        /// <param name="OutputPath"></param>
        public static void Report(string OutputPath)
        {
            FileStream fileStream = new FileStream(OutputPath, FileMode.Create);

            string report = "| CURIOUSLIB K MEANS CLUSTERING ITERATION REPORT  |";
            report += "\n" + "|" + DateTime.Now.ToString() + "|";
            report += "\n" + "_____________________";

            for (int i = 0; i < Iterations.Size; i++)
            {
                report += "\n" + "Iteration " + Iterations[i].ID;
                report += "\n" + "---------------------";

                for (int j = 0; j < Iterations[i].ClustersOfIteration.Count; j++)
                {
                    report += "\n" + "Cluster " + (Iterations[i].ClustersOfIteration[j].ClusterID + 1);
                    report += "\n" + "Centroid X:" + Iterations[i].ClustersOfIteration[j].CentroidX;
                    report += "\n" + "Centroid Y:" + Iterations[i].ClustersOfIteration[j].CentroidY;
                    report += "\n" + "Member Data Points";

                    for (int k = 0; k < Iterations[i].ClustersOfIteration[j].ClusterMembers.Count; k++)
                    {
                        report += "\n";
                        report += "PointID:" + Iterations[i].ClustersOfIteration[j].ClusterMembers[k].ID;
                        report += " X:" + Iterations[i].ClustersOfIteration[j].ClusterMembers[k].CoordinateX;
                        report += " Y:" + Iterations[i].ClustersOfIteration[j].ClusterMembers[k].CoordinateY;
                    }

                    report += "\n";
                }

                report += "\n" + "_____________________";
            }

            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(report);
            }
        }

    }
}
