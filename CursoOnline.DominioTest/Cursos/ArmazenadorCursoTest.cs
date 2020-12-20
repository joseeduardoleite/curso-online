using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio._Base;
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

            Assert.Throws<ExcecaoDominio>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem(Resource.PublicoAlvoInvalido);
        }

        [Fact]
        public void Nao_deve_adicionar_curso_com_mesmo_nome_de_outro_ja_salvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositoryMock.Setup(x => x.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDominio>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem(Resource.NomeCursoJaExiste);
        }

        [Fact]
        public void Deve_alterar_dados_do_curso()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(x => x.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void Nao_deve_adicionar_no_repositorio_quando_curso_ja_existe()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(x => x.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorCurso.Armazenar(_cursoDto);

            _cursoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }    
}