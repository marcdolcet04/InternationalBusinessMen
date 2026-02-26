using GNB.Domain.Entities;
using GNB.Domain.Interfaces;

namespace GNB.Application.Services;

public class RateService
{
    private readonly IRateRepository _rateRepository;

    public RateService(IRateRepository rateRepository)
    {
        _rateRepository = rateRepository;
    }

    public async Task<IEnumerable<Rate>> GetAllRatesAsync()
    {
        try
        {
            return await _rateRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener los rates: {ex.Message}", ex);
        }
    }
}