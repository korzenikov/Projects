open NettingLibrary
open Netter

type Trade = { 
    Account: string
    Strategy: string 
    Quantity: int 
}

type TradeType = 
    | Outright
    | Netting

type FinalTrade = { 
    Account: string
    Strategy: string 
    Type: TradeType
    Quantity: int 
}



// Tests
//let trades = [|
// { Account = "DELICTS"; Strategy = "US Equtiy Market Neutral"; Quantity = -2000 } ;
// { Account = "MSAF"; Strategy = "US Equtiy Market Neutral"; Quantity = -4700 } ; 
// { Account = "MSAF"; Strategy = "US Long/Short Equity"; Quantity = -4700 } ; 
// { Account = "GMSF"; Strategy = "US Long/Short Equity"; Quantity = 200 } ; 
// { Account = "DELICTS"; Strategy = "US Long/Short Equity"; Quantity = -2900 } |]

//let trades = [|
// { Account = "DELICTS"; Strategy = "US Equtiy Market Neutral"; Quantity = 2000 } ;
// { Account = "MSAF"; Strategy = "US Equtiy Market Neutral"; Quantity = 4700 } ; 
// { Account = "MSAF"; Strategy = "US Long/Short Equity"; Quantity = 4700 } ; 
// { Account = "GMSF"; Strategy = "US Long/Short Equity"; Quantity = -200 } ; 
// { Account = "DELICTS"; Strategy = "US Long/Short Equity"; Quantity = 2900 } |]

let trades =  [
 { Account = "Account 1"; Strategy = "Strategy l"; Trade.Quantity = 700 } 
 { Account = "Account 1"; Strategy = "Strategy 2"; Quantity = 200 } 
 { Account = "Account 1"; Strategy = "Strategy 3"; Quantity = 400 } 
 { Account = "Account 2"; Strategy = "Strategy l"; Quantity = -100 } 
 { Account = "Account 2"; Strategy = "Strategy 2"; Quantity = -200 } 
 { Account = "Account 2"; Strategy = "Strategy 3"; Quantity = -200 } ]


let splitTrade (trade: Trade, quantity) =
    let outright = trade.Quantity - quantity
    let nettingTrade =  { Account = trade.Account; Strategy = trade.Strategy; Type = Netting;  Quantity = quantity}
    let outrightTrade = { Account = trade.Account; Strategy = trade.Strategy; Type = Outright; Quantity = outright}
    [nettingTrade; outrightTrade] |> Seq.filter (fun x -> x.Quantity<>0)
  
let finalTrades = trades |> net (fun trade -> trade.Quantity) |> Seq.collect splitTrade

let totalNetting = finalTrades |> Seq.filter (fun x -> x.Type = Netting) |> Seq.sumBy (fun x -> x.Quantity)

let totalOutright = finalTrades |> Seq.filter (fun x -> x.Type = Outright) |> Seq.sumBy (fun x -> x.Quantity)

printfn "%A" totalNetting

printfn "%A" totalOutright

finalTrades |> Seq.iter (fun x -> printfn "%A " x)