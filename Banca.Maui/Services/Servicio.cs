namespace Banca.Maui.Services
{
    public class Servicio
    {
        public AhorroService Ahorro { get; }      

        public TipoDeCuentaService TipoDeCuenta { get; }

        public PeriodoService Periodo { get; }

        public Servicio(
            AhorroService ahorroService,            
            TipoDeCuentaService tipoDeCuentaService,
            PeriodoService periodoService
        )
        {
            Ahorro = ahorroService;            
            TipoDeCuenta = tipoDeCuentaService;
            Periodo = periodoService;
        }
    }
}
