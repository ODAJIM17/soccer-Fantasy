using Microsoft.EntityFrameworkCore;
using SFS.Domain.Entities;
using SFS.Domain.Responses;
using SFS.Infrastructure.Context;
using SFS.Services.Interfaces;

namespace SFS.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SoccerDbContext _context;

        public TeamRepository(IDbContextFactory<SoccerDbContext> factory)
        {
            _context = factory.CreateDbContext();
        }

        public Task AddAsync(Team team)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Team team)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Team>> GetAllAsync()
        {
            var teams = await _context.Teams
               .Include(x => x.Country)
               .OrderBy(x => x.Name)
               .ToListAsync();
            return teams;
        }

        public async Task<ActionResponse<IEnumerable<Team>>> GetAsync()
        {
            var teams = await _context.Teams
                .Include(x => x.Country)
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Team>>
            {
                WasSuccess = true,
                Result = teams
            };
        }

        public Task<Team> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Team team)
        {
            throw new NotImplementedException();
        }
    }
}