using System.Threading.Tasks;

namespace CursoOnline.Dominio._Base.Services
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}