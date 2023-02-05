using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IAppellationService
{
    Task<List<Appellation>?> GetAppellations();
    Task<Appellation?> GetAppellationById(int id);
    Task<Appellation?>GetAppellationByName(string name);
    Task<Appellation?> CreateAppellation(Appellation appellation);
    Task<Appellation?> UpdateAppellation(int id,Appellation appellation);
    Task<Appellation?> DeleteAppellation(int id);
}