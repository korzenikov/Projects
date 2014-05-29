let split list = 
   let rec splitRec (list:List<_>) count total acc = 
        if count = total then
            (List.rev acc, list)
        else
            splitRec list.Tail (count + 1) total (list.Head :: acc)
   
   splitRec list 0 (list.Length/2) []

let merge left right  =
  let rec tailRecursiveMerge left right acc = 
      match left, right with
      | [], list | list, [] -> List.rev acc @  list
      | lh::lt, rh::rt -> 
         if lh < rh then tailRecursiveMerge lt right (lh :: acc)
         else tailRecursiveMerge left rt (rh :: acc)
  
  tailRecursiveMerge left right []


let mergeSort (list:seq<_>) =
    let rec mergeSortList list =
        match list with
        | [] -> [] | head::[] -> list
        | _ ->
            let parts = split list
            match parts with
                | (left, right) -> merge (mergeSortList left) (mergeSortList right)

    list |> List.ofSeq |> mergeSortList


let array = [9; 8; 7; 6; 5; 4; 3; 2; 1; 10]


printfn "%A" (mergeSort array)   



//printfn "%A" (mergeSort array)   
//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code


