namespace SFS.Services.Interfaces;

public interface IRepository
{
    Task<T> GetAsync<T>(string url);

    Task<object> PostAsync<T>(string url, T model);

    Task<TActionResponse> PostAsync<T, TActionResponse>(string url, T model);
}