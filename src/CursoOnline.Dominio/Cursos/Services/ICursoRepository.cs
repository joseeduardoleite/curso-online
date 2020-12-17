namespace CursoOnline.Dominio.Cursos.Services
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}