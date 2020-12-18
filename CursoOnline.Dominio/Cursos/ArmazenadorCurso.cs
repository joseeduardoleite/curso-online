using System;
using CursoOnline.Dominio.Cursos.DTO;
using CursoOnline.Dominio.Cursos.Enums;
using CursoOnline.Dominio.Cursos.Services;

namespace CursoOnline.Dominio.Cursos
{
    public class ArmazenadorCurso
    {
        private readonly ICursoRepository _cursoRepository;

        public ArmazenadorCurso(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public void Armazenar(CursoDto cursoDto)
        {

            var cursoJaSalvo = _cursoRepository.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException("Nome do curso já consta no banco de dados");
            
            if (!Enum.TryParse<ECursoPublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException("Público Alvo inválido");

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor, cursoDto.Descricao);

            _cursoRepository.Adicionar(curso);
        }
    }
}