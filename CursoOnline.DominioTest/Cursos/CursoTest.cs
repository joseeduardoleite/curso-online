using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos.Enums;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        private  readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly ECursoPublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;
        private readonly Faker _faker;

        public CursoTest()
        {
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = ECursoPublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }

        [Fact]
        public void Deve_criar_curso()
        {
            var cursoEsperado = new {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor, cursoEsperado.Descricao);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_deve_curso_ter_nome_invalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDominio>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build()).ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_carga_horaria_invalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ExcecaoDominio>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()).ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Nao_deve_curso_ter_valor_invalido(double valorInvalido)
        {
            Assert.Throws<ExcecaoDominio>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build()).ComMensagem(Resource.ValorInvalido);
        }

        [Fact]
        public void Deve_alterar_nome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Nao_deve_alterar_com_nome_invalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDominio>(() => curso.AlterarNome(nomeInvalido)).ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void Deve_alterar_carga_horaria()
        {
            var cargaHorariaEsperada = 20.5;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Nao_deve_alterar_com_carga_horaria_invalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida)).ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void Deve_alterar_valor()
        {
            var valorEsperado = 240.90;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Nao_deve_alterar_com_valor_invalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDominio>(() => curso.AlterarValor(valorInvalido)).ComMensagem(Resource.ValorInvalido);
        }
    }
}