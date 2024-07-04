using AutoMapper;
using Banca.Api.Bl;
using Banca.Api.Dtos;
using Banca.Api.Interfaces;
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

        public readonly IGastosRepository _repositorioMongo;
        
        public BaseBl(DuckBankContext context, IMapper mapper, IGastosRepository repository)
        {
            _repositorio = context;
            _mapper = mapper;
            _repositorioMongo = repository;
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
            CuentaDto ahorroDto;

            ahorro = await _repositorio
                .Cuenta
                .Include(x => x.TipoDeCuenta)
                .Include(x => x.Transaccions)
                .Where(x => x.Id == ObtenerAhorroId(ahorroId))
                .FirstOrDefaultAsync();

            ahorroDto = _mapper.Map<CuentaDto>(ahorro);
            ahorroDto.Calculos = ObtenerCalculos(ahorro);

            return ahorroDto;
        }

        private List<Calculo> ObtenerCalculos(Cuentum ahorro)
        {
            List<Calculo> calculos;
            int numeroDeDias;
            DateTime fechaInicial;
            DateTime fechaActual;
            decimal cantidad;

            if (ahorro.Transaccions.Count > 0)
                fechaInicial = ahorro.Transaccions.OrderBy(x => x.FechaDeRegistro).FirstOrDefault().FechaDeRegistro;
            else
                fechaInicial = (DateTime)ahorro.FechaInicial;
            if (ahorro.FechaFinal == null)
                fechaActual = DateTime.Now;
            else
                fechaActual = (DateTime)ahorro.FechaFinal;
            numeroDeDias = (fechaActual - fechaInicial).Days + 2;
            calculos = new List<Calculo>();

            if (ahorro.Transaccions.Count > 0)
                cantidad = ahorro.Transaccions[0].Cantidad;
            else
                cantidad = 0;
            for (int i = 1; i < numeroDeDias; i++)
            {
                decimal interesDelDia;
                decimal interesCalculado;
                decimal transaccion;

                interesDelDia = ObtenerInteresDelDia(ahorro);
                interesCalculado = Math.Round((cantidad * interesDelDia / 100 / 365), 2);
                transaccion = ObtenerTransaccion(ahorro, fechaInicial.AddDays(i));
                calculos.Add(new Calculo
                {
                    Subtotal = cantidad,
                    InteresCalculado = interesCalculado,
                    Total = cantidad + interesCalculado + transaccion,
                    Transaccion = transaccion,
                    Fecha = fechaInicial.AddDays(i),
                });
                cantidad = calculos[i - 1].Total;
            }

            return calculos;
        }

        private decimal ObtenerTransaccion(Cuentum ahorro, DateTime fecha)
        {
            decimal movimiento;
            decimal retiros;
            decimal depositos;

            retiros = ahorro.Transaccions.Where(x => x.Tipo == Tipo.Retiro && x.FechaDeRegistro.Date == fecha.Date).Sum(x => x.Cantidad);
            depositos = ahorro.Transaccions.Where(x => x.Tipo == Tipo.Deposito && x.FechaDeRegistro.Date == fecha.Date).Sum(x => x.Cantidad);
            movimiento = depositos - retiros;

            return movimiento;
        }

        private decimal ObtenerInteresDelDia(Cuentum ahorro)
        {
            return (decimal)(ahorro.Interes);
        }

        private int ObtenerAhorroId(string ahorroId)
        {
            return int.Parse(ahorroId);
        }
    }
}
;