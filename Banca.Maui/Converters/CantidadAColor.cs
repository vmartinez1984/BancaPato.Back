using Banca.Core.Dtos;
using System.Globalization;

namespace Banca.Maui.Converters
{
    internal class EstadoAColor : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string valor = value.ToString();
            Color estado;
            switch (valor)
            {
                case EstadoDelPresupuesto.SinProcesar:
                    estado = Colors.DarkGray;
                    break;

                case EstadoDelPresupuesto.Ok:
                    estado = Colors.ForestGreen;
                    break;
                case EstadoDelPresupuesto.Danger:
                    estado = Colors.DarkRed;
                    break;
                case EstadoDelPresupuesto.Warning:
                    estado = Colors.OrangeRed;
                    break;
                default:
                    estado = Colors.GreenYellow;
                    break;
            }

            return estado;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
