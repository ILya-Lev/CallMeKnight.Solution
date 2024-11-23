using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace CallMeKnightSolver.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net80)]
[MemoryDiagnoser]
public class CallMeKnightBenchmarks
{
    [Params(20, 25)]
    public int N { get; set; }

    //[Benchmark] public long Calculate() => CallMeKnight.Lib.CallMeKnightSolver.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateLeafs() => CallMeKnight.Lib.CallMeKnightSolverLeafs.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateLeafsPredefinedSteps() => CallMeKnight.Lib.CallMeKnightSolverLeafsPredefinedSteps.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateSequences() => CallMeKnight.Lib.CallMeKnightSolverSequences.CalculateDistinctNumbers(N);
    //[Benchmark] public long CalculateSequencesImmutable() => CallMeKnight.Lib.CallMeKnightSolverSequencesImmutable.CalculateDistinctNumbers(N);
    [Benchmark(Baseline = true)] public long CalculateSequences2Steps() => CallMeKnight.Lib.CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(N);
    [Benchmark] public ulong CalculateFrequencies() => CallMeKnight.Lib.CallMeKnightSolverFrequencies.CalculateDistinctNumbers(N);
    [Benchmark] public BigInteger CalculateFrequenciesBigInteger() => CallMeKnight.Lib.CallMeKnightSolverFrequenciesBigInteger.CalculateDistinctNumbers(N);
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
   
 */