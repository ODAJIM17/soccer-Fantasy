using SFS.Domain.Entities;
using SFS.Domain.Responses;

namespace SFS.Services.Interfaces
{
    public interface ICountryRepository
    {
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<List<Country>> GetAllAsync();

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<IEnumerable<Country>> GetComboAsync();

        Task AddAsync(Country country);
    }
}