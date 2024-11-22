using System.Collections.Frozen;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverSequences
{
    public static long CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        if (n == 1) return 10;//for button with digit 5 - it is the only case to take it into account

        var total = 0L;
        Parallel.ForEach(_steps.Keys
            , new ParallelOptions() { MaxDegreeOfParallelism = Math.Min(_steps.Keys.Length, Environment.ProcessorCount) }
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

        for (int level = 1; level < n; level++)
        {
            var currentLeafs = 0L;
            for (int i = 0; i < leafs; i++)
            {
                foreach (var nextLocation in _steps[layer.Dequeue()])
                {
                    layer.Enqueue(nextLocation);
                    currentLeafs++;
                }
            }
            leafs = currentLeafs;
        }

        return leafs;
    }

    private static readonly FrozenDictionary<byte, byte[]> _steps = new Dictionary<byte, byte[]>()
    {
        [1] = [6, 8],
        [2] = [7, 9],
        [3] = [4, 8],
        [4] = [3, 9, 0],
        //[5] = [],
        [6] = [1, 7, 0],
        [7] = [2, 6],
        [8] = [1, 3],
        [9] = [2, 4],
        [0] = [4, 6],
    }
        .ToFrozenDictionary();

    private static readonly FrozenDictionary<byte, byte[]> _stepsPlus2 = new Dictionary<byte, byte[]>()
    {
        [1] = [1, 7, 0, 1, 3],
        [2] = [2, 6, 2, 4],
        [3] = [3, 9, 0, 1, 3],
        [4] = [4, 8, 2, 4, 4, 6],
        //[5] = [],
        [6] = [6, 8, 2, 6, 4, 6],
        [7] = [7, 9, 1, 7, 0],
        [8] = [6, 8, 4, 8],
        [9] = [7, 9, 3, 9, 0],
        [0] = [3, 9, 0, 1, 7, 0],
    }
        .ToFrozenDictionary();

}