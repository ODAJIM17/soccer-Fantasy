using Microsoft.EntityFrameworkCore;
using SFS.Domain.Entities;
using SFS.Domain.Responses;
using SFS.Infrastructure.Context;
using SFS.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SFS.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly SoccerDbContext _context;

        public CountryRepository(IDbContextFactory<SoccerDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public async Task AddAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Country>> GetAllAsync()
        {
            var country = await _context.Countries.ToListAsync();
            return country;
        }

        public async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
           .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }

        public async Task<ActionResponse<Country>> GetIdAsync(int id)
        {
            var country = await _context.Countries
             .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "ERR001"
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            return await _context.Countries
            .OrderBy(x => x.Name)
            .ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            return country;
        }

        public async Task UpdateAsync(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Country country)
        {
            if (country is not null)
            {
                _context.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }
}