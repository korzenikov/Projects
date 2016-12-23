#load "FuzzySets.fs"
open FuzzyLogic.FuzzySets

let low = RightBound(20.0, 40.0)
let medium = Triangle(20.0, 40.0, 60.0)
let high = LeftBound (40.0, 60.0)
