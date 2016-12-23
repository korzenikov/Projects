module FuzzySets

type FuzzySet =
    | Triangle of a : double * b :  double * c : double
    | LeftBound of a : double * b : double
    | RightBound of b : double * right : double

let truthValue fs x =
    let rec truthValueRec fs x =
        match fs with
            | LeftBound (a, b) -> 
                if x > b then 1.0 else if x < a then 0.0 else (x - a)/(b - a)
            | RightBound (b, c) -> 
               if x < b then 1.0 else if x > c then 0.0 else 1.0 - (x - b)/(c - b)
            | Triangle (a, b, c) -> 
                if x > b then truthValueRec (RightBound(b, c)) x else truthValueRec (LeftBound(a, b)) x
    truthValueRec fs x