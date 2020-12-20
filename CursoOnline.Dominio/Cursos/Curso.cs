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
                          .Quando(String.IsNullOrEmpty(nome), Resource.NomeInvalido)
                          .Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida)
                          .Quando(valor < 1, Resource.ValorInvalido)
                          .DispararException();
            
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }

        public void AlterarNome(string nome)
        {
            ValidadorRegra.Novo().Quando(String.IsNullOrEmpty(nome), Resource.NomeInvalido).DispararException();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorRegra.Novo().Quando(cargaHoraria < 1, Resource.CargaHorariaInvalida).DispararException();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorRegra.Novo().Quando(valor < 1, Resource.ValorInvalido).DispararException();

            Valor = valor;
        }
    }
}