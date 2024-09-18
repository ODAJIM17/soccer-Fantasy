using Microsoft.EntityFrameworkCore;
using SFS.Domain.Entities;
using SFS.Infrastructure.Context;
using SFS.Services.Interfaces;

namespace SFS.Infrastructure.Repositories;

public class Repository : IRepository
{
    private readonly SoccerDbContext _context;

    public Repository(IDbContextFactory<SoccerDbContext> factory)
    {
        _context = factory.CreateDbContext();
    }

    public Task<T> GetAsync<T>()
    {
        throw new NotImplementedException();
    }

    public Task<object> PostAsync<T>(T model)
    {
        throw new NotImplementedException();
    }

    public Task<TActionResponse> PostAsync<T, TActionResponse>(T model)
    {
        throw new NotImplementedException();
    }
}