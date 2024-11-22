using System.Collections.Frozen;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverLeafs
{
    private static readonly FrozenDictionary<Location, byte> _buttons = new Dictionary<Location, byte>()
    {
        [new(0, 0)] = 1,
        [new(0, 1)] = 2,
        [new(0, 2)] = 3,
        [new(1, 0)] = 4,
        [new(1, 1)] = 5,
        [new(1, 2)] = 6,
        [new(2, 0)] = 7,
        [new(2, 1)] = 8,
        [new(2, 2)] = 9,
        [new(3, 1)] = 0,
    }
        .ToFrozenDictionary();

    private static readonly Func<Location, Location>[] _stepGenerators =
    [
        a => new(++a.Row, (sbyte)(a.Col + 2)),
        a => new((sbyte)(a.Row + 2), ++a.Col),
        a => new((sbyte)(a.Row + 2), --a.Col),
        a => new(++a.Row, (sbyte)(a.Col - 2)),
        a => new(--a.Row, (sbyte)(a.Col - 2)),
        a => new((sbyte)(a.Row - 2), --a.Col),
        a => new((sbyte)(a.Row - 2), ++a.Col),
        a => new(--a.Row, (sbyte)(a.Col + 2)),
    ];

    public static int CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        //could be done in parallel - 10 threads at most...
        var total = 0;
        foreach (var startLocation in _buttons.Keys)
        {
            total += GenerateDistinctNumbersForLocation(startLocation, n);
        }
        return total;
    }

    private static int GenerateDistinctNumbersForLocation(Location startLocation, int n)
    {
        var leafs = 0;
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

    private static Location[] GetPossibleSteps(Location location) => _stepGenerators
        .Select(g => g(location))
        .Where(IsInRange)
        .ToArray();

    private static bool IsInRange(Location location) => _buttons.ContainsKey(location);

    private record struct Location(sbyte Row, sbyte Col);

    private record struct Node(int Depth, Location Location);
}