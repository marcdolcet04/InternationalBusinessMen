namespace GNB.Domain.Entities
{
    public class Transaction
    {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Transaction()
        {
        }
        public Transaction(string sku, decimal amount, string currency)
        {
            Sku = sku;
            Amount = amount;
            Currency = currency;
        }
    }
}
