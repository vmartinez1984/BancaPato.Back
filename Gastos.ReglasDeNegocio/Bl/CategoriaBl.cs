using Banca.Core.Dtos;
using Gastos.ReglasDeNegocio.Helpers;
using Gastos.ReglasDeNegocio.Repositories;

namespace Gastos.ReglasDeNegocio.Bl
{
    public class CategoriaBl
    {
        private readonly Repositorio _repositorio;

        public CategoriaBl(Repositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<CategoriaDto>> ObtenerAsync()
        {
            var entidades = await _repositorio.Categoria.ObtenerTodosAsync();

            return entidades.ToDtos();
        }
    }
}
