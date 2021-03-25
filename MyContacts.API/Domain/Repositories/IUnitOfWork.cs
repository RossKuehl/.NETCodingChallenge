using System.Threading.Tasks;

namespace MyContacts.API.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
