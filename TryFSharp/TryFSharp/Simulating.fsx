#load "..\packages\FSharp.Charting.0.90.14\FSharp.Charting.fsx"
#load "..\packages\MathNet.Numerics.FSharp.3.13.1\MathNet.Numerics.fsx"
#load "Chart.fs"
#load "Models.fs"

#r "..\packages\FSharp.Data.2.3.2\lib\portable-net45+netcore45\FSharp.Data.dll"

open System
open FSharp.Charting
open MathNet.Numerics.Distributions
open FSharp.Data
open Models
// Load CSV type provider, DojoChart and Math.NET


// Create type for accessing Yahoo Finance
let [<Literal>] msftUrl = "http://ichart.finance.yahoo.com/table.csv?s=MSFT"
type StockData = CsvProvider<msftUrl, InferRows=5>

/// Helper function that returns URL for required stock data
let urlFor ticker (startDate:System.DateTime) (endDate:System.DateTime) = 
    let root = "http://ichart.finance.yahoo.com"
    sprintf "%s/table.csv?s=%s&a=%i&b=%i&c=%i&d=%i&e=%i&f=%i" 
        root ticker (startDate.Month - 1) startDate.Day startDate.Year 
                    (endDate.Month - 1) endDate.Day endDate.Year

/// Returns stock data for a given ticker name and date range
let stockData ticker startDate endDate = 
    StockData.Load(urlFor ticker startDate endDate)
       
let msft2011 = stockData "MSFT" (DateTime(2016,1,1)) DateTime.Now
let first = msft2011.Rows |> Seq.minBy (fun itm -> itm.Date)
let firstClose = first.Close |> float


/// Generates prices that can be compared with 'msft2011' data
let simulateHistoricalPrices drift volatility = 
    let dist = Normal(0.0, 1.0)
    let dates = [ for v in msft2011.Rows -> v.Date.DayOfYear ] |> List.rev
    let randoms = randomPrice drift volatility 0.005 firstClose dist
    Seq.zip dates randoms

Chart.Combine
  [
    Chart.Line(simulateHistoricalPrices 0.05 0.1, Name = "Generated") |> Chart.WithLegend()
    Chart.Line([ for item in msft2011.Rows -> item.Date.DayOfYear, item.Close ], Name = "Real") |> Chart.WithLegend()
    ]