using System.Collections.Generic;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Cursos.DTO;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorCurso _armazenadorCurso;

        public CursoController(ArmazenadorCurso armazenadorCurso)
        {
            _armazenadorCurso = armazenadorCurso;
        }

        public IActionResult Index()
        {
            var cursos = new List<CursoDto>();

            return View("Index", PaginatedList<CursoDto>.Create(cursos, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorCurso.Armazenar(model);

            return Ok();
        }
    }
}