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
            TipoDeCuentaBl tipoDeCuentaBl,
            PeriodoBl periodoBl,
            MovimientoBl movimientoBl
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
            Periodo = periodoBl;
            Movimiento = movimientoBl;
        }

        public CuentaBl Cuenta { get; }
        public TransaccionBl Transaccion { get; }
        public HistorialBl Historial { get; internal set; }
        public SubcategoriaBl Subcategoria { get; internal set; }
        public CategoriaBl Categoria { get; set; }
        public VersionBl Version { get; set; }
        public PresupuestoBl Presupuesto { get; internal set; }
        public TipoDeCuentaBl TipoDeCuenta { get; internal set; }
        public PeriodoBl Periodo { get; internal set; }
        public MovimientoBl Movimiento { get; internal set; }
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

            entidades = await _repositorio.Cuenta
                .Include(x => x.TipoDeCuenta)
                .Where(x => x.EstaActivo)
                .ToListAsync();
            dtos = _mapper.Map<List<CuentaDto>>(entidades);

            return dtos;
        }

        internal async Task ActualizarAsync(string ahorroId, CuentaDtoIn ahorro)
        {
            Cuentum ahorroEntity;

            ahorroEntity = await _repositorio.Cuenta.FindAsync(ObtenerAhorroId(ahorroId));
            ahorro.Guid = ahorroEntity.Guid;
            ahorroEntity = _mapper.Map(ahorro, ahorroEntity);
            _repositorio.Cuenta.Update(ahorroEntity);

            await _repositorio.SaveChangesAsync();
        }

        internal async Task BorrarAsync(string ahorroId)
        {
            Cuentum ahorro;

            ahorro = await _repositorio.Cuenta.Where(x => x.Id == ObtenerAhorroId(ahorroId)).FirstOrDefaultAsync();
            ahorro.EstaActivo = false;
            _repositorio.Cuenta.Update(ahorro);

            await _repositorio.SaveChangesAsync();
        }

        internal async Task<CuentaDto> ObtenerAsync(string ahorroId)
        {
            Cuentum ahorro;

            ahorro = await _repositorio
                .Cuenta
                .Include(x => x.TipoDeCuenta)
                .Where(x => x.Id == ObtenerAhorroId(ahorroId))
                .FirstOrDefaultAsync();

            return _mapper.Map<CuentaDto>(ahorro);
        }

        private int ObtenerAhorroId(string ahorroId)
        {
            return int.Parse(ahorroId);
        }
    }
}
