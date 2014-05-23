let merge left right  =
  let rec tailRecursiveMerge left right acc = 
      match left, right with
      | [], list | list, [] -> List.rev acc @  list
      | lh::lt, rh::rt -> 
         if lh < rh then tailRecursiveMerge lt right (lh :: acc)
         else tailRecursiveMerge left rt (rh :: acc)
  
  tailRecursiveMerge left right []

let mergeSort (list:seq<_>) =
    let rec mergeSortList (list:List<_>) =
        let length = list.Length
        if (length <= 1) then
            list
        else
            let left  = list |> Seq.take (length / 2) |> List.ofSeq |> mergeSortList
            let right = list |> Seq.skip (length / 2) |> List.ofSeq |> mergeSortList
            merge left right
     
    list |> List.ofSeq |> mergeSortList
    
let array = [9; 8; 7; 6; 5; 4; 3; 2; 1; 10]

printfn "%A" (mergeSort array)   
//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code


