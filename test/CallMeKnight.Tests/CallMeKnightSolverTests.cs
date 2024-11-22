using CallMeKnight.Lib;
using FluentAssertions;

namespace CallMeKnight.Tests;

public class CallMeKnightSolverTests
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
        CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(n).Should().Be(expectedAmount);
    }

    [Theory]
    [InlineData(13, 180_288)]//0.86s naive; 0.4s leafs; 0.5s leafs-light; 0.09s predefined; 0.054s ||
    [InlineData(15, 944_000)]//2.4s naive; 1.6s leafs; 1.2s leafs-light; 0.3s predefined; 0.112s ||
    [InlineData(17, 4_942_848)]//11.8s naive; 7.5s leafs; 4.5s leafs-light; 0.78s predefined; 0.44s ||
    [InlineData(19, 25_881_088)]//54s naive; 40s leafs; 22.7s leafs-light; 2.9s predefined; 1.06s ||
    [InlineData(21, 135_515_136)]//4m 45s naive; 1m 56s leafs-light; 13.88s predefined, 3.9s ||; 3s just numbers, 1s || 
    public void CalculateDistinctNumbers_MidLength_MatchExpectations(int n, int expectedAmount)
    {
        CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(n).Should().Be(expectedAmount);
    }

    [Fact]
    public void CalculateDistinctNumbers_LargerLength_MatchExpectations()
    {
        //23 -> 709_566_464; 21s Location is record struct; 62s Location is record class; 15s just numbers, 4s ||
        //25 -> 3_715_338_240L; 5m 5s predefined Locations in ||; 19s just numbers in ||; 13s last layer count, not store; 6s last 2 layers count, not store; 4.5s step plus 2
        //27 -> 19_453_763_584L; 22s step plus 2 in debug; 17s in release; 8.5 GB RAM

        CallMeKnightSolverSequences2Steps.CalculateDistinctNumbers(27).Should().Be(19_453_763_584L);
    }
}