using GNB.Domain.Entities;

namespace GNB.Domain.Interfaces
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal amount, string fromCurrency, string toCurrency, IEnumerable<Rate> rates);
    }
}
