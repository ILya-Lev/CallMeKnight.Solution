using CallMeKnight.Lib;
using FluentAssertions;
using Xunit.Abstractions;

namespace CallMeKnight.Tests;

public class CallMeKnightSolverTests(ITestOutputHelper output)
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    [InlineData(3, 46)]
    [InlineData(4, 104)]
    [InlineData(5, 240)]
    [InlineData(6, 544)]
    [InlineData(7, 1256)]
    [InlineData(8, 2848)]
    [InlineData(9, 6576)]//growth speed is around 2.3x
    [InlineData(10, 14912)]
    public void CalculateDistinctNumbers_SmallLength_MatchExpectations(int n, int expectedAmount)
    {
        ChatGptSolution.CountDistinctNumbers(n).Should().Be(expectedAmount);
        KnightDialerMatrixBased.KnightDialer(n).Should().Be(expectedAmount);
        CallMeKnightSolverFrequencies.CalculateDistinctNumbers(n).Should().Be((ulong)expectedAmount);
        CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(n).Should().Be(expectedAmount);
    }

    [Theory]
    [InlineData(13, 180_288)]//0.86s naive; 0.4s leafs; 0.5s leafs-light; 0.09s predefined; 0.054s ||
    [InlineData(15, 944_000)]//2.4s naive; 1.6s leafs; 1.2s leafs-light; 0.3s predefined; 0.112s ||
    [InlineData(17, 4_942_848)]//11.8s naive; 7.5s leafs; 4.5s leafs-light; 0.78s predefined; 0.44s ||
    [InlineData(19, 25_881_088)]//54s naive; 40s leafs; 22.7s leafs-light; 2.9s predefined; 1.06s ||
    [InlineData(21, 135_515_136)]//4m 45s naive; 1m 56s leafs-light; 13.88s predefined, 3.9s ||; 3s just numbers, 1s || 
    public void CalculateDistinctNumbers_MidLength_MatchExpectations(int n, int expectedAmount)
    {
        CallMeKnightSolverFrequencies.CalculateDistinctNumbers(n).Should().Be((ulong)expectedAmount);
        CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(n).Should().Be(expectedAmount);
    }

    [Fact]
    public void CalculateDistinctNumbers_LargerLength_MatchExpectations()
    {
        //23 -> 709_566_464; 21s Location is record struct; 62s Location is record class; 15s just numbers, 4s ||
        //25 -> 3_715_338_240L; 5m 5s predefined Locations in ||; 19s just numbers in ||; 13s last layer count, not store; 6s last 2 layers count, not store; 4.5s step plus 2
        //27 -> 19_453_763_584L; 22s step plus 2 in debug; 17s in release; 8.5 GB RAM; 0.036s frequencies
        //27 -> 19_453_763_584L; 22s step plus 2 in debug; 17s in release; 8.5 GB RAM; 0.036s frequencies
        
        //long max 9_223_372_036_854_775_807 ~ 9E+18; 2.3^N = 9e+18 => N = 52.43
        //53 -> 6_365_088_326_348_701_696L and 0.036s frequencies
        //54 => Arithmetic operation resulted in an overflow. Long (Int64) is not enough

        CallMeKnightSolverFrequencies.CalculateDistinctNumbers(51).Should().Be(8261652954021363712UL);
        CallMeKnightSolverFrequenciesBigInteger.CalculateDistinctNumbers(51).Should().Be(8261652954021363712UL);
        CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(51).Should().Be(8261652954021363712UL);
    }

    [Fact]
    public void CalculateDistinctNumbers_VeryLong_Observe()
    {
        //var result = CallMeKnightSolverFrequenciesBigInteger.CalculateDistinctNumbers(100);   //0.5s
        var result = CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(10_000);  //0.032s
        output.WriteLine(result.ToString());
    }

    [Fact]
    public void CalculateDistinctNumbers_TimeFrame10Seconds_Observe()
    {
        //var result = CallMeKnightSolverFrequenciesBigInteger.CalculateDistinctNumbers(200_000);
        //output.WriteLine(result.ToString());//86s -> 20s ! by reusing arrays instead of dictionaries
        
        //200k 0.192s
        //4M 16.5s, 3M 10.155s, 2M 5s, 1M 2.5s
        var result = CallMeKnightSolverAdjacencyMatrix.CalculateDistinctNumbers(1_000_000);
        
        output.WriteLine($"result contains {result.GetByteCount()} bytes");
        output.WriteLine($"result contains {result.GetBitLength()} bits");
        var start = result.ToByteArray().SkipWhile(d => d == 0).Take(5);
        var end = result.ToByteArray().TakeLast(5);
        output.WriteLine($"result= {string.Join("", start.Select(d => d.ToString()))}...{string.Join("", end.Select(d => d.ToString()))}");
    }
}