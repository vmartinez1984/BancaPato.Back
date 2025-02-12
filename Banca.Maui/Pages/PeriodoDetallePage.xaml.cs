using Banca.Core.Dtos;
using Banca.Maui.Services;
using CommunityToolkit.Maui.Alerts;

namespace Banca.Maui.Pages;

public partial class PeriodoDetallePage : ContentPage
{
    private readonly Servicio _servicio;
    private PeriodoDto _periodo { get; set; }
    public string EncodedKey { get; set; }
    public bool EsNuevoEncodedKey { get; set; } = false;

    public PeriodoDetallePage(Servicio _servicio, PeriodoDto periodo)
    {
        InitializeComponent();
        this._servicio = _servicio;
        this._periodo = periodo;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ActivityIndicator.IsVisible = true;
        PeriodoDto periodo = await _servicio.Periodo.ObtenerPorId(_periodo.Id);
        BindingContext = periodo;
        CollectionView.ItemsSource = periodo.Version.Presupuestos;
        _periodo = periodo;
        ActivityIndicator.IsVisible = false;
        Picker.SelectedIndex = 0;
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PresupuestoDto presupuesto;

        presupuesto = (PresupuestoDto)e.CurrentSelection.FirstOrDefault();
        if (!EsNuevoEncodedKey)
        {
            EncodedKey = Guid.NewGuid().ToString();
            EsNuevoEncodedKey = true;
        }
        var cantidad = await DisplayPromptAsync(
            $"Presupuesto {presupuesto.Cantidad.ToString("c")}",
            $"{presupuesto.Subcategoria.Nombre}",
            initialValue: presupuesto.Cantidad.ToString(),
            keyboard: Keyboard.Numeric
        );

        if (cantidad != null)
        {
            try
            {
                ActivityIndicator.IsVisible = true;
                CollectionView.IsEnabled = false;
                _ = Toast.Make("Un momento por favor").Show();
                await _servicio.Periodo.AgregarMovimientoAsync(_periodo.Id, new MovimientoDto
                {
                    Cantidad = Convert.ToDecimal(cantidad),
                    Guid = EncodedKey,
                    PresupuestoId = presupuesto.Id
                });
                BindingContext = null;
                BindingContext = await _servicio.Periodo.ObtenerPorId(_periodo.Id);
                _ = Toast.Make("Datos registrado").Show();
            }
            catch (Exception)
            {
                _ = Toast.Make("Ocurrio un error, intente más tarde").Show();
            }
            finally
            {
                ActivityIndicator.IsVisible = false;
                EsNuevoEncodedKey = false;
                CollectionView.IsEnabled = true;
            }
        }

    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string estado = (sender as Picker).SelectedItem.ToString();

        CollectionView.ItemsSource = null;
        if (estado.ToLower() == "todos")
            CollectionView.ItemsSource = _periodo.Version.Presupuestos;
        else
        {
            var lista = _periodo.Version.Presupuestos.Where(x => x.Estado == estado).ToList();
            CollectionView.ItemsSource = lista;
        }
    }
}