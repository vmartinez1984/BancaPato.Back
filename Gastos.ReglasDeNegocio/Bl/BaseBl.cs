using DuckBank.Persistence.Interfaces;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class BaseBl
    {
        public readonly IRepositorio _repositorio;

        public BaseBl(IRepositorio repository)
        {            
            _repositorio = repository;
        }
    }
}
