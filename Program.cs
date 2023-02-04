// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using flat_vs_fk;

var FkSummary = BenchmarkRunner.Run<AppManager>();
Console.WriteLine("Hello, World!");
