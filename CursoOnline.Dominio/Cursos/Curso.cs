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
            Descricao = descricao;
        }
    }
}