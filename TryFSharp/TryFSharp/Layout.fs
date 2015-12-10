open System

let maxSquareEdge (a: float) (b: float) N  =
    let w, h =  if a > b then a, b else b, a
    let rec maxSquareEdgeRec r = 
        let c = Math.Ceiling(float N / float r) |> int
        match r <= c with
        | true -> 
            let cur = Math.Min( w / float c, h / float r)
            //printfn "%f %d %d" cur r c
            Math.Max (cur, maxSquareEdgeRec (r + 1)) 
        | false -> 0.0
    maxSquareEdgeRec 1

let maxSquareEdgeExt (a: float) (b: float) N  =
    let w, h =  if a > b then a, b else b, a
    let rec maxSquareEdgeRec r = 
        let c = Math.Ceiling(float N / float r) |> int
        let cur = Math.Min( w / float c, h / float r)
        //printfn "%f %d %d" cur r c
        cur
    let r = Math.Sqrt(h / w * float N) |> int
    Math.Max(maxSquareEdgeRec r, maxSquareEdgeRec (r + 1))

let parameters =
  seq { for a in 1..100 do
            for b in 1..100 do
                for N in 1..1000 do
                    yield (a, b, N) }

parameters 
    |> Seq.map (fun (a, b, N) -> a, b, N, maxSquareEdgeExt (float a) (float b) N, maxSquareEdge (float a) (float b) N)
    |> Seq.filter (fun (a, b, N, res1, res2) -> Math.Abs(res1 - res2) > 0.0)
    |> Seq.iter (fun (a, b, N, res1, res2) -> printfn "%d %d %d: %f %f" a b N res1 res2 ) 





