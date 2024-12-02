using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace CallMeKnightSolver.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class CallMeKnightBenchmarks
{
    [Params(20, 25, 1000, 10_000)]
    public int N { get; set; }

    //[Benchmark] public long Calculate() => CallMeKnight.Lib.CallMeKnightSolver.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateLeafs() => CallMeKnight.Lib.CallMeKnightSolverLeafs.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateLeafsPredefinedSteps() => CallMeKnight.Lib.CallMeKnightSolverLeafsPredefinedSteps.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateSequences() => CallMeKnight.Lib.CallMeKnightSolverSequences.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateSequencesImmutable() => CallMeKnight.Lib.CallMeKnightSolverSequencesImmutable.CalculateDistinctNumbers(N);
    //[Benchmark(Baseline = true)] public long CalculateSequences2Steps() => CallMeKnight.Lib.CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(N);
    //[Benchmark] public ulong CalculateFrequencies() => CallMeKnight.Lib.CallMeKnightSolverFrequencies.CalculateDistinctNumbers(N);
    [Benchmark(Baseline = true)] public BigInteger CalculateFrequenciesBigInteger() => CallMeKnight.Lib.CallMeKnightSolverFrequenciesBigInteger.CalculateDistinctNumbers(N);
    [Benchmark] public BigInteger CalculateAdjacent() => CallMeKnight.Lib.CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(N);
}

