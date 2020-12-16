using System;
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
            // Arrange
            const string nome = "Eduardo";
            const double cargaHoraria = 44;
            const string publicoAlvo = "Estudantes";
            const double valor = 500.0;

            // Action
            Curso curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            // Assert
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);
        }
    }
}