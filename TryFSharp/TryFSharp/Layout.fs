open System

let maxSquareEdge (a: double) (b: double) N  =
    let w, h =  if a > b then a, b else b, a
    let rec maxSquareEdgeRec r = 
        let c = Math.Ceiling(double N / double r) |> int
        match r <= c with
        | true -> 
            let cur = Math.Min( w / double c, h / double r)
            printfn "%f %d %d" cur r c
            Math.Max (cur, maxSquareEdgeRec (r + 1)) 
        | false -> 0.0
    maxSquareEdgeRec 1

let maxSquareEdgeExt (a: double) (b: double) N  =
    let r = b / a * double N |> Math.Sqrt|> Math.Round |> int
    let c = double N / double r |> Math.Ceiling |> int
    Math.Min( a / double c, b / double r)
    

printfn "%f" (maxSquareEdge 3.0 2.0 7)
printfn "%A" (maxSquareEdgeExt 3.0 2.0 7)

printfn "%f" (maxSquareEdge 8.0 4.0 15)
printfn "%A" (maxSquareEdgeExt 8.0 4.0 15)


printfn "%f" (maxSquareEdge 3.0 4.0 11)
printfn "%A" (maxSquareEdgeExt 3.0 4.0 11)
printfn "%A" (maxSquareEdgeExt 4.0 3.0 11)


printfn "%f" (maxSquareEdge 2.0 6.0 11)
printfn "%A" (maxSquareEdgeExt 2.0 6.0 11)
printfn "%A" (maxSquareEdgeExt 6.0 2.0 11)