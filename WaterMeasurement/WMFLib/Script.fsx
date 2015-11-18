// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "WaterMeasurement.fs"
open WMFLIb.WaterMeasurement

let buildingHeights1 = [ 5; 4; 3; 2; 1; 2; 3; 4]
let buildingHeights2 = [ 2; 1; 4; 3; 4; 1; 2 ]
let buildingHeights3 = [ 2; 5; 1; 2; 3; 4; 7; 7; 6 ]
let buildingHeights4 = [ 1; 3; 5; 6; 4; 2 ]
let buildingHeights5 = [ 9; 8; 6; 7; 4 ]
printfn "%A" (measureWater buildingHeights1)
printfn "%A" (measureWater buildingHeights2)
printfn "%A" (measureWater buildingHeights3)
printfn "%A" (measureWater buildingHeights4)
printfn "%A" (measureWater buildingHeights5)