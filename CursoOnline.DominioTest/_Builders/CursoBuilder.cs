using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Enums;

namespace CursoOnline.DominioTest._Builders
{
    public class CursoBuilder
    {
        private string _nome = "C# Completo";
        private double _cargaHoraria = 80;
        private ECursoPublicoAlvo _publicoAlvo = ECursoPublicoAlvo.Estudante;
        private double _valor = 950.0;
        private string _descricao = "Descrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(ECursoPublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }
    }
}