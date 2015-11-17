module NettingLibrary.Netter

open System

let roundDown (value: decimal) = 
    let roundedValue = if value > 0m then Math.Floor(value) else Math.Ceiling(value)
    roundedValue |> int

let haveTheSameSign (x: int, y: int) = Math.Sign(x) * Math.Sign(y) > 0

let maxByIndex project source =  
    source
    |> Seq.mapi (fun i x -> i, x)
    |> Seq.maxBy (fun x -> project (snd x)) 
    |> fst

let proRata value weights = 
    let totalWeight =  Seq.sum weights
    let proRatedValues = weights |> Seq.map (fun weight -> (decimal value) * (decimal weight) / (decimal totalWeight) |> roundDown) 
    let sumOfProratedValues = proRatedValues |> Seq.sum
    let diff = value - sumOfProratedValues
    let maxItemIndex = proRatedValues |> maxByIndex (fun x -> Math.Abs(x)) 
    proRatedValues |> Seq.mapi (fun i x -> if i = maxItemIndex then x + diff else x)

let net quantitySelector trades =
    let tradesWithQuantities = trades |> Seq.map (fun trade -> trade, quantitySelector trade)
    let totalBuys, totalSells = tradesWithQuantities |> Seq.map snd |> Seq.fold (fun (b, s) quantity -> if quantity > 0 then (b + quantity, s) else (b, s + quantity)) (0, 0)
    let nettingValue = if totalBuys < -totalSells then totalBuys else totalSells
    let smallerSideTrades, largerSideTrades = tradesWithQuantities |> List.ofSeq |> List.partition (fun (trade, quantity) -> haveTheSameSign (quantity, nettingValue))
    let proratedQuantities = largerSideTrades |> Seq.map snd |> proRata -nettingValue
    let outrightTrades =  Seq.zip (largerSideTrades |> Seq.map fst) proratedQuantities
    outrightTrades |> Seq.append smallerSideTrades  
        

