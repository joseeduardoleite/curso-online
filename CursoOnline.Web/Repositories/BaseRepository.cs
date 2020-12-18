using System.Collections.Generic;
using System.Linq;
using CursoOnline.Dominio._Base;
using CursoOnline.Web.Data;

namespace CursoOnline.Web.Repositories
{
    public class BaseRepository<TEntidade> : IRepository<TEntidade> where TEntidade : Entidade
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(TEntidade entity)
        {
            _context.Set<TEntidade>().Add(entity);
        }

        public virtual TEntidade ObterPorId(int id)
        {
            var query = _context.Set<TEntidade>().Where(x => x.Id == id);

            return query.Any() ? query.First() : null;
        }

        public virtual List<TEntidade> Consultar()
        {
            var entidades = _context.Set<TEntidade>().ToList();

            return entidades.Any() ? entidades : new List<TEntidade>();
        }
    }
}