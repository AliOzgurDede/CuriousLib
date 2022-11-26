<p align="center">
  <img width="200" alt="Icon" src="https://user-images.githubusercontent.com/74831928/155857344-0348b7c6-0a61-431e-acea-0b826adeae26.png">   
</p>

<h1 align="center" style="margin-top: 0px;">CuriousLib</h1>

<div align="center">
  
C# Library for Data Analysis  
    
![release](https://img.shields.io/badge/release-v3.0-green) ![nuget](https://img.shields.io/nuget/v/CuriousLib) ![downloads](https://img.shields.io/nuget/dt/CuriousLib?color=orange)
  
</div>

## Usage
To use this library, it is necessary to create an instance of DataSet collection 
```csharp
DataSet<T> "YourDataSetName" = new DataSet<T>();
```
DataSet collection class is inherited from .NET List collection class
```csharp
public class DataSet<T> : List<T>
```

## Class Structure

### DataSet
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| Pattern | Property  | Gets or sets the pattern characteristic of a DataSet |
| MinimumValue | Property | Gets the minimum value of a DataSet |
| MaximumValue | Property | Gets the maximum value of a DataSet |
| Range | Property | Gets the range of a DataSet |
| Size | Property | Gets the size of a DataSet |
| Mean | Property | Gets the mean of a DataSet |
| Median | Property | Gets the median of a DataSet |
| Mode | Property | Gets the mode of a DataSet |
| StandartDeviation | Property | Gets the standart deviation of a DataSet |
| Skewness | Property | Gets the skewness of a DataSet |
| Kurtosis | Property | Gets the kurtosis of a DataSet |
| IsNormal | Property | Gets whether a dataset is normal distributed or not |
| IsUniform | Property | Gets whether a dataset is uniformly distributed or not |
| IsExponential | Property | Gets whether a dataset is exponentially distributed or not |
| CalculateZvalue | Function | Calculates the standart Z value for a given X value from a DataSet |
| Smooth | Function | Smooths a DataSet for a given alpha parameter |
| DetectOutliers | Function | Returns the outlier values of a DataSet |

### StationaryDataSet : DataSet
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| MovingAverages | Function | Conducts point estimation of selected index using n-step moving averages |
| ExponentialSmoothing | Function | Conducts point estimation of selected index using simple exponential smoothing with the alpha parameter |

### TrendingDataSet : DataSet
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| Slope | Property | Gets the slope of a trending DataSet |
| Intercept | Property | Gets the intercept of a trending DataSet |
| LinearRegression | Function | Conducts point estimation of independent variable X using linear regression line |
| HoltsMethod | Function | Conducts point estimation of independent variable X using Holt's Method |

### SeasonalDataSet : DataSet
| Field | Field Type | Description |
| ------| ---------- | ----------- |
| NumberOfSeasons | Property | Gets or sets the number of seasons in a seasonal DataSet |
| SeasonSize | Property | Gets the size of each season in a seasonal DataSet |
| SeasonalFactors | Property | Gets the seasonal factors of a seasonal DataSet |
| SeasonalEstimate | Function | Conducts point estimation of independent variable X considering seasonality |
| SeasonalEstimateWithTrend | Function | Conducts point estimation of independent variable X considering seasonality and trend |
| Deseasonalize | Function | Removes the seasonal effect from a SeasonalDataSet |

### Miscellaneous.Measurements
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| CalculateMeanAbsoluteError | Function | Returns the mean absolute forecast error of a DataSet |
| CalculateMeanSquaredError | Function | Returns the mean squared forecast error of a DataSet |
| Covariance | Function | Calculates covariance of two DataSets |
| Correlation | Function | Calculates coefficient of correlation between two DataSets |
| Rsquared | Function | Calculates coefficient of determination (R Squared) between two DataSets |

### Miscellaneous.Generators
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| GeneratingFromDataGridView | Function | Generating collection members from WinForms DataGridView Control (+5 overloads) |
| GeneratingFromListBox | Function | Generating collection members from WinForms ListBox Control (+3 overloads) |
| GeneratingFromSQL | Function | Generating collection members from SQL Server (+5 overloads) |
| GeneratingFromAccessDB | Function | Generating collection members from MS Access Database (+5 overloads) |

### Clustering.Kmeans
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| RunAlgorithm | Function | Executes K Means Clustering Algoritm |
| Report | Function | Generates an algorithm iteration report to a specified file path |
| Assignments | Collection | Collection that stores data point cluster center assignments |
| Iterations | Collection | Collection that stores iterations |

### Clustering.DataPoint
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| ID | Property | Gets or sets the ID of a DataPoint |
| CoordinateX | Property | Gets or sets the X coordinate of a DataPoint |
| CoordinateY | Property | Gets or sets the Y coordinate of a DataPoint |

### Clustering.ClusterCenter
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| ID | Property | Gets or sets the ID of a ClusterCenter |
| CoordinateX | Property | Gets or sets the X coordinate of a ClusterCenter |
| CoordinateY | Property | Gets or sets the Y coordinate of a ClusterCenter |

### Clustering.Iteration
| Field | Field Type | Description |
| ----- | ---------- | ----------- |
| ID | Property | Gets or sets the ID of an Iteration |
| TentativeCluster | Struct | Tentative cluster structure belongs to an iteration |
| ClustersOfIteration | Collection | Collection that stores cluster assignments of an iteration |

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Used Technologies
Visual Studio  
C#  
.NET 5.0  
