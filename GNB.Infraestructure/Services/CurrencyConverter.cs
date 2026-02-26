using GNB.Domain.Entities;
using GNB.Domain.Interfaces;

namespace GNB.Infrastructure.Services;

public class CurrencyConverter : ICurrencyConverter
{
    public decimal Convert(decimal amount, string fromCurrency, string toCurrency, IEnumerable<Rate> rates)
    {
        try
        {
            if (fromCurrency == toCurrency)
                return amount;

            var rateList = rates.ToList();
            var conversionRate = FindConversionRate(fromCurrency, toCurrency, rateList);

            if (conversionRate == null)
                throw new InvalidOperationException($"No se encontró conversión de {fromCurrency} a {toCurrency}");

            return Math.Round(amount * conversionRate.Value, 2, MidpointRounding.ToEven);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al convertir de {fromCurrency} a {toCurrency}: {ex.Message}", ex);
        }
    }

    private decimal? FindConversionRate(string from, string to, List<Rate> rates)
    {
        // BFS para encontrar el camino entre divisas
        var visited = new HashSet<string>();
        var queue = new Queue<(string Currency, decimal AccumulatedRate)>();
        queue.Enqueue((from, 1m));

        while (queue.Count > 0)
        {
            var (current, accumulated) = queue.Dequeue();

            if (current == to)
                return accumulated;

            if (!visited.Add(current))
                continue;

            foreach (var rate in rates)
            {
                if (rate.From == current && !visited.Contains(rate.To))
                    queue.Enqueue((rate.To, Math.Round(accumulated * rate.RateValue, 2, MidpointRounding.ToEven)));

                if (rate.To == current && !visited.Contains(rate.From))
                    queue.Enqueue((rate.From, Math.Round(accumulated * (1m / rate.RateValue), 2, MidpointRounding.ToEven)));
            }
        }

        return null;
    }
}
