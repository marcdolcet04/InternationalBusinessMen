namespace GNB.Application.DTOs;

public class TransactionDto
{
    public string Sku { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}
