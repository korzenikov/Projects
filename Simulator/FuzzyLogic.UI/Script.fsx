#load "..\packages\FSharp.Charting.0.90.14\FSharp.Charting.fsx"
#r "bin\Debug\FuzzyLogic.dll"

open FuzzyLogic.FuzzySets
open FSharp.Charting

let getChart x =
    x |> getPoints |> Chart.Line

// Distance
let near = RightNumber (0.0, 50.0)
let medium = TriangleNumber(0.0, 50.0, 100.0)
let far = LeftNumber(50.0, 100.0)

// Speed
let low = RightNumber(20.0, 40.0)
let average = TriangleNumber(20.0, 40.0, 60.0)
let high = LeftNumber (40.0, 60.0)

let speed = [low; average; high]

let distance = [near; medium; far]

// Throttling
let largeNegative = RightNumber(-20.0, -10.0)
let smallNegative = TriangleNumber(-20.0, -10.0, 0.0)
let zero = TriangleNumber(-10.0, 0.0, 10.0)
let smallPositive = TriangleNumber(0.0, 10.0, 20.0)
let largePositive = LeftNumber(10.0, 20.0)

let throttling = [largeNegative; smallNegative; zero; smallPositive; largePositive] 

speed |> Seq.map getChart |> Chart.Combine
distance |> Seq.map getChart |> Chart.Combine
throttling |> Seq.map getChart |> Chart.Combine

// Rules
let rules = 
    [
        (far, low, largePositive)
        (far, average, smallPositive)
        (far, high, zero)
        (medium, low, largePositive)
        (medium, average, zero) 
        (medium, high, smallNegative)
        (near, low, zero)
        (near, average, smallNegative)
        (near, high, largeNegative)
    ]

let handleRule v1 v2 (input1, input2, output)  =
    (output, [(input1, v1); (input2, v2)])

let visualize v1 v2 rules =
    rules 
    |> getTriggeredOutputs (handleRule v1 v2)
    |> Seq.collect (fun (fs, y) -> [fs |> getPoints |> Chart.Line; slice y fs |> Chart.Area]) 
    |> Chart.Combine

rules |> visualize 80.0 80.0

let showResults v1 v2 rules =
    let outputs = rules |> fireRules (handleRule v1 v2)
    let figures = 
        outputs
        |> Seq.filter (fun (fs, y) -> y > 0.0)
        |> Seq.groupBy fst 
        |> Seq.map (fun (key, s) -> s |> Seq.maxBy (fun (_, y) -> y))
        |> Seq.distinct 
        |> Seq.sortBy (fun (fs, y) -> getMax fs)
    let cs = figures |> List.ofSeq |> unionResults |> Seq.pairwise |> Seq.map (fun (p1, p2) -> centroid p1 p2) 
    let outputFigures = outputs |> Seq.map (fun (fs, y) -> fs |> getPoints |> Chart.Line) |> List.ofSeq 
    let slicedFigures = figures |> Seq.map (fun (fs, y) -> slice y fs |> Chart.Area) |> List.ofSeq
    let centroid = Chart.Point ([ cs |> centroidOfCompositeShape |> fst ], Color = System.Drawing.Color.Red)
    List.concat
        [
            outputFigures
            slicedFigures
            [ centroid ]
        ] 
    |> Chart.Combine

rules |> showResults 100.0 10.0

rules |> getOutputValue (handleRule 10.0 80.0)
//
//[
//    largeNegative,0.9
//    smallNegative, 0.6
//    zero, 0.4
//    smallPositive, 0.5
//    largePositive, 1.0
//]
//|> unionResults 
//|> Chart.Area