namespace SFS.Services.Interfaces;

public interface IRepository
{
    Task<T> GetAsync<T>();

    Task<object> PostAsync<T>(T model);

    Task<TActionResponse> PostAsync<T, TActionResponse>(T model);
}