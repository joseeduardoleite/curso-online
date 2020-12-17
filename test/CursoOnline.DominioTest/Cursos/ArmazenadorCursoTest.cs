using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.DTO;
using CursoOnline.Dominio.Cursos.Services;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorCurso _armazenadorCurso;
        private readonly Mock<ICursoRepository> _cursoRepositoryMock;

        public ArmazenadorCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto {
                Nome = fake.Random.Word(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(50, 1000),
                Descricao = fake.Lorem.Paragraph()
            };

            _cursoRepositoryMock = new Mock<ICursoRepository>();
            _armazenadorCurso = new ArmazenadorCurso(_cursoRepositoryMock.Object);
        }
        [Fact]
        public void Deve_adicionar_curso()
        {
            _armazenadorCurso.Armazenar(_cursoDto);

            _cursoRepositoryMock.Verify(x => x.Adicionar(It.Is<Curso>(x => x.Nome == _cursoDto.Nome && x.Descricao == _cursoDto.Descricao)));
        }

        [Fact]
        public void Nao_deve_informar_publico_alvo_invalido()
        {
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem("Público Alvo inválido");
        }

        [Fact]
        public void Nao_deve_adicionar_curso_com_mesmo_nome_de_outro_ja_salvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositoryMock.Setup(x => x.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem("Nome do curso já consta no banco de dados");
        }
    }    
}