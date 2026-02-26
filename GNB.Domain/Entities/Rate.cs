namespace GNB.Domain.Entities
{
    public class Rate
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal RateValue { get; set; }
        public Rate()
        {
        }
        public Rate(string from, string to, decimal rateValue)
        {
            From = from;
            To = to;
            RateValue = rateValue;
        }
    }
}
