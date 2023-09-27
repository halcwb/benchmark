open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open MathNet.Numerics

open Informedica.Utils.Lib

type Benchmarks () =


    let nums =
        let rand = System.Random ()
        Array.init 100 (fun _ -> rand.Next (1, 1000))

    let dens =
        let rand = System.Random ()
        Array.init 100 (fun _ -> rand.Next (1, 1000))

    let brs =
        let brs =
            Array.zip nums dens
            |> Array.map (fun (n, d) -> 
                let n = bigint n
                let d = bigint d
                (BigRational.FromBigInt n) / (BigRational.FromBigInt d)
            )
        brs |> Array.allPairs brs

    let irs =
        let irs =
            Array.zip nums dens
            |> Array.map (fun (n, d) -> Int64Rational.create n d)
        irs |> Array.allPairs irs


    [<Benchmark>]
    member this.BigRationalMultiplication () =
        brs
        |> Array.map (fun (br1, br2) -> br1 * br2)


    [<Benchmark>]
    member this.Int64RationalMultiplication () =
        irs
        |> Array.map (fun (br1, br2) -> br1 * br2)

    [<Benchmark>]
    member this.BigRationalDivision () =
        brs
        |> Array.map (fun (br1, br2) -> br1 / br2)


    [<Benchmark>]
    member this.Int64RationalDivision () =
        irs
        |> Array.map (fun (br1, br2) -> br1 / br2)


// For more information see https://aka.ms/fsharp-console-apps
[<EntryPoint>]
let main (args: string[]) =

    let _ = BenchmarkRunner.Run<Benchmarks>()

    1