using AutoMapper;
using Banca.Api.Interfaces;
using Banco.Repositorios.Entities;

namespace Banca.Api.Bl
{
    public class BaseBl
    {
        //public readonly DuckBankContext _repositorio;

        public readonly IMapper _mapper;

        public readonly IGastosRepository _repositorioMongo;        

        public BaseBl(DuckBankContext context, IMapper mapper, IGastosRepository repository)
        {
          //  _repositorio = context;
            _mapper = mapper;
            _repositorioMongo = repository;
        }
    }
}
