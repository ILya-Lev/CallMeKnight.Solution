using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Numerics;

namespace CallMeKnight.Lib;

public class CallMeKnightSolverAdjacencyMatrix
{
    private const int Size = 10;

    public static BigInteger CalculateDistinctNumbers(int n)
    {
        if (n < 1) return 0;
        if (n == 1) return Size;//for button with digit 5 - it is the only case to take it into account

        var adjacencyMatrix = CreateAdjacencyMatrix();
        
        var leafLevelMatrix = adjacencyMatrix.Power(n-1);//as the matrix itself is as if the first step was done

        var total = GetTotal(leafLevelMatrix);
        return total;
    }

    private static M<BigInteger> CreateAdjacencyMatrix()
    {
        var m = new M<BigInteger>(Size, Size);
        
        for (int r = 0; r < Size; r++)
            for (int c = 0; c < Size; c++)
                m[r, c] = GetInitialMatrix(r, c);

        return m;
    }

    private static BigInteger GetInitialMatrix(int r, int c) => _steps[r].Contains(c) ? 1 : 0;

    private static BigInteger GetTotal(M<BigInteger> m)
    {
        BigInteger total = default;
        
        for (int r = 0; r < Size; r++)
            for (int c = 0; c < Size; c++)
                total += m[r, c];

        return total;
    }

    private static readonly FrozenDictionary<int, HashSet<int>> _steps = new Dictionary<int, HashSet<int>>()
        {
            [0] = [4, 6],
            [1] = [6, 8],
            [2] = [7, 9],
            [3] = [4, 8],
            [4] = [0, 3, 9],
            [5] = [],
            [6] = [0, 1, 7],
            [7] = [2, 6],
            [8] = [1, 3],
            [9] = [2, 4],
        }
        .ToFrozenDictionary();

    //I have to implement my own type as MathNet Matrix does not support T=BigInteger
    private record class M<T>(int Height, int Width) where T : INumber<T>
    {
        private readonly T[,] _matrix = new T[Height, Width];

        public T this[int r, int c]
        {
            get => _matrix[r, c];
            set => _matrix[r,c] = value;
        }

        private readonly ConcurrentDictionary<int, M<T>> _powers = new();

        public M<T> Power(int n)
        {
            if (n < 1) throw new ArgumentException($"powers n >= 1 are supported, provided '{n}'");
            if (n == 1) return this;

            return _powers.GetOrAdd(n, k => k % 2 == 0 
                ? Multiply(Power(k / 2), Power(k / 2)) 
                : Multiply(this, Power(k - 1)));
        }

        public static M<T> Multiply(M<T> lhs, M<T> rhs)
        {
            if (lhs.Width != rhs.Height)
                throw new ArgumentException($"Length of row in left matrix != length of column in the right one!");

            var result = new M<T>(lhs.Height, rhs.Width);

            Parallel.For(0, lhs.Height
                , new ParallelOptions() {MaxDegreeOfParallelism = Environment.ProcessorCount - 1}
                , r =>
            {
                for (int c = 0; c < rhs.Width; c++)
                {
                    var nextValue = T.Zero;
                    for (int i = 0; i < lhs.Width; i++)
                    {
                        nextValue += lhs[r, i] * rhs[i, c]; //O(n^3) for square matrix
                    }

                    result[r, c] = nextValue;
                }
            });

            return result;
        }
    }
}