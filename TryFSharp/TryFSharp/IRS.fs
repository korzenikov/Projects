type Direction =
    | Buy
    | Sell


type Trade = {
    Account: string
    FxCode: string
    Maturity: int
    Direction: Direction
}

// Splitting logic
let rec insertItem fitsGroup item groups  =
    match groups with
    | items::tail ->  
        match items |> fitsGroup item with
        | true -> (items @ [item])::tail
        | false -> items::(insertItem fitsGroup item tail)
    | [] -> [[item]]

let splitByGroups fitsGroup items = 
    items |> Seq.fold (fun groups item -> groups |> insertItem fitsGroup item) [] 

let nonDividedBy number numbers =
    not (numbers|> Seq.exists(fun i-> number % i = 0))

seq { 1..50 } |> splitByGroups nonDividedBy |> Seq.iter (fun i -> printfn "%A" i )

// Given
let testTrades = 
    [
        { Account = "N1"; FxCode = "EU"; Maturity = 2; Direction = Buy }
        { Account = "N1"; FxCode = "EU"; Maturity = 10; Direction = Sell }

        { Account = "N3"; FxCode = "EU"; Maturity = 10; Direction = Buy }
        
        { Account = "N1"; FxCode = "US"; Maturity = 2; Direction = Sell }
        { Account = "N1"; FxCode = "US"; Maturity = 10; Direction = Sell }
        { Account = "N3"; FxCode = "US"; Maturity = 10; Direction = Buy }

        { Account = "NN2"; FxCode = "EU"; Maturity = 2; Direction = Buy }
        { Account = "NN2"; FxCode = "EU"; Maturity = 10; Direction = Buy }

        { Account = "NN4"; FxCode = "EU"; Maturity = 2; Direction = Sell }
        { Account = "NN4"; FxCode = "EU"; Maturity = 10; Direction = Sell }

        { Account = "NN5"; FxCode = "EU"; Maturity = 2; Direction = Buy }
    ]

let restrictedAccounts =  ["NN2"; "NN4"; "NN5" ] |> Set.ofList

// Business requirements
let containsOppositeTrade trade trades =
    trades |> Seq.exists (fun x -> trade.Maturity = x.Maturity && x.Direction <> trade.Direction) 

let isSharableGroup fxCode (account, trades) sameLevelGroups =
    let sameLevelTrades = sameLevelGroups |> Seq.collect snd
    not((fxCode = "US" || restrictedAccounts.Contains account ) && trades |> Seq.exists(fun t -> sameLevelTrades |> containsOppositeTrade t))

let splitSheets groupSelector fitsOtherSheets trades = 
    trades
    |> Seq.groupBy groupSelector
    |> splitByGroups fitsOtherSheets
    |> Seq.mapi (fun i item -> i, item)
    |> Seq.collect (fun (g, groups) -> groups |> Seq.collect snd |> Seq.map (fun x -> g, x))  

// Execution
testTrades 
    |> Seq.groupBy (fun x -> x.FxCode)
    |> Seq.collect(fun (fxCode, items) -> items |> splitSheets (fun x -> x.Account) (isSharableGroup fxCode))
    |> Seq.map (fun (g, x) -> (x.FxCode + g.ToString(), x))  |> Seq.sortBy fst
    |>  Seq.iter (fun i -> printfn "%A" i )

