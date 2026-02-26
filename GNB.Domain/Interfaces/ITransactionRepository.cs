using GNB.Domain.Entities;

namespace GNB.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetBySkuAsync(string sku);
    }
}
