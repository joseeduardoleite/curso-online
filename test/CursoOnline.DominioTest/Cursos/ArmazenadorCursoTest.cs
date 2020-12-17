using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.Enums;
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
        public void Nao_deve_informar_publico_alvo_inalido()
        {
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem("Público Alvo inválido");
        }
    }

    public interface ICursoRepository
    {
        public void Adicionar(Curso curso);
    }

    public class ArmazenadorCurso
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazenadorCurso(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            Enum.TryParse(typeof(ECursoPublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Público Alvo inválido");

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, (ECursoPublicoAlvo)publicoAlvo, cursoDto.Valor, cursoDto.Descricao);

            _cursoRepository.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
    }
}