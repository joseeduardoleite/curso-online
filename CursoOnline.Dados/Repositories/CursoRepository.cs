using System.Linq;
using CursoOnline.Dados.Data;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Services;

namespace CursoOnline.Dados.Repositories
{
    public class CursoRepository : BaseRepository<Curso>, ICursoRepository
    {
        public CursoRepository(ApplicationDbContext context) : base(context) { }

        public Curso ObterPeloNome(string nome)
        {
            var entidade = _context.Set<Curso>().Where(x => x.Nome.Contains(nome));

            if (entidade.Any())
                return entidade.First();

            return null;
        }
    }
}