using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos.DTO
{
    public class CursoDto : Entidade
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}