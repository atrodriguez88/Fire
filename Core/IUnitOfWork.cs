

using System.Threading.Tasks;

namespace Fire.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}