namespace GNB.Domain.Entities
{
    public class TransactionSummary
    {
        public string Sku { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal Total { get; set; }
        public TransactionSummary()
        {
            Transactions = new List<Transaction>();
        }
    }
}
