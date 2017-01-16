module FuzzyLogic.FuzzySets

open System

type FuzzySet =
    | LeftNumber of a : float * b : float
    | RightNumber of c : float * d : float
    | TriangleNumber of a : float * b :  float * d : float
    | TrapezoidNumber of a : float * b : float * c : float * d : float
    
let truthValue x fs =
    let rec truthValueRec fs x =
        match fs with
        | LeftNumber (a, b) -> 
            if x > b then 1.0 else if x < a then 0.0 else (x - a)/(b - a)
        | RightNumber (c, d) -> 
            if x < c then 1.0 else if x > d then 0.0 else 1.0 - (x - c)/(d - c)
        | TriangleNumber (a, b, d) -> 
            if x > b then truthValueRec (RightNumber(b, d)) x else truthValueRec (LeftNumber(a, b)) x
        | TrapezoidNumber (a, b, c, d) -> 
            if x > c then truthValueRec (RightNumber(c, d)) x else if x < b then truthValueRec (LeftNumber(a, b)) x else 1.0
    truthValueRec fs x

let getX x0 x1 y = (x1 - x0)*(y + x0/(x1 - x0))

let getY ((x1 : float, y1), (x2, y2)) x =
    y1 + (y2 - y1)/(x2 - x1)*(x - x1)

let slice x fs =
    match fs with
        | LeftNumber (a, b) -> 
            [(a, 0.0); (getX a b x, x); (b, x)] |> List.distinct
        | RightNumber (c, d) -> 
            [(c, x); (getX d c x, x); (d, 0.0)]  |> List.distinct
        | TriangleNumber (a, b, d) -> 
            [(a, 0.0); (getX a b x, x); (getX d b x, x); (d, 0.0)] |> List.distinct
        | TrapezoidNumber (a, b, c, d) ->  
            [(a, 0.0); (getX a b x, x); (getX d c x, x); (d, 0.0)] |> List.distinct

let getPoints = slice 1.0

let epsilon = 0.00001

let inBound (x1, y1) (x2, y2) (x : float, y : float) =
    let xl, xr = if x1 < x2 then (x1, x2) else (x2, x1)
    let yl, yh = if y1 < y2 then (y1, y2) else (y2, y1)
    (x > xl || Math.Abs(x - xl) < epsilon) && (x < xr || Math.Abs(x - xr) < epsilon) && (y > yl || Math.Abs(y - yl) < epsilon) && (y < yh || Math.Abs(y - yh) < epsilon)  

let intersection line1 line2 =
    let (x1, y1), (x2, y2) = line1
    let (x3, y3), (x4, y4) = line2
    let deltaX12 = x1 - x2
    let deltaX34 = x3 - x4
    let deltaY12 = y1 - y2
    let deltaY34 = y3 - y4
    let denominator = deltaX12*deltaY34 - deltaY12*deltaX34
    if denominator = 0.0 then None
    else
        let A = x1*y2 - y1*x2
        let B = x3*y4 - y3*x4
        let x = (A*deltaX34 - B*deltaX12)/denominator
        let y = (A*deltaY34 - B*deltaY12)/denominator
        let p = (x, y)
        let p1, p2 = line1
        let p3, p4 = line2
        if inBound p1 p2 p && inBound p3 p4 p then 
            Some p
        else
            None

let getMax fs =
    match fs with
    | LeftNumber (a, b) -> 
        b
    | RightNumber (c, d) -> 
        c
    | TriangleNumber (a, b, d) -> 
        b
    | TrapezoidNumber (a, b, c, d) -> 
        b

let getLeftLine (fs, t) =
    match fs with
    | LeftNumber (a, b) -> 
        ((a, 0.0), (getX a b t, t)) 
    | TriangleNumber (a, b, d) -> 
        ((a, 0.0), (getX a b t, t))
    | TrapezoidNumber (a, b, c, d) -> 
         ((a, 0.0), (getX a b t, t))

