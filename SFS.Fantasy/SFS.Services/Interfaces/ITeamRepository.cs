using SFS.Domain.DTOs;
using SFS.Domain.Entities;
using SFS.Domain.Responses;

namespace SFS.Services.Interfaces;

public interface ITeamRepository
{
    Task AddAsync(TeamDTO teamDTO);

    Task<List<Team>> GetAllAsync();

    Task<Team> GetByIdAsync(int id);

    Task UpdateAsync(Team team);

    Task DeleteByIdAsync(Team team);

    Task<ActionResponse<IEnumerable<Team>>> GetAsync();

    Task<IEnumerable<Country>> GetComboAsync();
}