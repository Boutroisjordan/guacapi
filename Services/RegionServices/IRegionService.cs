using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IRegionService
{
    Task<List<Region>> GetAllRegions();
    Task<Region?> GetOne(int id);
    Task<Region> AddRegion(Region item);
    Task<Region?> UpdateRegion(int id, Region region);
    Task<List<Region>?> DeleteRegion(int id);
    void SaveChanges();

}