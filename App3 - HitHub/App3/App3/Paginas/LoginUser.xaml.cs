using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUser : ContentPage
    {
        public LoginUser()
        {
            InitializeComponent();
        }

        private async void BtnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (TxtContraseña.Text != "" & TxtUsuario.Text != "")
                {
                    Activador.IsVisible = true;
                    var usuario = await IniciarSesion(TxtUsuario.Text, TxtContraseña.Text);
                    if (usuario != "")
                    {
                        //await DisplayAlert("Res",usuario,"OK");                        
                        TxtUsuario.Text=String.Empty;
                        TxtContraseña.Text = String.Empty;
                        await Navigation.PushModalAsync(new PagInicio(usuario));
                    }
                    else
                    {
                        await DisplayAlert("Res", "Usuario o contraseña incorrecto", "OK");
                    }
                    Activador.IsVisible = false;
                }
                else
                {
                    await DisplayAlert("Res", "Debe completar los campos para continuar", "OK");
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Res", "Sin conexión", "OK");
            }
           
        }

        public async Task<string> IniciarSesion(string usuario, string contraseña)
        {
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            string resp = "";
            await Task.Run(() => { resp = service.ConsultarUsuario(usuario, contraseña); });
            return resp;
        }

        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Salir", "¿Desea salir?.", "Si", "No"))
                    await Navigation.PopModalAsync();
            });
            return true;
        }
    }
}