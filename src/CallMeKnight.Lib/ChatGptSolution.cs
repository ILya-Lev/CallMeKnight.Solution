namespace CallMeKnight.Lib;

public class ChatGptSolution
{
    public static int CountDistinctNumbers(int N)
    {
        if (N == 0) return 0;
        if (N == 1) return 10;

        // Knight's moves map
        int[][] moves = new int[][]
        {
            new int[] { 4, 6 }, // 0
            new int[] { 6, 8 }, // 1
            new int[] { 7, 9 }, // 2
            new int[] { 4, 8 }, // 3
            new int[] { 3, 9, 0 }, // 4
            new int[] { }, // 5 (No moves)
            new int[] { 1, 7, 0 }, // 6
            new int[] { 2, 6 }, // 7
            new int[] { 1, 3 }, // 8
            new int[] { 2, 4 } // 9
        };

        // DP table to store number of ways to reach each number with N hops
        int[,] dp = new int[N + 1, 10];
        // Initialize base case: 1 hop from each digit
        for (int i = 0; i < 10; i++)
            dp[1, i] = 1;

        // Fill DP table for N hops
        for (int hop = 2; hop <= N; hop++)
        {
            for (int num = 0; num <= 9; num++)
            {
                dp[hop, num] = 0;
                foreach (int next in moves[num])
                {
                    dp[hop, num] += dp[hop - 1, next];
                }
            }
        }

        // Sum all possible end digits for exactly N hops
        int totalNumbers = 0;
        for (int i = 0; i <= 9; i++)
            totalNumbers += dp[N, i];

        return totalNumbers;
    }
}

public class KnightDialerMatrixBased
{
    static readonly int MOD = 1_000_000_007;

    // Matrix exponentiation method
    public static int[,] MatrixExponentiation(int[,] matrix, int power)
    {
        int size = matrix.GetLength(0);
        int[,] result = IdentityMatrix(size);
        int[,] baseMatrix = matrix;

        while (power > 0)
        {
            if ((power & 1) == 1)
                result = MultiplyMatrices(result, baseMatrix);

            baseMatrix = MultiplyMatrices(baseMatrix, baseMatrix);
            power >>= 1;
        }

        return result;
    }

    // Returns an identity matrix of given size
    public static int[,] IdentityMatrix(int size)
    {
        int[,] identity = new int[size, size];
        for (int i = 0; i < size; i++)
            identity[i, i] = 1;
        return identity;
    }

    // Multiplies two matrices with modulo
    public static int[,] MultiplyMatrices(int[,] a, int[,] b)
    {
        int n = a.GetLength(0);
        int[,] result = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                long sum = 0;
                for (int k = 0; k < n; k++)
                {
                    sum += (long)a[i, k] * b[k, j] % MOD;
                }

                result[i, j] = (int)(sum % MOD);
            }
        }

        return result;
    }
    public static int KnightDialer(int N)
    {
        if (N == 0) return 0;
        if (N == 1) return 10;

        // Transition matrix representing knight moves
        int[,] T = new int[10, 10]
        {
                { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 }, // 0 -> 4, 6
                { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 }, // 1 -> 6, 8
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 }, // 2 -> 7, 9
                { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 }, // 3 -> 4, 8
                { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 }, // 4 -> 0, 3, 9
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 5 -> no moves
                { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0 }, // 6 -> 0, 1, 7
                { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 }, // 7 -> 2, 6
                { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // 8 -> 1, 3
                { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0 } // 9 -> 2, 4
        };

        // Raise T to the (N - 1)th power
        int[,] T_N_minus_1 = MatrixExponentiation(T, N - 1);

        // Sum all entries in the resulting matrix's column vector (1, 1, ..., 1)
        int totalDistinctNumbers = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                totalDistinctNumbers = (totalDistinctNumbers + T_N_minus_1[i, j]) % MOD;
            }
        }

        return totalDistinctNumbers;
    }

}
