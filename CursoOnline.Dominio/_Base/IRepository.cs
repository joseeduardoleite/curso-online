using System.Collections.Generic;

namespace CursoOnline.Dominio._Base
{
    public interface IRepository<TEntidade>
    {
         TEntidade ObterPorId(int id);
         List<TEntidade> Consultar();
         void Adicionar(TEntidade entity);
    }
}