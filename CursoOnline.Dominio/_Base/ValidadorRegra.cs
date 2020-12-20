using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio._Base
{
    public class ValidadorRegra
    {
        private readonly List<string> _mensagensErro;

        private ValidadorRegra()
        {
            _mensagensErro = new List<string>();
        }

        public static ValidadorRegra Novo()
        {
            return new ValidadorRegra();
        }
        
        public ValidadorRegra Quando(bool temErro, string mensagemErro)
        {
            if (temErro)
                _mensagensErro.Add(mensagemErro);

            return this;
        }

        public void DispararException()
        {
            if (_mensagensErro.Any())
                throw new ExcecaoDominio(_mensagensErro);
        }
    }

    public class ExcecaoDominio : ArgumentException
    {
        public List<string> MensagensErro { get; set; }

        public ExcecaoDominio(List<string> mensagensErros)
        {
            MensagensErro = mensagensErros;
        }
    }
}