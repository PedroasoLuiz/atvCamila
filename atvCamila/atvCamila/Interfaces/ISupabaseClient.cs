namespace atvCamila.Interfaces
{
    public interface ISupabaseClient
    {
        Task<string> GetAsync(string endpoint);
        Task<string> PostAsync(string endpoint, string jsonPayload);
        Task<string> PutAsync(string endpoint, string jsonPayload,int id);
        Task<string> PatchAsync(string endpoint, string jsonPayload, int id);
        Task<string> DeleteAsync(string endpoint, int id);
    }
}
