using AutoMapper;
using Banca.Api.Interfaces;
using DuckBank.Persistence.Interfaces;

namespace Banca.Api.Bl
{
    public class BaseBl
    {       
        public readonly IMapper _mapper;

        public readonly IGastosRepository _repositorioMongo;        

        public BaseBl(IMapper mapper, IGastosRepository repository)
        {          
            _mapper = mapper;
            _repositorioMongo = repository;
        }
    }
}
