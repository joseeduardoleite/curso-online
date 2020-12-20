using System;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos.Enums;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public ECursoPublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        public string Descricao { get; private set; }

        private Curso() { }

        public Curso(string nome, double cargaHoraria, ECursoPublicoAlvo publicoAlvo, double valor, string descricao)
        {
            ValidadorRegra.Novo()
                          .Quando(String.IsNullOrEmpty(nome), "Nome inválido")
                          .Quando(cargaHoraria < 1, "Carga horária inválida")
                          .Quando(valor < 1, "Valor inválido")
                          .DispararException();
            
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }
    }
}