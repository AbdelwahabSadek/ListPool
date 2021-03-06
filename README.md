# ListPool
ListPool is an allocation free implementation of IList using ArrayPool.

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/faustodavid/ListPool/Build)
![Coveralls github](https://img.shields.io/coveralls/github/faustodavid/ListPool)
![Nuget](https://img.shields.io/nuget/v/ListPool)
![GitHub](https://img.shields.io/github/license/faustodavid/ListPool)

## Benchmarks.

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.17134.1006 (1803/April2018Update/Redstone4)
Intel Core i5-4570 CPU 3.20GHz (Haswell), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT  [AttachedDebugger]
  Job-HLWBMP : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT

Concurrent=True  Server=True

### Add

```
|   Method |    N |        Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----- |------------:|----------:|----------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |   10 |    94.94 ns |  22.13 ns |  57.51 ns |   100.0 ns |     ? |       ? |    1 |     - |     - |     - |         - |
|     List |   10 |   146.43 ns |  34.40 ns |  92.42 ns |   100.0 ns |     ? |       ? |    1 |     - |     - |     - |         - |
|          |      |             |           |           |            |       |         |      |       |       |       |           |
|     List |  100 |   351.16 ns |  60.92 ns | 165.74 ns |   300.0 ns |     ? |       ? |    1 |     - |     - |     - |         - |
| ListPool |  100 |   731.18 ns | 220.20 ns | 624.67 ns |   400.0 ns |     ? |       ? |    2 |     - |     - |     - |         - |
|          |      |             |           |           |            |       |         |      |       |       |       |           |
|     List | 1000 | 2,118.87 ns |  46.08 ns |  96.19 ns | 2,100.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 1000 | 2,342.31 ns |  84.12 ns | 217.14 ns | 2,300.0 ns |  1.09 |    0.08 |    2 |     - |     - |     - |         - |
```

### Clear

```
|   Method |    N | CapacityFilled |        Mean |     Error |      StdDev |      Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----- |--------------- |------------:|----------:|------------:|------------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool | 1000 |            0.1 |    48.81 ns |  23.47 ns |    63.04 ns |   0.0000 ns |  0.28 |    0.40 |    1 |     - |     - |     - |         - |
|     List | 1000 |            0.1 |   222.09 ns |  30.57 ns |    83.17 ns | 200.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |      |                |             |           |             |             |       |         |      |       |       |       |           |
|     List | 1000 |            0.5 |   300.56 ns |  86.73 ns |   240.32 ns | 250.0000 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 1000 |            0.5 | 1,371.13 ns | 747.20 ns | 2,167.75 ns | 100.0000 ns |  5.86 |   11.83 |    2 |     - |     - |     - |         - |
|          |      |                |             |           |             |             |       |         |      |       |       |       |           |
| ListPool | 1000 |            0.8 |    97.83 ns |  40.02 ns |   112.88 ns | 100.0000 ns |  0.52 |    0.67 |    1 |     - |     - |     - |         - |
|     List | 1000 |            0.8 |   208.05 ns |  25.05 ns |    68.57 ns | 200.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |      |                |             |           |             |             |       |         |      |       |       |       |           |
|     List | 1000 |              1 |   342.86 ns |  91.96 ns |   247.05 ns | 200.0000 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 1000 |              1 | 1,404.08 ns | 751.66 ns | 2,192.63 ns | 100.0000 ns |  7.04 |   13.05 |    1 |     - |     - |     - |         - |
```

### Contains

```
|   Method |     N |       Mean |      Error |     StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------ |-----------:|-----------:|-----------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |    10 |   756.1 ns |   110.7 ns |   323.0 ns |   600.0 ns |  0.87 |    1.28 |    1 |     - |     - |     - |         - |
|     List |    10 | 3,625.5 ns | 1,460.7 ns | 4,260.8 ns | 1,150.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |            |            |            |            |       |         |      |       |       |       |           |
|     List |   100 | 1,326.6 ns |   365.1 ns |   948.9 ns | 1,100.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |   100 | 3,003.0 ns | 1,308.9 ns | 3,838.8 ns |   900.0 ns |  1.39 |    2.39 |    1 |     - |     - |     - |         - |
|          |       |            |            |            |            |       |         |      |       |       |       |           |
| ListPool |  1000 | 1,021.1 ns |   179.2 ns |   514.2 ns |   800.0 ns |  0.59 |    0.51 |    1 |     - |     - |     - |         - |
|     List |  1000 | 3,803.1 ns | 1,329.2 ns | 3,877.3 ns | 1,800.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |            |            |            |            |       |         |      |       |       |       |           |
|     List | 10000 | 3,747.8 ns |   681.2 ns | 1,887.6 ns | 2,950.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 10000 | 6,524.5 ns | 1,653.2 ns | 4,716.7 ns | 4,200.0 ns |  2.15 |    1.85 |    2 |     - |     - |     - |         - |
```


### CopyTo

```
|   Method |     N |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------ |-----------:|----------:|-----------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |    10 |   562.6 ns |  67.27 ns |   184.1 ns |   550.0 ns |  1.04 |    0.54 |    1 |     - |     - |     - |         - |
|     List |    10 |   632.6 ns | 104.64 ns |   284.7 ns |   500.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
|     List |   100 |   567.0 ns |  53.18 ns |   149.1 ns |   500.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |   100 |   702.5 ns | 146.05 ns |   382.2 ns |   600.0 ns |  1.38 |    1.07 |    1 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
|     List |  1000 |   757.0 ns |  52.45 ns |   142.7 ns |   700.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |  1000 |   805.8 ns |  70.50 ns |   191.8 ns |   800.0 ns |  1.10 |    0.33 |    1 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
| ListPool | 10000 | 2,984.1 ns | 125.52 ns |   345.7 ns | 3,000.0 ns |  0.78 |    0.23 |    1 |     - |     - |     - |         - |
|     List | 10000 | 4,618.9 ns | 743.17 ns | 2,132.3 ns | 3,600.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
```

### CreateAndAdd

```
|   Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD | Rank |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|--------- |------ |-------------:|-----------:|-----------:|------:|--------:|-----:|-------:|-------:|------:|----------:|
|     List |    10 |     40.02 ns |   0.873 ns |   2.461 ns |  1.00 |    0.00 |    1 | 0.0064 |      - |     - |      96 B |
| ListPool |    10 |     54.73 ns |   0.422 ns |   0.394 ns |  1.38 |    0.10 |    2 |      - |      - |     - |         - |
|          |       |              |            |            |       |         |      |        |        |       |           |
| ListPool |   100 |    217.37 ns |   0.775 ns |   0.725 ns |  0.81 |    0.03 |    1 |      - |      - |     - |         - |
|     List |   100 |    263.99 ns |   5.228 ns |   8.589 ns |  1.00 |    0.00 |    2 | 0.0300 |      - |     - |     456 B |
|          |       |              |            |            |       |         |      |        |        |       |           |
| ListPool |  1000 |  1,826.03 ns |   5.414 ns |   4.521 ns |  0.75 |    0.01 |    1 |      - |      - |     - |         - |
|     List |  1000 |  2,426.96 ns |  37.785 ns |  33.496 ns |  1.00 |    0.00 |    2 | 0.2670 |      - |     - |    4056 B |
|          |       |              |            |            |       |         |      |        |        |       |           |
| ListPool | 10000 | 18,055.16 ns | 154.166 ns | 136.665 ns |  0.76 |    0.00 |    1 |      - |      - |     - |         - |
|     List | 10000 | 23,721.46 ns | 190.536 ns | 159.106 ns |  1.00 |    0.00 |    2 | 2.6550 | 0.1526 |     - |   40056 B |
```

### Create

```
|   Method |     N |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD | Rank |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|--------- |------ |------------:|-----------:|-----------:|------------:|------:|--------:|-----:|-------:|-------:|------:|----------:|
|     List |    10 |    19.08 ns |   0.411 ns |   0.993 ns |    18.76 ns |  1.00 |    0.00 |    1 | 0.0063 |      - |     - |      96 B |
| ListPool |    10 |    36.64 ns |   0.765 ns |   1.190 ns |    35.96 ns |  1.92 |    0.11 |    2 |      - |      - |     - |         - |
|          |       |             |            |            |             |       |         |      |        |        |       |           |
| ListPool |   100 |    35.70 ns |   0.110 ns |   0.086 ns |    35.72 ns |  0.55 |    0.03 |    1 |      - |      - |     - |         - |
|     List |   100 |    64.86 ns |   1.459 ns |   4.066 ns |    64.06 ns |  1.00 |    0.00 |    2 | 0.0300 |      - |     - |     456 B |
|          |       |             |            |            |             |       |         |      |        |        |       |           |
| ListPool |  1000 |    35.96 ns |   0.320 ns |   0.299 ns |    35.91 ns |  0.07 |    0.00 |    1 |      - |      - |     - |         - |
|     List |  1000 |   499.07 ns |  10.784 ns |  30.417 ns |   486.87 ns |  1.00 |    0.00 |    2 | 0.2689 | 0.0010 |     - |    4056 B |
|          |       |             |            |            |             |       |         |      |        |        |       |           |
| ListPool | 10000 |    37.07 ns |   0.393 ns |   0.367 ns |    37.13 ns | 0.008 |    0.00 |    1 |      - |      - |     - |         - |
|     List | 10000 | 4,333.76 ns | 141.489 ns | 405.960 ns | 4,255.24 ns | 1.000 |    0.00 |    2 | 2.6627 | 0.1678 |     - |   40056 B |
```

### Enumerate

```
|   Method |    N | CapacityFilled |        Mean |    Error |    StdDev |        Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----- |--------------- |------------:|---------:|----------:|--------------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool | 1000 |            0.1 |    43.33 ns | 21.54 ns |  60.06 ns |     0.0000 ns |  0.08 |    0.13 |    1 |     - |     - |     - |         - |
|     List | 1000 |            0.1 |   595.65 ns | 75.06 ns | 211.70 ns |   500.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |      |                |             |          |           |               |       |         |      |       |       |       |           |
| ListPool | 1000 |            0.5 |   217.35 ns | 25.11 ns |  73.25 ns |   200.0000 ns |  0.14 |    0.05 |    1 |     - |     - |     - |         - |
|     List | 1000 |            0.5 | 1,513.04 ns | 33.99 ns |  82.09 ns | 1,500.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |      |                |             |          |           |               |       |         |      |       |       |       |           |
| ListPool | 1000 |            0.8 |   430.53 ns | 25.46 ns |  73.04 ns |   400.0000 ns |  0.20 |    0.03 |    1 |     - |     - |     - |         - |
|     List | 1000 |            0.8 | 2,121.25 ns | 48.14 ns | 125.98 ns | 2,100.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |      |                |             |          |           |               |       |         |      |       |       |       |           |
| ListPool | 1000 |              1 |   437.36 ns | 28.84 ns |  78.95 ns |   450.0000 ns |  0.16 |    0.03 |    1 |     - |     - |     - |         - |
|     List | 1000 |              1 | 2,709.41 ns | 64.22 ns | 173.63 ns | 2,700.0000 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
```

### IndexOf

```
|   Method |     N |       Mean |       Error |     StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------ |-----------:|------------:|-----------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |    10 |   821.9 ns |   154.53 ns |   445.8 ns |   600.0 ns |  0.89 |    0.67 |    1 |     - |     - |     - |         - |
|     List |    10 | 1,143.4 ns |   192.89 ns |   565.7 ns | 1,000.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |            |             |            |            |       |         |      |       |       |       |           |
| ListPool |   100 |   765.8 ns |   138.71 ns |   360.5 ns |   600.0 ns |  0.79 |    0.50 |    1 |     - |     - |     - |         - |
|     List |   100 | 1,158.8 ns |   202.81 ns |   588.4 ns |   800.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |            |             |            |            |       |         |      |       |       |       |           |
| ListPool |  1000 |   675.0 ns |    68.59 ns |   188.9 ns |   600.0 ns |  0.60 |    0.48 |    1 |     - |     - |     - |         - |
|     List |  1000 | 3,182.5 ns | 1,299.97 ns | 3,771.4 ns |   900.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |            |             |            |            |       |         |      |       |       |       |           |
|     List | 10000 | 2,069.0 ns |   114.25 ns |   306.9 ns | 1,900.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 10000 | 2,310.2 ns |   333.31 ns |   918.0 ns | 1,900.0 ns |  1.14 |    0.49 |    1 |     - |     - |     - |         - |
```

### Insert

```
|   Method |      N |         Mean |        Error |      StdDev |       Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------- |-------------:|-------------:|------------:|-------------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |   1000 |     794.6 ns |     96.03 ns |    272.4 ns |     700.0 ns |  0.19 |    0.11 |    1 |     - |     - |     - |         - |
|     List |   1000 |   5,704.2 ns |  1,218.07 ns |  3,514.4 ns |   4,150.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |    8024 B |
|          |        |              |              |             |              |       |         |      |       |       |       |           |
| ListPool |  10000 |   2,214.9 ns |    298.78 ns |    852.4 ns |   1,900.0 ns |  0.20 |    0.10 |    1 |     - |     - |     - |         - |
|     List |  10000 |  12,292.6 ns |  1,310.10 ns |  3,737.8 ns |  12,900.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |   80024 B |
|          |        |              |              |             |              |       |         |      |       |       |       |           |
| ListPool | 100000 |  16,501.0 ns |  2,957.83 ns |  8,534.0 ns |  12,750.0 ns |  0.09 |    0.06 |    1 |     - |     - |     - |         - |
|     List | 100000 | 196,619.6 ns | 18,795.66 ns | 54,529.6 ns | 192,400.0 ns |  1.00 |    0.00 |    2 |     - |     - |     - |  800024 B |
```

### RemoveAt

```
|   Method |     N |       Mean |     Error |     StdDev |     Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------ |-----------:|----------:|-----------:|-----------:|------:|--------:|-----:|------:|------:|------:|----------:|
|     List |    10 |   554.8 ns |  68.27 ns |   193.7 ns |   500.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |    10 |   713.7 ns |  93.69 ns |   268.8 ns |   600.0 ns |  1.44 |    0.77 |    2 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
|     List |   100 |   802.5 ns | 132.27 ns |   343.8 ns |   700.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |   100 | 2,471.1 ns | 966.81 ns | 2,804.9 ns | 1,000.0 ns |  3.93 |    4.77 |    2 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
| ListPool |  1000 |   665.7 ns |  69.86 ns |   193.6 ns |   650.0 ns |  1.03 |    0.50 |    1 |     - |     - |     - |         - |
|     List |  1000 |   743.7 ns | 121.88 ns |   333.7 ns |   600.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
|          |       |            |           |            |            |       |         |      |       |       |       |           |
|     List | 10000 | 1,289.2 ns | 163.09 ns |   435.3 ns | 1,100.0 ns |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool | 10000 | 3,421.6 ns | 974.75 ns | 2,827.9 ns | 1,800.0 ns |  2.52 |    2.25 |    2 |     - |     - |     - |         - |
```

### Remove

```
|   Method |     N |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |------ |---------:|----------:|----------:|---------:|------:|--------:|-----:|------:|------:|------:|----------:|
| ListPool |    10 | 1.245 us | 0.1426 us | 0.4091 us | 1.200 us |  0.74 |    0.61 |    1 |     - |     - |     - |         - |
|     List |    10 | 3.854 us | 1.3575 us | 3.9382 us | 2.000 us |  1.00 |    0.00 |    2 |     - |     - |     - |         - |
|          |       |          |           |           |          |       |         |      |       |       |       |           |
|     List |   100 | 1.270 us | 0.1669 us | 0.4815 us | 1.000 us |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
| ListPool |   100 | 1.622 us | 0.2998 us | 0.8155 us | 1.350 us |  1.43 |    0.87 |    2 |     - |     - |     - |         - |
|          |       |          |           |           |          |       |         |      |       |       |       |           |
| ListPool |  1000 | 1.332 us | 0.1523 us | 0.4322 us | 1.100 us |  1.06 |    0.47 |    1 |     - |     - |     - |         - |
|     List |  1000 | 1.421 us | 0.2084 us | 0.5980 us | 1.100 us |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
|          |       |          |           |           |          |       |         |      |       |       |       |           |
| ListPool | 10000 | 3.253 us | 0.3207 us | 0.8887 us | 2.850 us |  1.00 |    0.34 |    1 |     - |     - |     - |         - |
|     List | 10000 | 3.501 us | 0.4613 us | 1.2706 us | 2.900 us |  1.00 |    0.00 |    1 |     - |     - |     - |         - |
```

### ArrayToList

```
|   Method |    N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Rank |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|--------- |----- |----------:|----------:|----------:|----------:|------:|--------:|-----:|-------:|-------:|------:|----------:|
|     List |   10 |  61.77 ns |  1.223 ns |  1.502 ns |  61.23 ns |  1.00 |    0.00 |    1 | 0.0063 |      - |     - |      96 B |
| ListPool |   10 |  82.70 ns |  0.443 ns |  0.415 ns |  82.68 ns |  1.34 |    0.04 |    2 |      - |      - |     - |         - |
|          |      |           |           |           |           |       |         |      |        |        |       |           |
| ListPool |   50 |  93.70 ns |  0.394 ns |  0.369 ns |  93.76 ns |  0.98 |    0.04 |    1 |      - |      - |     - |         - |
|     List |   50 |  95.46 ns |  1.935 ns |  3.681 ns |  94.34 ns |  1.00 |    0.00 |    1 | 0.0169 |      - |     - |     256 B |
|          |      |           |           |           |           |       |         |      |        |        |       |           |
| ListPool |  100 |  99.07 ns |  1.443 ns |  1.350 ns |  99.23 ns |  0.77 |    0.05 |    1 |      - |      - |     - |         - |
|     List |  100 | 123.76 ns |  2.497 ns |  7.125 ns | 122.58 ns |  1.00 |    0.00 |    2 | 0.0300 |      - |     - |     456 B |
|          |      |           |           |           |           |       |         |      |        |        |       |           |
| ListPool | 1000 | 165.41 ns |  1.209 ns |  1.131 ns | 165.12 ns |  0.24 |    0.02 |    1 |      - |      - |     - |         - |
|     List | 1000 | 715.83 ns | 33.077 ns | 94.370 ns | 685.85 ns |  1.00 |    0.00 |    2 | 0.2689 | 0.0010 |     - |    4056 B |
```

### EnumerableToList

```
|   Method |    N |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------- |----- |----------:|----------:|----------:|----------:|------:|--------:|-----:|------:|------:|------:|----------:|
|     Linq |   10 |  2.145 us | 0.2244 us | 0.5872 us |  1.900 us |  1.00 |    0.00 |    1 |     - |     - |     - |      96 B |
| ListPool |   10 |  2.511 us | 0.1792 us | 0.4965 us |  2.300 us |  1.25 |    0.37 |    2 |     - |     - |     - |         - |
|          |      |           |           |           |           |       |         |      |       |       |       |           |
| ListPool |   50 |  3.059 us | 0.1619 us | 0.4514 us |  2.900 us |  0.94 |    0.49 |    1 |     - |     - |     - |         - |
|     Linq |   50 |  5.167 us | 1.2581 us | 3.6699 us |  3.150 us |  1.00 |    0.00 |    1 |     - |     - |     - |     256 B |
|          |      |           |           |           |           |       |         |      |       |       |       |           |
|     Linq |  100 |  2.235 us | 0.1103 us | 0.3001 us |  2.150 us |  1.00 |    0.00 |    1 |     - |     - |     - |     456 B |
| ListPool |  100 |  4.292 us | 0.3948 us | 1.1072 us |  3.950 us |  1.96 |    0.54 |    2 |     - |     - |     - |         - |
|          |      |           |           |           |           |       |         |      |       |       |       |           |
|     Linq | 1000 |  5.023 us | 0.1779 us | 0.4717 us |  4.900 us |  1.00 |    0.00 |    1 |     - |     - |     - |    4056 B |
| ListPool | 1000 | 14.714 us | 0.2999 us | 0.7300 us | 14.400 us |  2.94 |    0.24 |    2 |     - |     - |     - |         - |
```

## Contributors
A big thanks to the project contributors!
* [Sergey Mokin](https://github.com/SergeyMokin)
