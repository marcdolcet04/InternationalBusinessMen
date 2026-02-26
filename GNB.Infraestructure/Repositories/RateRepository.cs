using GNB.Domain.Entities;
using GNB.Domain.Interfaces;
using System.Text.Json;

namespace GNB.Infrastructure.Repositories;

public class RateRepository : IRateRepository
{
    private readonly string _filePath;

    public RateRepository()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rates.json");
    }

    public async Task<IEnumerable<Rate>> GetAllAsync()
    {
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            var rates = JsonSerializer.Deserialize<List<RateJson>>(json) ?? new List<RateJson>();

            return rates.Select(r => new Rate(r.From, r.To, decimal.Parse(r.RateValue, System.Globalization.CultureInfo.InvariantCulture)));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al leer rates.json: {ex.Message}", ex);
        }
    }

    private class RateJson
    {
        [System.Text.Json.Serialization.JsonPropertyName("from")]
        public string From { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("rate")]
        public string RateValue { get; set; } = string.Empty;
    }
}

