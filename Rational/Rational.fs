namespace Informedica.Utils.Lib


type Rational<'T> = { Numerator: 'T; Denominator : 'T }


module Rational =


    open System
    open MathNet.Numerics


    let inline gcd zero a b =
        let rec loop a b = 
            if b = zero then a else loop b (a % b)
        loop a b


    let inline lcm zero a b = (a * b) / (gcd zero a b)


    let inline normalize zero one (num, denom) =
        if denom = zero then raise <| DivideByZeroException ()
        else
            if denom = one then num, denom
            else
                let k = gcd zero num denom
                num / k, denom / k


    let inline mult zero one (num1, den1) (num2, den2) =
        (num1 * num2, den1 * den2)
        |> normalize zero one


    let inline div zero one (num1, den1) (num2, den2) =
        (num1 * den2, den1 * num2)
        |> normalize zero one


    let inline add zero one (num1, den1) (num2, den2) =
        let lcm = lcm zero den1 den2
        (num1 * (lcm / den1) + num2 * (lcm / den2), lcm)
        |> normalize zero one


    let inline sub zero one (num1, den1) (num2, den2) =
        let lcm = lcm zero den1 den2
        (num1 * (lcm / den1) - num2 * (lcm / den2), lcm)
        |> normalize zero one



    let inline create zero one num denom =
        let num, denom = normalize zero one (num, denom)
        { Numerator = num; Denominator = denom }


module Int64Rational =

    let inline create num denom =
        Rational.create 0L 1L num denom



    module Operators =

        open Rational

        let (*) (r1: Rational<int64>) (r2: Rational<int64>) =
            mult 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
            |> fun (num, denom) -> create num denom

        let (/) (r1: Rational<int64>) (r2: Rational<int64>) =
            div 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
            |> fun (num, denom) -> create num denom

        let (+) (r1: Rational<int64>) (r2: Rational<int64>) =
            add 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
            |> fun (num, denom) -> create num denom

        let (-) (r1: Rational<int64>) (r2: Rational<int64>) =
            sub 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
            |> fun (num, denom) -> create num denom



type Rational<'T>  with


    static member (*) ((r1: Rational<_>), (r2: Rational<_>)) =
        Rational.mult 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
        |> fun (num, denom) -> Int64Rational.create num denom


    static member (/) ((r1: Rational<_>), (r2: Rational<_>)) =
        Rational.div 0L 1L (r1.Numerator, r1.Denominator) (r2.Numerator, r2.Denominator)
        |> fun (num, denom) -> Int64Rational.create num denom


