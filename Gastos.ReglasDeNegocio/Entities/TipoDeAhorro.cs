namespace Gastos.ReglasDeNegocio.Entities
{
    internal class TipoDeAhorro : EntidadBase
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }        
    }
}
