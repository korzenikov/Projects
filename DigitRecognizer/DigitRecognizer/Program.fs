open System
open System.IO

type Example = { Label:int; Pixels:int[] }

let split (s:string) = s.Split(',')

let dropHeader (x:_[]) = x.[1..]

let convert (s:string) = Convert.ToInt32(s)

let example (row:int[]) = { Label = row.[0]; Pixels = row.[1..]}

let read path = 
    File.ReadAllLines(path) 
    |> dropHeader 
    |> Array.map split 
    |> Array.map (fun line -> line |> Array.map convert)
    |> Array.map example
 
  
let trainingSamples = read "trainingsample.csv"

let distance (p1: int[]) (p2: int[]) = (p1, p2) ||> Array.map2 (fun p1 p2 -> (p1 - p2)*(p1 - p2)) |> Array.sum
 
let K = 10

let closestSamples (unknown:int[]) = 
  trainingSamples 
    |> Seq.sortBy (fun x -> distance x.Pixels unknown) 
    |> Seq.take K 

let classify (unknown:int[]) =
    closestSamples unknown
    |> Seq.groupBy (fun x -> x.Label) 
    |> Seq.maxBy (fun (key, values) -> Seq.length values) 
    |> (fun (key, values) -> key)

let validationSamples = read "validationsample.csv"

let unrecognizedSamples = validationSamples |> Array.map (fun x -> (x, (classify x.Pixels))) |> Array.filter (fun x -> ((fst x)).Label <> (snd x)) |> Array.map (fun x -> (fst x))

let options = unrecognizedSamples |> Array.map (fun x -> (x.Label, (closestSamples x.Pixels) |> Array.ofSeq |> Array.map (fun s -> s.Label)))

printfn "%A" options


//printfn "%A" (unrecognizedSamples |> Seq.length)


//let percentage = validationSamples |> Array.averageBy (fun x -> if (classify x.Pixels) = x.Label then 1.0 else 0.0)

//printfn "%A" (percentage * 100.0)
 
