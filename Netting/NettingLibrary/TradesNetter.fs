module NettingLibrary.TradesNetter

type Trade = { 
    Account: string
    Strategy: string 
    Quantity: int
    IsEligible: bool
}

let netTrades trades = Netter.net (fun x -> x.Quantity) trades

