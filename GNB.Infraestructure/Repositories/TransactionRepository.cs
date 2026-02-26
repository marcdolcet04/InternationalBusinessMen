using GNB.Domain.Entities;
using GNB.Domain.Interfaces;
using System.Text.Json;

namespace GNB.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly string _filePath;

    public TransactionRepository()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.json");
    }

    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        try
        {
            var json = await File.ReadAllTextAsync(_filePath);
            var transactions = JsonSerializer.Deserialize<List<TransactionJson>>(json) ?? new List<TransactionJson>();

            return transactions.Select(t => new Transaction(t.Sku, decimal.Parse(t.Amount, System.Globalization.CultureInfo.InvariantCulture), t.Currency));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al leer transactions.json: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Transaction>> GetBySkuAsync(string sku)
    {
        try
        {
            var all = await GetAllAsync();
            return all.Where(t => t.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al filtrar por SKU {sku}: {ex.Message}", ex);
        }
    }

    private class TransactionJson
    {
        [System.Text.Json.Serialization.JsonPropertyName("sku")]
        public string Sku { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("amount")]
        public string Amount { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
    }
}
