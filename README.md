# CuriousLib
Ready-to-use C# Class Library for Statistical Computations

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
| StandartDeviation | Double | Gets the standart deviation of a DataSet |
| Skewness | Double | Gets the skewness of a DataSet |

| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| CalculateZvalue | Double | Calculates the standart Z value for a given X value from a DataSet |
| Smooth | DataSet | Smooths a DataSet for a given alpha parameter |

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

### StationaryDataSet : DataSet
| Properties | Data Type | Description |
| ---------- | --------- | ----------- |
| NumberOfSeasons | Integer | Gets or sets the number of seasons in a seasonal DataSet |
| SeasonSize | Integer | Gets or sets the size of each season in a seasonal DataSet |
| SeasonalFactors | Array | Gets the seasonal factors of a seasonal DataSet |

| Methods | Data Type | Description |
| ------- | --------- | ----------- |
| SeasonalEstimate | Double | Conducts point estimation of selected index using seasonal factors |

### Generators
| Static Methods | Data Type | Description |
| -------------- | --------- | ----------- |
| GeneratingFromDataGridView | Void | Generating collection members from WinForms DataGridView Control (+3 overloads) |
| GeneratingFromListBox | Void | Generating collection members from WinForms ListBox Control (+3 overloads) |

## License
[MIT](https://choosealicense.com/licenses/mit/)
