using Xunit;
using CursoOnline.Dominio._Base;

namespace CursoOnline.DominioTest._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDominio exception, string mensagem)
        {
            if (exception.MensagensErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(true, $"Esperava '{mensagem}'");
        }
    }
}