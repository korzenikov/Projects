#load "..\packages\FSharp.Charting.0.90.14\FSharp.Charting.fsx"
#r "bin\Debug\FuzzyLogic.dll"

open FuzzyLogic.FuzzySets
open FSharp.Charting

open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

let getPoints fs =
    match fs with
        | LeftBound (a, b) -> 
            [(a, 0.0); (b, 1.0)]
        | RightBound (c, d) -> 
            [(c, 1.0); (d, 0.0)]
        | Triangle (a, b, d) -> 
            [(a, 0.0); (b, 1.0); (d, 0.0)]
        | Trapezoid (a, b, c, d) -> 
            [(a, 0.0); (b, 1.0); (c, 1.0); (d, 0.0)]


let getChart x =
    x |> getPoints |> Chart.Line

// Speed
let low = RightBound(20.0, 40.0)
let medium = Triangle(20.0, 40.0, 60.0)
let high = LeftBound (40.0, 60.0)

let speed = [low; medium; high]

speed |> Seq.map getChart |> Chart.Combine  |> Chart.WithLegend()


//let println expr =
//    let rec print expr =
//        match expr with
//        | Application(expr1, expr2) ->
//            // Function application.
//            print expr1
//            printf " "
//            print expr2
//        | SpecificCall <@@ (+) @@> (_, _, exprList) ->
//            // Matches a call to (+). Must appear before Call pattern.
//            print exprList.Head
//            printf " + "
//            print exprList.Tail.Head
//        | Call(exprOpt, methodInfo, exprList) ->
//            // Method or module function call.
//            match exprOpt with
//            | Some expr -> print expr
//            | None -> printf "%s" methodInfo.DeclaringType.Name
//            printf ".%s(" methodInfo.Name
//            if (exprList.IsEmpty) then printf ")" else
//            print exprList.Head
//            for expr in exprList.Tail do
//                printf ","
//                print expr
//            printf ")"
//        | Int32(n) ->
//            printf "%d" n
//        | Lambda(param, body) ->
//            // Lambda expression.
//            printf "fun (%s:%s) -> " param.Name (param.Type.ToString())
//            print body
//        | Let(var, expr1, expr2) ->
//            // Let binding.
//            if (var.IsMutable) then
//                printf "let mutable %s = " var.Name
//            else
//                printf "let %s = " var.Name
//            print expr1
//            printf " in "
//            print expr2
//        | PropertyGet(_, propOrValInfo, _) ->
//            printf "PropertyGet %s" propOrValInfo.Name
//        | String(str) ->
//            printf "%s" str
//        | Value(value, typ) ->
//            printf "%s" (value.ToString())
//        | Var(var) ->
//            printf "%s" var.Name
//        | _ -> printf "%s" (expr.ToString())
//    print expr
//    printfn ""


//
//type Test = 
//    static member EchoExpression([<ReflectedDefinition(true)>] x : Expr<_>) =
//        let rec toCode = function
//            | PropertyGet(_, v, _) -> v.Name
//            | Call (_, mthd, args) ->
//                let argStr = args |> List.map toCode |> String.concat ","
//                sprintf "%s.%s(%s)" mthd.DeclaringType.Name mthd.Name argStr
//        let (WithValue(value, _, expr)) = x
//        printfn "%s evaluates to %O" (toCode expr) value
//
//
//let x, y = 23, 93
//
//
//let velocity = 5
//Test.EchoExpression(velocity)

//let expr : Expr<FuzzySet> = <@ low @>

//println expr


