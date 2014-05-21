
let merge left right  =
  let rec merge left right acc = 
      match left, right with
      | [], [] -> List.rev acc
      | [], l | l, [] -> List.rev acc @  l
      | lh::lt, rh::rt -> 
         if lh < rh then merge lt right (lh :: acc)
         else merge left rt (rh :: acc)
  
  merge left right []
          
let l1 = [1;3;5;7]
let l2 = [2;4;6]

printfn "%A" (merge l1 l2)

//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code
