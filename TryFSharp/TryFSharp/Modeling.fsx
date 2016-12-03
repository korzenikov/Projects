#load "..\packages\FSharp.Charting.0.90.14\FSharp.Charting.fsx"
#load "..\packages\MathNet.Numerics.FSharp.3.13.1\MathNet.Numerics.fsx"

#load "Chart.fs"

open System
open FSharp.Charting
open MathNet.Numerics.Distributions


// Generate random walk from 'value' recursively
let rec randomWalk value (dist:IContinuousDistribution) = seq { 
    yield value 
    yield! randomWalk (value + dist.Sample()) dist }

let randomPrice drift volatility dt initial (dist:Normal) = 
    // Calculate parameters of the exponential
    let driftExp = (drift - 0.5 * pown volatility 2) * dt
    let randExp = volatility * (sqrt dt)

    // Recursive loop that actually generates the price
    let rec loop price = seq {
        yield price
        let price = price * exp (driftExp + randExp * dist.Sample()) 
        yield! loop price }

    // Return path starting at 'initial'
    loop initial


let norm1 = Normal(0.0, 1.0, RandomSource = Random(100))
let norm2 = Normal(0.0, 2.0, RandomSource = Random(100))

Chart.Combine [
                Chart.SimpleLine(randomWalk 10.0 norm1, 500) |> Chart.WithStyling(Name = "norm1") |> Chart.WithLegend()
                Chart.SimpleLine(randomWalk 10.0 norm2, 500) |> Chart.WithStyling(Name = "norm2") |> Chart.WithLegend()
               ]

// Generate price using geometric Brownian motion

// Create normal distribution and test the model
let dist = Normal(0.0, 1.0, RandomSource = Random(100))
Chart.SimpleLine(randomPrice 0.05 0.05 0.005 5.0 dist, 500)


// Two probability distributions with the same random seed
let dist1 = Normal(0.0, 1.0, RandomSource = Random(100))
let dist2 = Normal(0.0, 1.0, RandomSource = Random(100))

// Vary the parameters between 0.01 to 0.10
let drift1, vol1 = 0.025, 0.05
let drift2, vol2 = 0.05, 0.10 

// Compare randomly generated prices 
Chart.Combine
    [ 
        Chart.SimpleLine(randomPrice drift1 vol1 0.005 5.0 dist1, 500) |> Chart.WithStyling(Name = "drift1") |> Chart.WithLegend()
        Chart.SimpleLine(randomPrice drift2 vol2 0.005 5.0 dist2, 500) |> Chart.WithStyling(Name = "drift2") |> Chart.WithLegend()
    ]
