using Banca.Core.Dtos;
using Banca.Maui.Services;

namespace Banca.Maui.Pages;

public partial class AhorroDetallePage : TabbedPage
{
    private readonly Servicio _servicio;
    AhorroDto Ahorro;

    public AhorroDetallePage(Servicio servicio, AhorroDto ahorro)
	{
		InitializeComponent();
        this._servicio = servicio;
        Ahorro = ahorro;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        ActivityIndicator.IsVisible = true;
        Ahorro = await _servicio.Ahorro.ObtenerPorIdAsync(Ahorro.Id);
        BindingContext = Ahorro;
        ActivityIndicator.IsVisible = false;
    }

}