let getTopLine (fs, t) =
    match fs with
    | LeftNumber (a, b) -> 
        ((getX a b t, t), (b, t)) 
     | RightNumber (c, d) -> 
        ((c, t), (getX d c t, t)) 
    | TriangleNumber (a, b, d) -> 
        ((getX a b t, t), (getX d b t, t))
    | TrapezoidNumber (a, b, c, d) -> 
        ((getX a b t, t), (getX d c t, t))

let getRightLine(fs, t) =
    match fs with
    | RightNumber (c, d) ->
        (( getX d c t, t), (d, 0.0))
    | TriangleNumber (a, b, d) ->
        (( getX d b t, t), (d, 0.0))
    | TrapezoidNumber (a, b, c, d) -> 
       ((getX d c t, t), (d, 0.0))

let intersectResults r1 r2 =
    let top1 = getTopLine r1
    let right1 =  getRightLine r1
    let left2 = getLeftLine r2
    let top2 = getTopLine r2
    match (intersection top1 left2) with
    | Some (x, y) -> 
        Some (x, y)
    | None ->
        match (intersection right1 left2) with
        | Some (x, y) ->
            Some (x, y)
        | None ->
            match (intersection right1 top2) with
            | Some (x, y) ->
                Some (x, y)
            | None -> 
                None

let unionResults results =
    let rec unionRec li results =
        match results with
        | h1::tail -> 
            let points = 
                match li with
                | Some (xi, _) -> slice (snd h1) (fst h1) |> List.skipWhile (fun p -> fst p <= xi) 
                | None -> slice (snd h1) (fst h1)
            match tail with
            | h2::tail2 ->
                let pi = intersectResults h1 h2 
                match pi with
                | Some (xr, yr) ->
                    List.append (points |> List.takeWhile (fun p -> fst p < xr)) [(xr, yr)] @ unionRec pi (h2::tail2) 
                | None ->
                    points@unionRec pi (h2::tail2)
            | [] -> points
        | [] -> []
    unionRec None results

let centroidOfCompositeShape cs =
    let a = cs |> Seq.sumBy snd
    let x = (cs |> Seq.sumBy (fun ((x, _), a) -> x*a)) / a
    let y = (cs |> Seq.sumBy (fun ((_, y), a) -> y*a)) / a
    ((x, y), a)

let centroid (x1: float, y1: float) (x2, y2) =
    let dx = Math.Abs(x1 - x2)
    let dy = Math.Abs(y1 - y2)
    let minX = Math.Min(x1, x2)
    let h = Math.Min(y1, y2)
    let ct = 
        if (y1 < y2) then
            (minX + 2.0*dx/3.0, h + dy/3.0), dx*dy/2.0
        else
            (minX + dx/3.0, h + dy/3.0), dx*dy/2.0
    let cr = (minX + dx/2.0, h/2.0), dx*h
    centroidOfCompositeShape [ct; cr]

let fireRules handleRule rules =
    rules 
    |> Seq.map handleRule
    |> Seq.map (fun (output, inputs) -> output, inputs |> Seq.map (fun (fs, x) -> truthValue x fs) |> Seq.min)
    
let getTriggeredOutputs handleRule rules =
    rules
    |> fireRules handleRule
    |> Seq.filter (fun (fs, y) -> y > 0.0)
    |> Seq.groupBy fst 
    |> Seq.map (fun (key, s) -> s |> Seq.maxBy (fun (_, y) -> y))
    |> Seq.distinct 
    |> Seq.sortBy (fun (fs, y) -> getMax fs)

let getOutputValue handleRule rules =
    rules 
    |> getTriggeredOutputs handleRule
    |> List.ofSeq
    |> unionResults
    |> Seq.pairwise 
    |> Seq.map (fun (p1, p2) -> centroid p1 p2)
    |> centroidOfCompositeShape
    |> fst 
    |> fst