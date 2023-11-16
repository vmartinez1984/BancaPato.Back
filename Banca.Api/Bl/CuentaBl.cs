using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Dtos;
using Banca.Comun.Dtos;
using Banco.Repositorios.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banca.BusinessLayer.Bl
{
    public class UnitOfWork
    {
        public UnitOfWork(
            CuentaBl cuentaBl,
            TransaccionBl transaccionBl,
            HistorialBl historialBl,
            CategoriaBl categoriaBl,
            SubcategoriaBl subcategoriaBl,
            VersionBl versionBl,
            PresupuestoBl presupuestoBl,
            TipoDeCuentaBl tipoDeCuentaBl
        )
        {
            Categoria = categoriaBl;
            Cuenta = cuentaBl;
            Transaccion = transaccionBl;
            Historial = historialBl;
            Subcategoria = subcategoriaBl;
            Version = versionBl;
            Presupuesto = presupuestoBl;
            TipoDeCuenta = tipoDeCuentaBl;
        }

        public CuentaBl Cuenta { get; }
        public TransaccionBl Transaccion { get; }
        public HistorialBl Historial { get; internal set; }
        public SubcategoriaBl Subcategoria { get; internal set; }
        public CategoriaBl Categoria { get;  set; }
        public VersionBl Version { get; set; }
        public PresupuestoBl Presupuesto { get; internal set; }
        public TipoDeCuentaBl TipoDeCuenta { get; internal set; }        
    }    

    public class BaseBl
    {
        public readonly DuckBankContext _repositorio;
        public readonly IMapper _mapper;

        public BaseBl(DuckBankContext context, IMapper mapper)
        {
            _repositorio = context;
            _mapper = mapper;
        }
    }

    public class CuentaBl
    {
        private readonly DuckBankContext _repositorio;
        private readonly IMapper _mapper;

        public CuentaBl(DuckBankContext context, IMapper mapper)
        {
            this._repositorio = context;
            this._mapper = mapper;
        }

        public async Task<IdDto> AgregarAsync(CuentaDtoIn cuenta)
        {
            Cuentum entity;

            if (cuenta.Guid == null)
                cuenta.Guid = Guid.NewGuid();
            entity = _mapper.Map<Cuentum>(cuenta);
            _repositorio.Cuenta.Add(entity);
            await _repositorio.SaveChangesAsync();

            return new IdDto { Id = entity.Id, Guid = entity.Guid };
        }

        public async Task<List<CuentaDto>> Obtener()
        {
            List<CuentaDto> dtos;
            List<Cuentum> entidades;

            entidades = await _repositorio.Cuenta.ToListAsync();
            dtos = _mapper.Map<List<CuentaDto>>(entidades);

            return dtos;
        }
    }
}
