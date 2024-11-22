using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace CallMeKnightSolver.Benchmarks;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class CallMeKnightBenchmarks
{
    [Params(4, 10, 20)]
    public int N { get; set; }

    [Benchmark] public long Calculate() => CallMeKnight.Lib.CallMeKnightSolver.CalculateDistinctNumbers(N);
    [Benchmark] public long CalculateLeafs() => CallMeKnight.Lib.CallMeKnightSolverLeafs.CalculateDistinctNumbers(N);
    [Benchmark] public long CalculateLeafsPredefinedSteps() => CallMeKnight.Lib.CallMeKnightSolverLeafsPredefinedSteps.CalculateDistinctNumbers(N);
    [Benchmark] public long CalculateSequences() => CallMeKnight.Lib.CallMeKnightSolverSequences.CalculateDistinctNumbers(N);
    [Benchmark] public long CalculateSequencesImmutable() => CallMeKnight.Lib.CallMeKnightSolverSequencesImmutable.CalculateDistinctNumbers(N);
    [Benchmark] public long CalculateSequences2Steps() => CallMeKnight.Lib.CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(N);
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
 */