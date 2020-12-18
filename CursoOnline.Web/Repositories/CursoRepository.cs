using System.Linq;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Services;
using CursoOnline.Web.Data;

namespace CursoOnline.Web.Repositories
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