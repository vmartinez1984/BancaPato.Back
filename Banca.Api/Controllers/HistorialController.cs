﻿using Banca.Api.Bl;
using Banca.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class HistorialController : BancaBase
    //{
    //    public HistorialController(UnitOfWork unitOfWork) : base(unitOfWork)
    //    {
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Agregar(HistorialDtoIn historial)
    //    {
    //        IdDto id;

    //        id = await _unitOfWork.Historial.AgregarAsync(historial);

    //        return Created("", id);
    //    }

    //    [HttpGet()]
    //    public async Task<IActionResult> ObtenerTodos()
    //    {
    //        List<HistorialDto> lista;

    //        lista = await _unitOfWork.Historial.Obtener();

    //        return Ok(lista.OrderBy(x => x.FechaDeRegistro));
    //    }
    //}
}
