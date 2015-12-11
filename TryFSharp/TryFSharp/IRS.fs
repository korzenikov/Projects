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
let rec getGroupIndex fitsOtherGroups (currentIndex, group) groups  =
    let getSameLevelGroups currentIndex = groups |> Seq.filter (fun (g, _) -> g = currentIndex) |> Seq.map snd
    match getSameLevelGroups currentIndex |> fitsOtherGroups group  with
    | false ->  getGroupIndex fitsOtherGroups ((currentIndex + 1), group) groups
    | true -> currentIndex

let assignGroups groupSelector fitsOtherGroups trades = 
    trades
    |> Seq.groupBy groupSelector
    |> Seq.fold 
        (fun previousGroups group -> 
                    let groupIndex = getGroupIndex fitsOtherGroups (0, group) previousGroups
                    previousGroups @ [(groupIndex, group)]) 
        [] 
    |> Seq.collect (fun (g, (_, items)) -> items |> Seq.map (fun x -> g, x))  

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
    trades |> Seq.exists (fun x -> trade.FxCode = x.FxCode && trade.Maturity = x.Maturity && x.Direction <> trade.Direction) 

let isSharableGroup ((fxCode, account), trades) sameLevelGroups =
    let sameLevelTrades = sameLevelGroups |> Seq.collect snd
    not((fxCode = "US" || restrictedAccounts.Contains account ) && trades |> Seq.exists(fun t -> sameLevelTrades |> containsOppositeTrade t))
// Execution
testTrades 
    |> assignGroups (fun x -> x.FxCode, x.Account) isSharableGroup 
    |> Seq.map (fun (g, x) -> (x.FxCode + g.ToString(), x))  |> Seq.sortBy fst
    |>  Seq.iter (fun i -> printfn "%A" i )