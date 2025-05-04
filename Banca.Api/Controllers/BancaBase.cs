using Gastos.ReglasDeNegocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Yo merengues")]
    public class BancaBase : ControllerBase
    {
        public readonly UnitOfWork _unitOfWork;

        public BancaBase(
            UnitOfWork unitOfWork
        )
        {
            this._unitOfWork = unitOfWork;
        }
    }
}
