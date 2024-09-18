using SFS.Domain.Entities;
using SFS.Domain.Responses;

namespace SFS.Services.Interfaces
{
    public interface ICountryRepository
    {
        Task AddAsync(Country country);

        Task<List<Country>> GetAllAsync();

        Task<Country> GetByIdAsync(int id);

        Task UpdateAsync(Country country);

        Task DeleteByIdAsync(Country country);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<IEnumerable<Country>> GetComboAsync();
    }
}