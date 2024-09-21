using Microsoft.EntityFrameworkCore;
using SFS.Domain.DTOs;
using SFS.Domain.Entities;
using SFS.Domain.Responses;
using SFS.Infrastructure.Context;
using SFS.Services;
using SFS.Services.Interfaces;

namespace SFS.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SoccerDbContext _context;
        private readonly IFileStorage _fileStorage;

        public TeamRepository(IDbContextFactory<SoccerDbContext> factory, IFileStorage fileStorage)
        {
            _context = factory.CreateDbContext();
            _fileStorage = fileStorage;
        }

        public async Task AddAsync(TeamDTO teamDTO)
        {
            var country = await _context.Countries.FindAsync(teamDTO.CountryId);

            if (country == null)
            {
                return;
            }

            var team = new Team
            {
                Country = country,
                Name = teamDTO.Name,
            };

            if (!string.IsNullOrEmpty(teamDTO.Image))
            {
                var imageBase64 = Convert.FromBase64String(teamDTO.Image!);
                team.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
            }

            _context.Add(team);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
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

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            var countries = await _context.Countries
             .OrderBy(x => x.Name)
             .ToListAsync();
            return countries;
        }

        public Task UpdateAsync(Team team)
        {
            throw new NotImplementedException();
        }
    }
}