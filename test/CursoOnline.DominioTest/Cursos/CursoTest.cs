using System;
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
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
    }

    public class CursoTest
    {
        [Fact]
        public void Deve_criar_curso()
        {
            var cursoEsperado = new {
                Nome = "C# Completo",
                CargaHoraria = (double)80,
                PublicoAlvo = "Estudantes",
                Valor = (double)500.0
            };

            Curso curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }
    }
}