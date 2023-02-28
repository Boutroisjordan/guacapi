using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;
namespace GuacAPI.Services;
 
public class DomainService : IDomainService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    #endregion

    // #region Constructors
    public DomainService(DataContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
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

    public async Task<Domain> AddDomain(DomainRegister request)
    {

        Domain domain = _mapper.Map<Domain>(request);

        var savedDomain = await _context.Domains.AddAsync(domain);
        await _context.SaveChangesAsync();
        return savedDomain.Entity;
    }

    public async Task<Domain> UpdateDomain(int id, DomainRegister request)
    {

            var domain = await _context.Domains.FindAsync(id);
            Domain newDomain = _mapper.Map(request, domain);
            if (domain == null)
            {
                throw new Exception("Domain Not Found");
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