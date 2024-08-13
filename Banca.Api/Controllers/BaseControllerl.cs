using Banca.Api.Bl;
using Microsoft.AspNetCore.Mvc;

namespace Banca.Api.Controllers
{
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
