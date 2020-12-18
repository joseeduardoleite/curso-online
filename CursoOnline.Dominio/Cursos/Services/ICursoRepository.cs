using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos.Services
{
    public interface ICursoRepository : IRepository<Curso>
    {
        Curso ObterPeloNome(string nome);
    }
}