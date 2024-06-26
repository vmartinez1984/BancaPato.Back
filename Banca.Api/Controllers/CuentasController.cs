﻿using Banca.Api.Dtos;
using Banca.BusinessLayer.Bl;
using Banca.Comun.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CuentasController(
            UnitOfWork unitOfWork
        )
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(CuentaDtoIn cuenta)
        {
            IdDto id;

            id = await _unitOfWork.Cuenta.AgregarAsync(cuenta);

            return Created($"Cuentas/{id.Id}", id);
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            List<CuentaDto> lista;

            lista = await _unitOfWork.Cuenta.Obtener();
            var total = lista.Sum(x => x.Balance);

            return Ok(lista);
        }

        [HttpGet("{ahorroId}")]
        public async Task<IActionResult> Obtener(string ahorroId)
        {
            CuentaDto ahorro;

            ahorro = await _unitOfWork.Cuenta.ObtenerAsync(ahorroId);

            return Ok(ahorro);
        }

        [HttpPut("{ahorroId}")]
        public async Task<IActionResult> ActualizarAhorro(string ahorroId, CuentaDtoIn ahorro)
        {

            await _unitOfWork.Cuenta.ActualizarAsync(ahorroId, ahorro);

            return Accepted();
        }

        [HttpDelete("{ahorroId}")]
        public async Task<IActionResult> Borrar(string ahorroId)
        {
            await _unitOfWork.Cuenta.BorrarAsync(ahorroId);

            return Accepted();
        }

        [HttpGet("{ahorroId}/Transacciones")]
        public async Task<IActionResult> ObtenerTransaccionesPorAhorroId(string ahorroId)
        {
            List<TransaccionDto> lista;

            lista = await _unitOfWork.Transaccion.ObtenerPorAhorroId(ahorroId);

            return Ok(lista);
        }

        [HttpPost("{cuentaIdGuid}/depositos")]
        public async Task<IActionResult> Depositar(string cuentaIdGuid, DepositoDtoIn deposito)
        {
            int id;

            id = await _unitOfWork.Transaccion.Depositar(cuentaIdGuid, deposito);

            return Created("", new { Id = id });
        }

        [HttpPost("{cuentaIdGuid}/retiros")]
        public async Task<IActionResult> Retirar(string cuentaIdGuid, RetiroDtoIn retiro)
        {
            int id;

            id = await _unitOfWork.Transaccion.Retirar(cuentaIdGuid, retiro);

            return Created("", new { Id = id });
        }
    }
}
