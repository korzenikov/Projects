// This F# dojo is directly inspired by the 
// Digit Recognizer competition from Kaggle.com:
// http://www.kaggle.com/c/digit-recognizer
// The datasets below are simply shorter versions of
// the training dataset from Kaggle.
 
// The goal of the dojo will be to
// create a classifier that uses training data
// to recognize hand-written digits, and
// evaluate the quality of our classifier
// by looking at predictions on the validation data.

// This file provides some guidance through the problem:
// each section is numbered, and 
// solves one piece you will need. Sections contain
// general instructions, 
// [ YOUR CODE GOES HERE! ] tags where you should
// make the magic happen, and
// <F# QUICK-STARTER> blocks. These are small
// F# tutorials illustrating aspects of the
// syntax which could come in handy. Run them,
// see what happens, and tweak them to fit your goals!

open System
open System.IO

// the following might come in handy: 
//File.ReadAllLines(path)
// returns an array of strings for each line 
 
let path = "D:\\trainingsample.csv"

let strings = File.ReadAllLines(path)

// 2. EXTRACTING COLUMNS
 
// Break each line of the file into an array of string,
// separating by commas, using Array.map

// <F# QUICK-STARTER> 
// Array.map quick-starter:
// Array.map takes an array, and transforms it
// into another array by applying a function to it.
// Example: starting from an array of strings:

// The following function might help
let split (s:string) = s.Split(',')

let allRows = strings |> Array.map split 
 
// 3. CLEANING UP HEADERS
let rows = allRows.[1 ..]
// 4. CONVERTING FROM STRINGS TO INTS
 
// Now that we have an array containing arrays of strings,
// and the headers are gone, we need to transform it 
// into an array of arrays of integers.
// Array.map seems like a good idea again :)

let convert (s:string) = Convert.ToInt32(s)

let convertedRow row = row |> Array.map convert
 
let integerRows = rows |> Array.map convertedRow
 
// 5. CONVERTING ARRAYS TO RECORDS
 
// Rather than dealing with a raw array of ints,
// for convenience let's store these into an array of Records

// <F# QUICK-STARTER>  
// Record quick starter: we can declare a 
// Record (a lightweight, immutable class) type that way:
type Example = { Label:int; Pixels:int[] }
// and instantiate one this way:
//let example = { Label = 1; Pixels = [| 1; 2; 3; |] }
// </F# QUICK-STARTER>  
let example (row:int[]) = { Label = row.[0]; Pixels = row.[1..]}
 
let examples = integerRows |> Array.map example

// 6. COMPUTING DISTANCES
 
// We need to compute the distance between images
// Math reminder: the euclidean distance is
// distance [ x1; y1; z1 ] [ x2; y2; z2 ] = 
// sqrt((x1-x2)*(x1-x2) + (y1-y2)*(y1-y2) + (z1-z2)*(z1-z2))
 
// <F# QUICK-STARTER> 
// Array.map2 could come in handy here.
// Suppose we have 2 arrays:
let point1 = [| 0; 1; 2 |]
let point2 = [| 3; 4; 5 |]
// Array.map2 takes 2 arrays at a time
// and maps pairs of elements, for instance:
let map2Example = 
    Array.map2 (fun p1 p2 -> p1 + p2) point1 point2
// This simply computes the sums for point1 and point2,
// but we can easily turn this into a function now:
let map2PointsExample (P1: int[]) (P2: int[]) =
    Array.map2 (fun p1 p2 -> p1 + p2) P1 P2
// </F# QUICK-STARTER>  

let squaredDiffs (P1: int[]) (P2: int[]) = 
    Array.map2 (fun p1 p2 -> (p1 - p2)*(p1 - p2)) P1 P2

// Having a function like
let distance (p1: int[]) (p2: int[]) = (squaredDiffs p1 p2) |> Array.sum
// would come in very handy right now,
// except that in this case, 
// 42 is likely not the right answer
 
// 7. WRITING THE CLASSIFIER FUNCTION
 
// We are now ready to write a classifier function!
// The classifier should take a set of pixels
// (an array of ints) as an input, search for the
// closest example in our sample, and predict
// the value of that closest element.
 
// <F# QUICK-STARTER> 
// Array.minBy can be handy here, to find
// the closest element in the Array of examples.
// Suppose we have an Array of Example:
// </F# QUICK-STARTER>  
 

 // The classifier function should probably
// look like this - except that this one will
// classify everything as a 0:
let classify (unknown:int[]) =
    examples |> Array.minBy (fun x -> distance x.Pixels unknown) |> (fun x -> x.Label)
 
// [ YOUR CODE GOES HERE! ]
 
 
// 8. EVALUATING THE MODEL AGAINST VALIDATION DATA
 
// Now that we have a classifier, we need to check
// how good it is. 
// This is where the 2nd file, validationsample.csv,
// comes in handy. 
// For each Example in the 2nd file,
// we know what the true Label is, so we can compare
// that value with what the classifier says.
// You could now check for each 500 example in that file
// whether your classifier returns the correct answer,
// and compute the % correctly predicted.
 
let path2 = "D:\\validationsample.csv"

let allRows2 = File.ReadAllLines(path2) |> Array.map split 

let rows2 = allRows2.[1 .. ]
 
let integerRows2 = rows2 |> Array.map convertedRow
 
let validationSamples = integerRows2 |> Array.map example

let actualLabels = validationSamples |> Array.map (fun sample -> classify sample.Pixels) 

let expectedLabels = validationSamples |> Array.map (fun sample -> sample.Label)

let comparisons = Array.map2 (fun expected actual -> expected = actual) expectedLabels actualLabels

let percentage = Convert.ToDouble((comparisons |> Array.filter (fun p -> p)).Length) / Convert.ToDouble(comparisons.Length)

printfn "%A" (percentage * 100.0)
 
// [ YOUR CODE GOES HERE! ]
//[<EntryPoint>]
//let main argv = 
//    printfn "%A" argv
//    0 // return an integer exit code
