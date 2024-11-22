// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using CallMeKnightSolver.Benchmarks;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<CallMeKnightBenchmarks>();