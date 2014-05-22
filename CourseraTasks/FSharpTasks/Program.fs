let merge left right  =
  let rec tailRecursiveMerge left right acc = 
      match left, right with
      | [], list | list, [] -> List.rev acc @  list
      | lh::lt, rh::rt -> 
         if lh < rh then tailRecursiveMerge lt right (lh :: acc)
         else tailRecursiveMerge left rt (rh :: acc)
  
  tailRecursiveMerge left right []

let rec mergeSort list =
    let length = list |> Seq.length
        
    if (Seq.length list <= 1) then
        list
    else
        let left  = list |> Seq.take (length / 2) |> mergeSort
        let right = list |> Seq.skip (length / 2) |> mergeSort
        merge (left |> List.ofSeq) (right |> List.ofSeq) |> Seq.ofList
    
let array = [9; 8; 7; 6; 5; 4; 3; 2; 1; 10]

printfn "%A" (mergeSort array)   
//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code


