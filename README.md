<p align="center">
  <img width="200" alt="Icon" src="https://user-images.githubusercontent.com/74831928/155857344-0348b7c6-0a61-431e-acea-0b826adeae26.png">   
</p>

<h1 align="center" style="margin-top: 0px;">CuriousLib</h1>

<div align="center">
  
C# Class Library for Statistical Computations  
    
![release](https://img.shields.io/badge/release-v2.0-green) ![nuget](https://img.shields.io/nuget/v/CuriousLib) ![license](https://img.shields.io/github/license/AliOzgurDede/CuriousLib?color=red) 
  
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
| Properties | Data Type | Description |
| ---------- | --------- | ----------- |
| Pattern | Enum  | Gets or sets the pattern characteristic of a DataSet |
| MinimumValue | Double | Gets the minimum value of a DataSet |
| MaximumValue | Double | Gets the maximum value of a DataSet |
| Range | Integer | Gets the range of a DataSet |
| Size | Integer | Gets the size of a DataSet |
| Mean | Double | Gets the mean of a DataSet |
| Median | Double | Gets the median of a DataSet |
| Mode | Double | Gets the mode of a DataSet |
| StandartDeviation | Double | Gets the standart deviation of a DataSet |
| Skewness | Double | Gets the skewness of a DataSet |
| Kurtosis | Double | Gets the kurtosis of a DataSet |
| IsNormal | Boolean | Gets whether a dataset is normal distributed or not |
| IsUniform | Boolean | Gets whether a dataset is uniformly distributed or not |
| IsExponential | Boolean | Gets whether a dataset is exponentially distributed or not |

| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| CalculateZvalue | Double | Calculates the standart Z value for a given X value from a DataSet |
| Smooth | DataSet | Smooths a DataSet for a given alpha parameter |
| DetectOutliers | DataSet | Returns the outlier values of a DataSet |

### StationaryDataSet : DataSet
| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| MovingAverages | Double | Conducts point estimation of selected index using n-step moving averages |
| ExponentialSmoothing | Double | Conducts point estimation of selected index using simple exponential smoothing with the alpha parameter |

### TrendingDataSet : DataSet
| Properties | Data Type | Description |
| ---------- | --------- | ----------- |
| Slope | Double | Gets the slope of a trending DataSet |
| Intercept | Double | Gets the intercept of a trending DataSet |

| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| LinearRegression | Double | Conducts point estimation of independent variable X using linear regression line |
| HoltsMethod | Double | Conducts point estimation of independent variable X using Holt's Method |

### SeasonalDataSet : DataSet
| Properties | Data Type | Description |
| ---------- | --------- | ----------- |
| NumberOfSeasons | Integer | Gets or sets the number of seasons in a seasonal DataSet |
| SeasonSize | Integer | Gets the size of each season in a seasonal DataSet |
| SeasonalFactors | Array | Gets the seasonal factors of a seasonal DataSet |

| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| SeasonalEstimate | Double | Conducts point estimation of independent variable X considering seasonality |
| SeasonalEstimateWithTrend | Double | Conducts point estimation of independent variable X considering seasonality and trend |
| Deseasonalize | DataSet | Removes the seasonal effect from a SeasonalDataSet |

### Miscellaneous
| Static Methods | Data Type | Description |
| -------------- | --------- | ----------- |
| CalculateMeanAbsoluteError | Double | Returns the mean absolute forecast error of a DataSet |
| CalculateMeanSquaredError | Double | Returns the mean squared forecast error of a DataSet |
| Covariance | Double | Calculates covariance of two DataSets |
| Correlation | Double | Calculates coefficient of correlation between two DataSets |
| Rsquared | Double | Calculates coefficient of determination (R Squared) between two DataSets |

### Generators
| Static Methods | Data Type | Description |
| -------------- | --------- | ----------- |
| GeneratingFromDataGridView | Void | Generating collection members from WinForms DataGridView Control (+3 overloads) |
| GeneratingFromListBox | Void | Generating collection members from WinForms ListBox Control (+3 overloads) |
| GeneratingFromSQL | Void | Generating collection members from SQL Server (+3 overloads) |
| GeneratingFromAccessDB | Void | Generating collection members from MS Access Database (+3 overloads) |

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Used Technologies
Visual Studio  
C#  
.NET 6.0  
