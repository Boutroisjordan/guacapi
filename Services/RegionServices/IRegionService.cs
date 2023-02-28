using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IRegionService
{
    Task<List<Region>> GetAllRegions();
    Task<Region> GetOne(int id);
    Task<Region> AddRegion(RegionRegister item);
    Task<Region> UpdateRegion(int id, RegionRegister region);
    Task<List<Region>> DeleteRegion(int id);

}