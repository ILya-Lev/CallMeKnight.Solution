using System.Collections.Immutable;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverSequencesImmutable
{
    public static long CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        if (n == 1) return 10;//for button with digit 5 - it is the only case to take it into account

        var steps = new byte[] { 1, 2, 3, 4, 6, 7, 8, 9, 0 };

        var total = 0L;
        Parallel.ForEach(steps
            , new ParallelOptions() { MaxDegreeOfParallelism = Math.Min(steps.Length, Environment.ProcessorCount) }
            , startLocation =>
        {
            var amount = GenerateDistinctNumbersForLocation(startLocation, n);
            Interlocked.Add(ref total, amount);
        });

        //foreach (var startLocation in _steps.Keys)
        //    total += GenerateDistinctNumbersForLocation(startLocation, n);

        return total;
    }

    private static long GenerateDistinctNumbersForLocation(byte startLocation, int n)
    {
        var leafs = 1L;
        var layer = new Queue<byte>();
        layer.Enqueue(startLocation);
        
        for(int level = 1; level < n; level++)
        {
            var currentLeafs = 0L;
            for (int i = 0; i < leafs; i++)
            {
                foreach (var nextLocation in GetNextSteps(layer.Dequeue()))
                {
                    layer.Enqueue(nextLocation);
                    currentLeafs++;
                }
            }
            leafs = currentLeafs;
        }
        
        return leafs;
    }

    private static readonly ImmutableArray<byte> _steps1 = [6, 8];
    private static readonly ImmutableArray<byte> _steps2 = [7, 9];
    private static readonly ImmutableArray<byte> _steps3 = [4, 8];
    private static readonly ImmutableArray<byte> _steps4 = [3, 9, 0];
    //private static readonly ImmutableArray<byte> _steps5 = [];
    private static readonly ImmutableArray<byte> _steps6 = [1, 7, 0];
    private static readonly ImmutableArray<byte> _steps7 = [2, 6];
    private static readonly ImmutableArray<byte> _steps8 = [1, 3];
    private static readonly ImmutableArray<byte> _steps9 = [2, 4];
    private static readonly ImmutableArray<byte> _steps0 = [4, 6];

    private static ImmutableArray<byte> GetNextSteps(byte digit) => digit switch
    {
        1 => _steps1,
        2 => _steps2,
        3 => _steps3,
        4 => _steps4,
        6 => _steps6,
        7 => _steps7,
        8 => _steps8,
        9 => _steps9,
        0 => _steps0,
        _ => []
    };
}