module Chart

open System
open FSharp.Charting

let SimpleLine(seq, count) =
    seq |> Seq.truncate count
        |> Seq.mapi (fun i v -> i, v)
        |> Chart.Line
