open System

let maxSquareEdge (a: double) (b: double) N  =
    let w, h =  if a > b then a, b else b, a
    let rec maxSquareEdgeRec r c = 
        match r <= c with
        | true -> 
            let cur = Math.Min( w / double c, h / double r)
            printfn "%f %d %d" cur r c
            Math.Max (cur, maxSquareEdgeRec (r + 1) (Math.Ceiling(double N / double (r + 1)) |> int))
        | false -> 0.0
    maxSquareEdgeRec 1 N

printfn "%f" (maxSquareEdge 8.0 4.0 15)