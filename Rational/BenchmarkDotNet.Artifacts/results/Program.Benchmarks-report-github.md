``` ini

BenchmarkDotNet=v0.13.2, OS=macOS Monterey 12.6.9 (21G726) [Darwin 21.6.0]
Intel Core i7-6700K CPU 4.00GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


```
|                      Method |     Mean |     Error |    StdDev |
|---------------------------- |---------:|----------:|----------:|
|   BigRationalMultiplication | 1.728 ms | 0.0054 ms | 0.0048 ms |
| Int64RationalMultiplication | 2.938 ms | 0.0165 ms | 0.0138 ms |
|         BigRationalDivision | 1.744 ms | 0.0054 ms | 0.0045 ms |
|       Int64RationalDivision | 2.893 ms | 0.0097 ms | 0.0086 ms |
