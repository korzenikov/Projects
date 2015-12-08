module WMFLIb.WaterMeasurement
let rec measureWater levels =
    let rec measureWaterRec lowerBorder upperBorder total levels =
        match lowerBorder > upperBorder with
        | true -> levels |> List.rev |> measureWaterRec upperBorder lowerBorder total 
        | false ->
            match levels with
            | [] -> total
            | head::tail -> let diff = lowerBorder - head
                            match diff > 0 with
                            | true ->  measureWaterRec lowerBorder upperBorder (total + diff) tail
                            | false->  measureWaterRec head upperBorder total tail
    let levelsList = levels |> List.ofSeq
    match levelsList with
    | [] -> 0
    | [_] -> 0
    | head::tail -> measureWaterRec head (Seq.last tail) 0 levelsList