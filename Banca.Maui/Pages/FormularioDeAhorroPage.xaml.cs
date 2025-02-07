using Banca.Core.Dtos;
using Banca.Maui.Services;

namespace Banca.Maui.Pages;

public partial class FormularioDeAhorroPage : ContentPage
{
    private readonly Servicio _servicio;

    public FormularioDeAhorroPage(Servicio servicio)
	{
		InitializeComponent();
        _servicio = servicio;
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //ActivityIndicator.IsVisible = true;
        var lista = await _servicio.TipoDeCuenta.ObtenerTodosAsync();
        lista.Insert(0, new Core.Dtos.TipoDeCuentaDto { Id = 0, Nombre = "Seleccione" });
        Picker.ItemsSource = lista;
        Picker.SelectedIndex = 0;
        //ActivityIndicator.IsVisible = false;
    }
    private void ButtonCancelar_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private async void ButtonGuardar_Clicked(object sender, EventArgs e)
    {
        AhorroDtoIn ahorro;

        ahorro = new AhorroDtoIn
        {
            Guid = Guid.NewGuid().ToString(),            
            Interes = 0,
            Nombre = EntryNombre.Text,
            Nota = EntryNota.Text,
            TipoDeCuentaId = (Picker.SelectedItem as TipoDeCuentaDto).Id
        };

        await _servicio.Ahorro.AgregarAsync(ahorro);
    }
}