using GNB.Domain.Entities;

namespace GNB.Domain.Interfaces
{
    public interface IRateRepository
    {    
        Task<IEnumerable<Rate>> GetAllAsync();
    }
}
