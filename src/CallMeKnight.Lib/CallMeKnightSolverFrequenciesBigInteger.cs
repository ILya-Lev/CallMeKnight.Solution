using System.Collections.Frozen;
using System.Numerics;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverFrequenciesBigInteger
{
    public static BigInteger CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        if (n == 1) return 10;//for button with digit 5 - it is the only case to take it into account

        BigInteger total = 0L;

        foreach (var startLocation in _steps.Keys)
            total += GenerateDistinctNumbersForLocation(startLocation, n);

        return total;
    }

    private static BigInteger GenerateDistinctNumbersForLocation(byte startLocation, int n)
    {
        var layer = new Dictionary<byte, BigInteger> { [startLocation] = 1 };

        for (int level = 1; level < n; level++)
        {
            var nextLayer = new Dictionary<byte, BigInteger>();
            foreach (var (digit, amount) in layer)
            {
                foreach (var nextDigit in _steps[digit])
                {
                    nextLayer.TryAdd(nextDigit, 0);
                    nextLayer[nextDigit] += amount;
                }
            }
            layer = nextLayer;
        }

        BigInteger leafsNumber = 0;
        foreach (var (_, amount) in layer)
        {
            leafsNumber += amount;
        }
        return leafsNumber;
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
}