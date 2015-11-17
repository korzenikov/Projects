namespace NettingClient
{
    internal class DefaultTrade
    {
        private readonly string account;
        private readonly string strategy;
        private readonly int quantity;

        public DefaultTrade(string account, string strategy, int quantity)
        {
            this.account = account;
            this.strategy = strategy;
            this.quantity = quantity;
        }
        
        public string Account => account;

        public string Strategy => strategy;

        public int Quantity => quantity;

        public bool IsEligible { get; internal set; }
    }
}