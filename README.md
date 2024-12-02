An attempt to solve CallMeKnight challenge from dotnet chapter Luxoft Nov 2024.

Challenge author [Andrey Mishkinis](https://github.com/andreymi-nx).

The best solution is in class [CallMeKnightSolverFrequenciesBigInteger](https://github.com/ILya-Lev/CallMeKnight.Solution/blob/master/src/CallMeKnight.Lib/CallMeKnightSolverFrequenciesBigInteger.cs).
It is based on O(N) algorithm.

Optimized straightforward solution is in class [CallMeKnightSolverSequences2Steps](https://github.com/ILya-Lev/CallMeKnight.Solution/blob/master/src/CallMeKnight.Lib/CallMeKnightSolverSequences2Steps.cs).
It is based on O((20/9)^N) algorithm.

After solution presentation by Andrey, added here [Adjacency Matrix](https://github.com/ILya-Lev/CallMeKnight.Solution/blob/master/src/CallMeKnight.Lib/CallMeKnightSolverAdjacencyMatrix.cs) based solution.
Which is sum(M^n, over all cells) = number of unique paths (i.e. numbers). M^n could be calculated recursively as
M^n = M * M^(n-1) when n is odd, or M^(n/2) * M^(n/2) when n is even. Memoization speeds up the process.
