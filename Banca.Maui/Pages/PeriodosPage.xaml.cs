using Banca.Core.Dtos;
using Banca.Maui.Services;

namespace Banca.Maui.Pages;

public partial class PeriodosPage : ContentPage
{
    private readonly Servicio _servicio;

    public PeriodosPage(Servicio servicio)
    {
        InitializeComponent();
        this._servicio = servicio;
    }

    /// <summary>
    /// Cuando est� a punto de aparecer la pantala
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Iniciar la animaci�n del ActivityIndicator
        //this.ActivityIndicator.IsVisible = true;

        var lista = await _servicio.Periodo.ObtenerTodosAsync();
        CollectionView.ItemsSource = lista.OrderByDescending(x => x.Id);
        //this.CollectionView.ItemsSource = Lista;

        // Detener la animaci�n del ActivityIndicator        
        //this.ActivityIndicator.IsVisible = false;
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        PeriodoDto periodo = (PeriodoDto)e.CurrentSelection.FirstOrDefault();

        Navigation.PushAsync(new PeriodoDetallePage(_servicio, periodo));
    }
}