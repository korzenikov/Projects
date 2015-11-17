#load "Netter.fs"
#load "TradesNetter.fs"

open NettingLibrary
open Netter
open TradesNetter

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

let trades = [
 { Account = "Account 1"; Strategy = "Strategy l"; Quantity = 700; IsEligible = true } 
 { Account = "Account 1"; Strategy = "Strategy 2"; Quantity = 200; IsEligible = true  } 
 { Account = "Account 1"; Strategy = "Strategy 3"; Quantity = 400; IsEligible = true } 
 { Account = "Account 2"; Strategy = "Strategy l"; Quantity = -100; IsEligible = true } 
 { Account = "Account 2"; Strategy = "Strategy 2"; Quantity = -200; IsEligible = true } 
 { Account = "Account 2"; Strategy = "Strategy 3"; Quantity = -200; IsEligible = true }
 //{ Account = "Account 3"; Strategy = "Strategy l"; Quantity = 100 }
  ]

//let nonNettableAccounts = [ "Account 3"]

//let nettedTrade trade = 
//    { trade with NettingQuantity = trade.OutrightQuantity; OutrightQuantity = 0 }
//
//let outrightTrade trade quantity =
//    { trade with NettingQuantity = quantity; OutrightQuantity = trade.OutrightQuantity - quantity } 

let finalTrades = netTrades trades

//let totalNetting = finalTrades |> Seq.sumBy (fun x -> x.NettingQuantity)

//let totalOutright = finalTrades |> Seq.sumBy (fun x -> x.OutrightQuantity)

//printfn "%A" totalNetting

//printfn "%A" totalOutright

finalTrades |> Seq.iter (fun x -> printfn "%A " x)