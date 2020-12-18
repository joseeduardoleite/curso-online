using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos.Services
{
    public interface ICursoRepository : IRepository<Curso>
    {
        // void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}