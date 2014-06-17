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
 
  
let trainingExpamples = read "D:\\trainingsample.csv"

let distance (p1: int[]) (p2: int[]) = (p1, p2) ||> Array.map2 (fun p1 p2 -> (p1 - p2)*(p1 - p2)) |> Array.sum
 
let K = 1

let classify (unknown:int[]) =
    trainingExpamples 
    |> Seq.sortBy (fun x -> distance x.Pixels unknown) 
    |> Seq.take K 
    |> Seq.groupBy (fun x -> x.Label) 
    |> Seq.maxBy (fun (key, values) -> Seq.length values) 
    |> (fun (key, values) -> key)

let validationSamples = read "D:\\validationsample.csv"

let percentage = validationSamples |> Array.averageBy (fun x -> if (classify x.Pixels) = x.Label then 1.0 else 0.0)

printfn "%A" (percentage * 100.0)
 
// [ YOUR CODE GOES HERE! ]
//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code
