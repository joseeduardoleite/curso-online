using System;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    // "Eu, enquanto administrador, quero criar e ditar cursos para que sejam abertas matriculas para o mesmo."

    // Critérios de aceite
    // - Criar um curso com nome, carga horária, público alvo e valor do curso
    // - As opções para público alvo são: Estudante, Universiário, Empregado e Empregador
    // - Todos os campos do curso são obrigatórios

    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private  readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly ECursoPublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");

            _nome = "C# Completo";
            _cargaHoraria = 80;
            _publicoAlvo = ECursoPublicoAlvo.Estudante;
            _valor = 950.0;
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void Deve_criar_curso()
        {
            var cursoEsperado = new {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_deve_curso_ter_nome_invalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_carga_horaria_menor_que_1(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() => new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor)).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_valor_menor_que_1(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() => new Curso(_nome, _cargaHoraria, _publicoAlvo, valorInvalido)).ComMensagem("Valor inválido");
        }
    }

    public class Curso
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public ECursoPublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, double cargaHoraria, ECursoPublicoAlvo publicoAlvo, double valor)
        {
            if (String.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");
            
            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor inválido");
            
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }

    public enum ECursoPublicoAlvo
    {
        Estudante,
        Universitário,
        Empregado,
        Empreendedor
    }
}