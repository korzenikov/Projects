using Microsoft.FSharp.Core;
using static NettingLibrary.TradesNetter;
using System.Linq;
using System;

namespace NettingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Net_OneTradeIneligible_IsNotNetWithEligibleTrades();
        }
        private static void Net_OneTradeIneligible_IsNotNetWithEligibleTrades()
        {
            var trades = new[] {
                GetDefaultTrade(500, "Strategy1"),
                GetDefaultTrade(100, "Strategy2"),
                GetDefaultTrade(-200, "Strategy3"),
                GetDefaultTrade(300, "Strategy4")
            };

            trades.Last().IsEligible = false;

            var expectedNettingTrades = new[] {
                GetDefaultTrade(167, "Strategy1"),
                GetDefaultTrade(33, "Strategy2"),
                GetDefaultTrade(-200, "Strategy3")
            };

            var expectedOutrightTrades = new[] {
                GetDefaultTrade(333, "Strategy1"),
                GetDefaultTrade(67, "Strategy2"),
            };

            var expectedIneligibleTrades = new[] { GetDefaultTrade(300, "Strategy4") };

            var nettedTrades = netTrades(trades.Select(DefaultTrade2Trade)).ToArray();

        }

        private static DefaultTrade GetDefaultTrade(int quantity, string strategy)
        {
            return new DefaultTrade("", strategy, quantity);
        }

        private static Trade DefaultTrade2Trade(DefaultTrade trade)
        {
            return new Trade(trade.Account, trade.Strategy, trade.Quantity, trade.IsEligible);
        }
    }
}
 