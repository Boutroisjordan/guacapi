
namespace GuacAPI.Interface;

//Use it to define class is a repository
public interface IRepository
{
    IUnitOfWork UnitOfWork { get; }
}