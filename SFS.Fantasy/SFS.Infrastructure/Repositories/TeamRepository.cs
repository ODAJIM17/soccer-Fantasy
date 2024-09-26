using Microsoft.EntityFrameworkCore;
using SFS.Domain.DTOs;
using SFS.Domain.Entities;
using SFS.Domain.Responses;
using SFS.Infrastructure.Context;
using SFS.Services;
using SFS.Services.Interfaces;
using System.Diagnostics.Metrics;

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

        public async Task<Team> GetByIdAsync(int id)
        {
            var team = await _context.Teams
                 .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);
            return team!;
        }

        public async Task<IEnumerable<Country>> GetComboAsync()
        {
            var countries = await _context.Countries
             .OrderBy(x => x.Name)
             .ToListAsync();
            return countries;
        }

        public async Task<IEnumerable<Team>> GetComboAsync(int countryId)
        {
            return await _context.Teams
                .Where(x => x.CountryId == countryId)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO)
        {
            var currentTeam = await _context.Teams.FindAsync(teamDTO.Id);
            if (currentTeam == null)
            {
                return new ActionResponse<Team>
                {
                    WasSuccess = false,
                    Message = "ERR005"
                };
            }

            var country = await _context.Countries.FindAsync(teamDTO.CountryId);
            if (country == null)
            {
                return new ActionResponse<Team>
                {
                    WasSuccess = false,
                    Message = "ERR004"
                };
            }

            if (!string.IsNullOrEmpty(teamDTO.Image))
            {
                var imageBase64 = Convert.FromBase64String(teamDTO.Image!);
                currentTeam.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
            }

            currentTeam.Country = country;
            currentTeam.Name = teamDTO.Name;

            _context.Update(currentTeam);
            try
            {
                await _context.SaveChangesAsync();
                return new ActionResponse<Team>
                {
                    WasSuccess = true,
                    Result = currentTeam
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Team>
                {
                    WasSuccess = false,
                    Message = "ERR003"
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Team>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }
    }
}