/*
 * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
   Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   Job=.NET 9.0  Runtime=.NET 9.0
   
   | Method                        | N  | Mean              | Error           | StdDev          | Gen0         | Gen1         | Gen2       | Allocated     |
   |------------------------------ |--- |------------------:|----------------:|----------------:|-------------:|-------------:|-----------:|--------------:|
   | Calculate                     | 4  |         37.875 us |       0.8610 us |       2.3714 us |       8.9722 |            - |          - |      41.42 KB |
   | CalculateLeafs                | 4  |         23.473 us |       0.4591 us |       0.7671 us |       4.2725 |            - |          - |      19.64 KB |
   | CalculateLeafsPredefinedSteps | 4  |          6.155 us |       0.1265 us |       0.3630 us |       5.3711 |       0.1831 |          - |      21.98 KB |
   | CalculateSequences            | 4  |          4.297 us |       0.0797 us |       0.0706 us |       0.8621 |            - |          - |       3.94 KB |
   | CalculateSequencesImmutable   | 4  |          4.365 us |       0.0774 us |       0.0922 us |       0.8621 |            - |          - |       3.96 KB |
   | CalculateSequences2Steps      | 4  |          4.533 us |       0.0893 us |       0.1609 us |       4.3030 |       0.1221 |          - |      17.17 KB |
   | Calculate                     | 10 |      5,590.335 us |     111.6021 us |     148.9856 us |    1312.5000 |     257.8125 |          - |    6284.75 KB |
   | CalculateLeafs                | 10 |      3,352.098 us |      66.6151 us |      93.3851 us |     628.9063 |       3.9063 |          - |    2901.42 KB |
   | CalculateLeafsPredefinedSteps | 10 |        174.699 us |       3.4423 us |       3.9641 us |      94.9707 |       0.2441 |          - |     382.99 KB |
   | CalculateSequences            | 10 |         69.877 us |       1.3849 us |       2.3139 us |      12.0850 |       0.6104 |          - |       49.6 KB |
   | CalculateSequencesImmutable   | 10 |         88.460 us |       3.7536 us |      11.0677 us |      12.0850 |       0.4883 |          - |       49.5 KB |
   | CalculateSequences2Steps      | 10 |         59.012 us |       1.1155 us |       2.1758 us |      10.6201 |       0.6104 |          - |      44.61 KB |
   | Calculate                     | 20 | 67,561,664.600 us | 329,059.2714 us | 307,802.2468 us | 3400000.0000 | 1028000.0000 | 32000.0000 | 26255092.6 KB |
   | CalculateLeafs                | 20 | 13,381,868.560 us | 174,790.0677 us | 163,498.7378 us | 2202000.0000 |   21000.0000 | 20000.0000 | 11470507.2 KB |
   | CalculateLeafsPredefinedSteps | 20 |    391,768.415 us |   8,566.9197 us |  25,259.7578 us |    8000.0000 |    8000.0000 |  8000.0000 | 1441832.59 KB |
   | CalculateSequences            | 20 |    165,850.344 us |   3,309.1878 us |   6,608.8060 us |    3000.0000 |    3000.0000 |  3000.0000 |  180234.47 KB |
   | CalculateSequencesImmutable   | 20 |    150,707.458 us |   3,003.9711 us |   8,273.8095 us |    2333.3333 |    2333.3333 |  2333.3333 |  180234.04 KB |
   | CalculateSequences2Steps      | 20 |    114,855.586 us |   2,253.2482 us |   3,508.0362 us |    2600.0000 |    2600.0000 |  2600.0000 |   73769.12 KB |


   // * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
   Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   
   | Method                         | Job      | Runtime  | N  | Mean            | Error         | StdDev         | Median          | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated     | Alloc Ratio |
   |------------------------------- |--------- |--------- |--- |----------------:|--------------:|---------------:|----------------:|------:|--------:|----------:|----------:|----------:|--------------:|------------:|
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 20 |   150,045.61 us |  2,988.550 us |   5,464.733 us |   150,789.64 us | 1.001 |    0.05 | 1750.0000 | 1750.0000 | 1750.0000 |    73768.6 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 20 |        28.90 us |      0.257 us |       0.201 us |        28.89 us | 0.000 |    0.00 |   24.1699 |    0.1526 |         - |     105.37 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20 |        68.53 us |      1.580 us |       4.583 us |        67.96 us | 0.000 |    0.00 |   19.4092 |         - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 20 |   128,390.23 us |  2,544.757 us |   7,135.772 us |   126,660.20 us | 1.003 |    0.08 | 2200.0000 | 2200.0000 | 2200.0000 |   73768.85 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 20 |        27.41 us |      0.408 us |       0.341 us |        27.45 us | 0.000 |    0.00 |   23.6511 |    0.0610 |         - |     103.34 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20 |        65.07 us |      1.348 us |       3.953 us |        64.61 us | 0.001 |    0.00 |   19.4092 |         - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 25 | 4,432,236.18 us | 85,236.321 us |  98,158.274 us | 4,401,777.10 us | 1.000 |    0.03 | 7000.0000 | 7000.0000 | 7000.0000 | 2097197.84 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 25 |        36.99 us |      1.021 us |       2.995 us |        36.04 us | 0.000 |    0.00 |   28.6255 |    0.1831 |         - |     127.19 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25 |        86.84 us |      1.693 us |       2.966 us |        86.73 us | 0.000 |    0.00 |   24.6582 |         - |         - |     113.72 KB |       0.000 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 25 | 3,086,478.18 us | 61,641.609 us | 145,296.536 us | 3,040,724.35 us | 1.002 |    0.07 | 7000.0000 | 7000.0000 | 7000.0000 | 2097197.73 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 25 |        35.97 us |      0.936 us |       2.745 us |        35.17 us | 0.000 |    0.00 |   28.1982 |    0.1831 |         - |     125.09 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25 |        81.71 us |      1.560 us |       2.607 us |        81.78 us | 0.000 |    0.00 |   24.6582 |         - |         - |     113.72 KB |       0.000 |
   

// * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
   12th Gen Intel Core i7-12800H, 1 CPU, 20 logical and 14 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   
   | Method                         | Job      | Runtime  | N  | Mean            | Error         | StdDev         | Median          | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated     | Alloc Ratio |
   |------------------------------- |--------- |--------- |--- |----------------:|--------------:|---------------:|----------------:|------:|--------:|----------:|----------:|----------:|--------------:|------------:|
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 20 |    79,051.50 us |  1,580.888 us |   3,600.481 us |    78,987.61 us | 1.002 |    0.06 | 2000.0000 | 2000.0000 | 2000.0000 |   73771.23 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 20 |        14.78 us |      0.289 us |       0.242 us |        14.78 us | 0.000 |    0.00 |    9.0027 |    0.7782 |         - |     105.35 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20 |        43.13 us |      2.847 us |       8.395 us |        43.69 us | 0.001 |    0.00 |    7.2632 |         - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 20 |    78,277.31 us |  3,181.529 us |   9,330.875 us |    80,821.19 us | 1.017 |    0.19 | 1555.5556 | 1555.5556 | 1555.5556 |   73771.77 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 20 |        17.66 us |      0.343 us |       0.381 us |        17.61 us | 0.000 |    0.00 |    8.9722 |    0.7629 |         - |        105 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20 |        56.13 us |      3.871 us |      11.415 us |        57.80 us | 0.001 |    0.00 |    7.2632 |         - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 25 | 2,527,283.50 us | 49,896.130 us | 118,583.441 us | 2,518,603.20 us | 1.002 |    0.07 | 8000.0000 | 8000.0000 | 8000.0000 | 2097198.36 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 25 |        20.41 us |      0.405 us |       0.554 us |        20.27 us | 0.000 |    0.00 |   10.5286 |    0.9460 |         - |      124.4 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25 |        67.12 us |      2.203 us |       6.495 us |        68.52 us | 0.000 |    0.00 |    9.2773 |         - |         - |     113.72 KB |       0.000 |
   |                                |          |          |    |                 |               |                |                 |       |         |           |           |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 25 | 1,582,812.18 us | 31,116.636 us |  52,838.413 us | 1,579,046.80 us | 1.001 |    0.05 | 8000.0000 | 8000.0000 | 8000.0000 |  2097198.4 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 25 |        19.01 us |      0.379 us |       0.493 us |        18.98 us | 0.000 |    0.00 |   10.5286 |    0.9460 |         - |      123.8 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25 |        60.17 us |      2.194 us |       6.469 us |        62.79 us | 0.000 |    0.00 |    9.2773 |         - |         - |     113.72 KB |       0.000 |
   
   // * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 10 (10.0.20348.2762) (Hyper-V)
   Intel Xeon Gold 6426Y, 1 CPU, 8 logical and 4 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
   
   
   | Method                         | Job      | Runtime  | N  | Mean            | Error         | StdDev         | Median          | Ratio | RatioSD | Gen0       | Gen1       | Gen2      | Allocated     | Alloc Ratio |
   |------------------------------- |--------- |--------- |--- |----------------:|--------------:|---------------:|----------------:|------:|--------:|-----------:|-----------:|----------:|--------------:|------------:|
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 20 |   109,997.41 us |  2,144.797 us |   3,401.876 us |   110,232.90 us | 1.001 |    0.04 |  2600.0000 |  2600.0000 | 1600.0000 |   73760.29 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 20 |        29.66 us |      0.591 us |       1.406 us |        29.31 us | 0.000 |    0.00 |     5.3101 |     0.3052 |         - |      98.71 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20 |        62.71 us |      1.488 us |       4.269 us |        61.06 us | 0.001 |    0.00 |     4.6387 |          - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |            |            |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 20 |    90,594.25 us |  2,096.578 us |   6,181.808 us |    89,368.71 us | 1.005 |    0.10 |  2571.4286 |  2571.4286 | 1857.1429 |   73760.37 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 20 |        35.54 us |      1.612 us |       4.651 us |        33.37 us | 0.000 |    0.00 |     5.2490 |     0.3052 |         - |      97.11 KB |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20 |        51.28 us |      0.947 us |       1.868 us |        50.85 us | 0.001 |    0.00 |     4.6387 |          - |         - |      89.69 KB |       0.001 |
   |                                |          |          |    |                 |               |                |                 |       |         |            |            |           |               |             |
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 25 | 3,004,940.20 us | 70,588.843 us | 207,024.872 us | 2,972,681.60 us | 1.005 |    0.10 | 11000.0000 | 11000.0000 | 6000.0000 | 2097190.19 KB |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 25 |        72.69 us |      7.192 us |      20.979 us |        72.96 us | 0.000 |    0.00 |     6.2866 |     0.4272 |         - |     117.22 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25 |        87.29 us |      1.708 us |       2.992 us |        86.70 us | 0.000 |    0.00 |     5.8594 |          - |         - |     113.72 KB |       0.000 |
   |                                |          |          |    |                 |               |                |                 |       |         |            |            |           |               |             |
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 25 | 2,380,063.45 us | 73,585.608 us | 215,813.866 us | 2,322,811.10 us | 1.008 |    0.13 | 11000.0000 | 11000.0000 | 6000.0000 | 2097190.17 KB |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 25 |        43.20 us |      2.296 us |       6.696 us |        40.44 us | 0.000 |    0.00 |     6.2866 |     0.4272 |         - |     117.63 KB |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25 |        64.10 us |      1.228 us |       1.681 us |        63.92 us | 0.000 |    0.00 |     5.8594 |          - |         - |     113.72 KB |       0.000 |
   

optimized in terms of swapping arrays instead of creating dictionaries for the frequency table
// * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
   Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   
   | Method                         | Job      | Runtime  | N  | Mean             | Error          | StdDev          | Median           | Ratio | RatioSD | Gen0      | Gen1      | Gen2      | Allocated    | Alloc Ratio |
   |------------------------------- |--------- |--------- |--- |-----------------:|---------------:|----------------:|-----------------:|------:|--------:|----------:|----------:|----------:|-------------:|------------:|
   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 20 |   160,805.420 us |  3,170.1079 us |   4,935.4765 us |   160,875.913 us | 1.001 |    0.04 | 1750.0000 | 1750.0000 | 1750.0000 |   75540002 B |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 20 |        34.896 us |      0.6548 us |       0.9179 us |        34.499 us | 0.000 |    0.00 |   24.8413 |    0.8545 |         - |     110601 B |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20 |         4.655 us |      0.0863 us |       0.0807 us |         4.652 us | 0.000 |    0.00 |    0.0763 |         - |         - |        368 B |       0.000 |
   
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 20 |   111,953.300 us |  2,213.9115 us |   5,635.1121 us |   111,361.550 us | 1.002 |    0.07 | 2000.0000 | 1800.0000 | 1800.0000 |   75539096 B |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 20 |        29.045 us |      0.5332 us |       1.0524 us |        28.724 us | 0.000 |    0.00 |   24.2920 |    0.3052 |         - |     108679 B |       0.001 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20 |         2.927 us |      0.0458 us |       0.0471 us |         2.929 us | 0.000 |    0.00 |    0.0763 |         - |         - |        368 B |       0.000 |

   | CalculateSequences2Steps       | .NET 8.0 | .NET 8.0 | 25 | 3,631,587.320 us | 70,676.9016 us | 156,615.1438 us | 3,624,515.100 us | 1.002 |    0.06 | 8000.0000 | 8000.0000 | 8000.0000 | 2147531088 B |       1.000 |
   | CalculateFrequencies           | .NET 8.0 | .NET 8.0 | 25 |        38.719 us |      0.7655 us |       1.3408 us |        38.063 us | 0.000 |    0.00 |   29.1748 |    0.3662 |         - |     132645 B |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25 |         5.843 us |      0.1141 us |       0.1121 us |         5.837 us | 0.000 |    0.00 |    0.0992 |         - |         - |        496 B |       0.000 |
   
   | CalculateSequences2Steps       | .NET 9.0 | .NET 9.0 | 25 | 2,654,386.895 us | 49,484.5973 us |  60,771.5347 us | 2,648,842.450 us | 1.001 |    0.03 | 8000.0000 | 8000.0000 | 8000.0000 | 2147531248 B |       1.000 |
   | CalculateFrequencies           | .NET 9.0 | .NET 9.0 | 25 |        37.892 us |      1.1415 us |       3.3479 us |        36.700 us | 0.000 |    0.00 |   28.6255 |    0.3052 |         - |     129980 B |       0.000 |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25 |         3.781 us |      0.0716 us |       0.1327 us |         3.746 us | 0.000 |    0.00 |    0.0992 |         - |         - |        496 B |       0.000 |
   

// * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
   Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   
   | Method                         | Job      | Runtime  | N     | Mean          | Error       | StdDev        | Gen0       | Gen1     | Allocated   |
   |------------------------------- |--------- |--------- |------ |--------------:|------------:|--------------:|-----------:|---------:|------------:|
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20    |      4.757 us |   0.0931 us |     0.1449 us |     0.0763 |        - |       368 B |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20    |      2.876 us |   0.0557 us |     0.0833 us |     0.0763 |        - |       368 B |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25    |      5.649 us |   0.0863 us |     0.0807 us |     0.0992 |        - |       496 B |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25    |      3.671 us |   0.0732 us |     0.1074 us |     0.1030 |        - |       496 B |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 1000  |    905.989 us |  17.7791 us |    26.0604 us |   432.6172 |   5.8594 |   2_035_808 B |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 1000  |    968.900 us |  17.3693 us |    29.4943 us |   431.6406 |   5.8594 |   2_035_809 B |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 10000 | 38,589.702 us | 756.1688 us | 1,344.0894 us | 32846.1538 | 384.6154 | 154_850_249 B |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 10000 | 38,924.189 us | 774.8648 us | 1,780.3809 us | 32857.1429 | 357.1429 | 154_850_197 B |
   


   // * Summary *
   
   BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
   Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
   .NET SDK 9.0.100
     [Host]   : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
     .NET 8.0 : .NET 8.0.11 (8.0.1124.51707), X64 RyuJIT AVX2
     .NET 9.0 : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
   
   
   | Method                         | Job      | Runtime  | N     | Mean          | Error       | StdDev        | Ratio | RatioSD | Gen0       | Gen1     | Allocated   | Alloc Ratio |
   |------------------------------- |--------- |--------- |------ |--------------:|------------:|--------------:|------:|--------:|-----------:|---------:|------------:|------------:|
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 20    |      4.662 us |   0.0923 us |     0.1712 us |  1.00 |    0.05 |     0.0763 |        - |       368 B |        1.00 |
   | CalculateAdjacent              | .NET 8.0 | .NET 8.0 | 20    |     67.307 us |   0.4849 us |     0.4299 us | 14.46 |    0.53 |     8.1787 |   0.4883 |     37842 B |      102.83 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 20    |      2.829 us |   0.0559 us |     0.1128 us |  1.00 |    0.06 |     0.0763 |        - |       368 B |        1.00 |
   | CalculateAdjacent              | .NET 9.0 | .NET 9.0 | 20    |     51.797 us |   0.7875 us |     0.7366 us | 18.34 |    0.76 |     7.8735 |   0.4883 |     36711 B |       99.76 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 25    |      6.204 us |   0.1023 us |     0.0957 us |  1.00 |    0.02 |     0.0992 |        - |       496 B |        1.00 |
   | CalculateAdjacent              | .NET 8.0 | .NET 8.0 | 25    |     60.182 us |   1.1720 us |     1.9256 us |  9.70 |    0.34 |     7.0801 |   0.3662 |     33040 B |       66.61 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 25    |      3.654 us |   0.0676 us |     0.0632 us |  1.00 |    0.02 |     0.0992 |        - |       496 B |        1.00 |
   | CalculateAdjacent              | .NET 9.0 | .NET 9.0 | 25    |     44.363 us |   0.8031 us |     0.8247 us | 12.15 |    0.30 |     6.8359 |   0.2441 |     32174 B |       64.87 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 1000  |    899.028 us |  17.9100 us |    29.4267 us |  1.00 |    0.05 |   432.6172 |   5.8594 |   2035808 B |        1.00 |
   | CalculateAdjacent              | .NET 8.0 | .NET 8.0 | 1000  |    327.942 us |   6.2979 us |     6.1854 us |  0.37 |    0.01 |   108.3984 |   1.9531 |    502777 B |        0.25 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 1000  |    965.317 us |  18.8864 us |    28.8416 us |  1.00 |    0.04 |   431.6406 |   5.8594 |   2035809 B |        1.00 |
   | CalculateAdjacent              | .NET 9.0 | .NET 9.0 | 1000  |    288.627 us |   6.6439 us |    19.2752 us |  0.30 |    0.02 |   107.4219 |   1.9531 |    501091 B |        0.25 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 8.0 | .NET 8.0 | 10000 | 38,057.731 us | 759.6699 us | 1,204.9171 us |  1.00 |    0.04 | 32857.1429 | 357.1429 | 154850245 B |        1.00 |
   | CalculateAdjacent              | .NET 8.0 | .NET 8.0 | 10000 |  2,320.549 us |  45.6681 us |    68.3538 us |  0.06 |    0.00 |   625.0000 | 265.6250 |   3134441 B |        0.02 |
   |                                |          |          |       |               |             |               |       |         |            |          |             |             |
   | CalculateFrequenciesBigInteger | .NET 9.0 | .NET 9.0 | 10000 | 37,131.185 us | 737.3874 us | 1,080.8531 us |  1.00 |    0.04 | 32857.1429 | 357.1429 | 154850245 B |        1.00 |
   | CalculateAdjacent              | .NET 9.0 | .NET 9.0 | 10000 |  2,001.438 us |  39.1050 us |    46.5517 us |  0.05 |    0.00 |   632.8125 | 218.7500 |   3132511 B |        0.02 |
   
   // * Warnings *
   MultimodalDistribution
     CallMeKnightBenchmarks.CalculateFrequenciesBigInteger: .NET 9.0 -> It seems that the distribution can have several modes (mValue = 3)
   
 */