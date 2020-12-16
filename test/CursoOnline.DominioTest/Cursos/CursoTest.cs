using System;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    // "Eu, enquanto administrador, quero criar e ditar cursos para que sejam abertas matriculas para o mesmo."

    // Critérios de aceite
    // - Criar um curso com nome, carga horária, público alvo e valor do curso
    // - As opções para público alvo são: Estudante, Universiário, Empregado e Empregador
    // - Todos os campos do curso são obrigatórios

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

    public class CursoTest
    {
        [Fact]
        public void Deve_criar_curso()
        {
            var cursoEsperado = new {
                Nome = "C# Completo",
                CargaHoraria = (double)80,
                PublicoAlvo = ECursoPublicoAlvo.Estudante,
                Valor = (double)500.0
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_deve_curso_ter_nome_invalido(string nomeInvalido)
        {
            var cursoEsperado = new {
                Nome = "C# Completo",
                CargaHoraria = (double)100,
                PublicoAlvo = ECursoPublicoAlvo.Universitário,
                Valor = (double)700.0
            };

            Assert.Throws<ArgumentException>(
                                        () => new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
                                    ).ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_carga_horaria_menor_que_1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new {
                Nome = "C# Completo",
                CargaHoraria = (double)60,
                PublicoAlvo = ECursoPublicoAlvo.Empregado,
                Valor = (double)350.0
            };

            Assert.Throws<ArgumentException>(
                                        () => new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
                                    ).ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_valor_menor_que_1(double valorInvalido)
        {
            var cursoEsperado = new {
                Nome = "ASP.NET Core Completo",
                CargaHoraria = (double)50,
                PublicoAlvo = ECursoPublicoAlvo.Estudante,
                Valor = (double)3000.0
            };

            Assert.Throws<ArgumentException>(
                                        () => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido)
                                    ).ComMensagem("Valor inválido");
        }
    }
}