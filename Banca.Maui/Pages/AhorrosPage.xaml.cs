using Banca.Core.Dtos;
using Banca.Maui.Services;

namespace Banca.Maui.Pages;

public partial class AhorrosPage : ContentPage
{
    private readonly Servicio _servicio;
    List<AhorroDto> Lista;

    public AhorrosPage(Servicio servicio)
    {
        InitializeComponent();
        this._servicio = servicio;
    }

    /// <summary>
    /// Cuando está a punto de aparecer la pantala
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Iniciar la animación del ActivityIndicator
        this.ActivityIndicator.IsVisible = true;

        Lista = await _servicio.Ahorro.ObtenerTodosAsync();

        this.CollectionView.ItemsSource = Lista;

        // Detener la animación del ActivityIndicator        
        this.ActivityIndicator.IsVisible = false;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AhorroDto ahorro;

        ahorro = e.CurrentSelection.FirstOrDefault() as AhorroDto;

        Navigation.PushAsync(new AhorroDetallePage(_servicio, ahorro));
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string filtro;

        filtro = e.NewTextValue.ToLower();
        if (string.IsNullOrEmpty(filtro)) {
            this.CollectionView.ItemsSource = Lista;
        }
        else {
            List<AhorroDto> listaFiltrada;

            listaFiltrada = Lista.Where(x => x.Nombre.ToLower().Contains(filtro)).ToList();

            this.CollectionView.ItemsSource = listaFiltrada;
        }

    }   

    private void ButtonNuevo_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new FormularioDeAhorroPage(_servicio));
    }
}