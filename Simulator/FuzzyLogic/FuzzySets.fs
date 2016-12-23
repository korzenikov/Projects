module FuzzyLogic.FuzzySets

type FuzzySet =
    | LeftBound of a : double * b : double
    | RightBound of c : double * d : double
    | Triangle of a : double * b :  double * d : double
    | Trapezoid of a : double * b : double * c : double * d : double
    
let truthValue fs x =
    let rec truthValueRec fs x =
        match fs with
            | LeftBound (a, b) -> 
                if x > b then 1.0 else if x < a then 0.0 else (x - a)/(b - a)
            | RightBound (c, d) -> 
                if x < c then 1.0 else if x > d then 0.0 else 1.0 - (x - c)/(d - c)
            | Triangle (a, b, d) -> 
                if x > b then truthValueRec (RightBound(b, d)) x else truthValueRec (LeftBound(a, b)) x
            | Trapezoid (a, b, c, d) -> 
                if x > c then truthValueRec (RightBound(c, d)) x else if x < b then truthValueRec (LeftBound(a, b)) x else 1.0
    truthValueRec fs x