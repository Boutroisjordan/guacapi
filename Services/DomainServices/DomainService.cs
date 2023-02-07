using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;
 
public class DomainService : IDomainService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    // #region Constructors
    public DomainService(DataContext context)
    {
        this._context = context;
    }

    public async Task<ICollection<Domain>> GetAllDomains()
    {
        var domains = await _context.Domains.ToListAsync();
        return domains;
    }

    public async Task<Domain> GetDomainById(int id)
    {
        var domainById = await _context.Domains.FindAsync(id);
        return domainById;
    }

    public async Task<Domain> GetDomainByName(string name)
    {
        var domainName = await _context.Domains.FirstOrDefaultAsync(x => x.Name == name);
        return domainName;
    }

    public async Task<Domain> AddDomain(Domain domain)
    {
        // return await Task.Run(() =>
        // {
           

            var addedDomain = new Domain {
                Name = domain.Name 
            };

            var savedDomain = _context.Domains.Add(addedDomain).Entity;


            await _context.SaveChangesAsync();
            return savedDomain;
        // });
    }

    public async Task<Domain> UpdateDomain(int id, Domain request)
    {
        // return await Task.Run(() =>
        // {
            var domain = await _context.Domains.FindAsync(id);
            if (domain == null)
            {
                return null;
            }

            domain.Name = request.Name;
            await _context.SaveChangesAsync();
            return domain;
        // });
    }

    public async Task<Domain> DeleteDomain(int id)
    {
        // return await Task.Run(() =>
        // {
            var domain = await _context.Domains.FindAsync(id);
            if (domain == null)
            {
                return null;
            }

            _context.Domains.Remove(domain);
            await _context.SaveChangesAsync();
            return domain;
        // });
    }

}