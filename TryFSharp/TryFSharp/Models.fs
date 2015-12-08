module Models

open System
open MathNet.Numerics.Distributions

// Generate price using geometric Brownian motion
let randomPrice drift volatility dt initial (dist:IContinuousDistribution) = 
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