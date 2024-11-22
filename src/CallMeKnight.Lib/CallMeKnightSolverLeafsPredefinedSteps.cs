using System.Collections.Frozen;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverLeafsPredefinedSteps
{
    private static readonly Location[] _locations =
    [
        new(0, 0), //1
        new(0, 1), //2
        new(0, 2), //3
        new(1, 0), //4
        new(1, 1), //5
        new(1, 2), //6
        new(2, 0), //7
        new(2, 1), //8
        new(2, 2), //9
        new(3, 1), //0
    ];

    private static readonly FrozenDictionary<Location, Location[]> _steps = new Dictionary<Location, Location[]>()
    {
        [_locations[0]] = [_locations[5], _locations[7]],
        [_locations[1]] = [_locations[6], _locations[8]],
        [_locations[2]] = [_locations[3], _locations[7]],
        [_locations[3]] = [_locations[2], _locations[8], _locations[9]],
        [_locations[4]] = [],
        [_locations[5]] = [_locations[0], _locations[6], _locations[9]],
        [_locations[6]] = [_locations[1], _locations[5]],
        [_locations[7]] = [_locations[0], _locations[2]],
        [_locations[8]] = [_locations[1], _locations[3]],
        [_locations[9]] = [_locations[3], _locations[5]],
    }
    .ToFrozenDictionary();

    public static long CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        if (n == 1) return 10;//for button with digit 5 - it is the only case to take it into account

        var total = 0L;
        Parallel.ForEach(_locations.Take(4).Concat(_locations.Skip(5))//ignore start location #5
            , new ParallelOptions(){MaxDegreeOfParallelism = Math.Min(9, Environment.ProcessorCount)}
            , startLocation =>
        {
            var amount = GenerateDistinctNumbersForLocation(startLocation, n);
            Interlocked.Add(ref total, amount);
        });

        //foreach (var startLocation in _locations)
        //    total += GenerateDistinctNumbersForLocation(startLocation, n);

        return total;
    }

    private static long GenerateDistinctNumbersForLocation(Location startLocation, int n)
    {
        var leafs = 0L;
        var layer = new Queue<Node>();
        layer.Enqueue(new Node(1, startLocation));

        while (layer.Any())
        {
            var current = layer.Dequeue();
            
            if (current.Depth == n)
            {
                leafs++;
                continue;
            }

            foreach (var nextLocation in GetPossibleSteps(current.Location))
            {
                layer.Enqueue(new Node(current.Depth + 1, nextLocation));
            }
        }
        
        return leafs;
    }

    private static Location[] GetPossibleSteps(Location location) => _steps[location];

    private record struct Location(sbyte Row, sbyte Col);

    private record struct Node(int Depth, Location Location);
}