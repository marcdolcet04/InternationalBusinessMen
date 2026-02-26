using GNB.Application.DTOs;
using GNB.Domain.Interfaces;

namespace GNB.Application.Services;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IRateRepository _rateRepository;
    private readonly ICurrencyConverter _currencyConverter;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IRateRepository rateRepository,
        ICurrencyConverter currencyConverter)
    {
        _transactionRepository = transactionRepository;
        _rateRepository = rateRepository;
        _currencyConverter = currencyConverter;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync()
    {
        try
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(t => new TransactionDto
            {
                Sku = t.Sku,
                Amount = t.Amount,
                Currency = t.Currency
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener todas las transacciones: {ex.Message}", ex);
        }
    }

    public async Task<TransactionSummaryDto> GetTransactionsBySkuAsync(string sku)
    {
        try
        {
            var transactions = await _transactionRepository.GetBySkuAsync(sku);
            var rates = await _rateRepository.GetAllAsync();
            var rateList = rates.ToList();

            var convertedTransactions = transactions.Select(t => new TransactionDto
            {
                Sku = t.Sku,
                Amount = _currencyConverter.Convert(t.Amount, t.Currency, "EUR", rateList),
                Currency = "EUR"
            }).ToList();

            return new TransactionSummaryDto
            {
                Sku = sku,
                Transactions = convertedTransactions,
                TotalAmount = convertedTransactions.Sum(t => t.Amount)
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener transacciones del SKU {sku}: {ex.Message}", ex);
        }
    }
}
