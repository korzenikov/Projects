namespace CourseraTasks.FSharp

module MergeSort =

    let split list = 
       let rec splitRec (list:List<_>) count acc = 
            if count = 0 then
                (List.rev acc, list)
            else
                splitRec list.Tail (count - 1) (list.Head :: acc)
   
       splitRec list (list.Length/2) []

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
