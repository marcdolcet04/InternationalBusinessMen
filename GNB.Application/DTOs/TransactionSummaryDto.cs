namespace GNB.Application.DTOs;

public class TransactionSummaryDto
{
    public string Sku { get; set; } = string.Empty;
    public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    public decimal TotalAmount { get; set; }
}
