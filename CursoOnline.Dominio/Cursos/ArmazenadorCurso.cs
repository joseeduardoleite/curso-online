using System;
using CursoOnline.Dominio._Base;
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

            ValidadorRegra.Novo()
                          .Quando(cursoJaSalvo != null && cursoJaSalvo.Id != cursoDto.Id, Resource.NomeCursoJaExiste)
                          .Quando(!Enum.TryParse<ECursoPublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo), Resource.PublicoAlvoInvalido)
                          .DispararException();

            var curso = new Curso(cursoDto.Nome, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor, cursoDto.Descricao);

            if (cursoDto.Id > 0) {
                curso = _cursoRepository.ObterPorId(cursoDto.Id);
                curso.AlterarNome(cursoDto.Nome);
                curso.AlterarValor(cursoDto.Valor);
                curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            }

            if (cursoDto.Id == 0)
                _cursoRepository.Adicionar(curso);
        }
    }
}