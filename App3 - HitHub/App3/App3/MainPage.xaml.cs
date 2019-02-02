using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App3.Droid;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TxtContraseña.Text = "1";
            TxtUsuario.Text = "1";
        }
       
        private async void BtnEntrar2_Clicked(object sender, EventArgs e)
        {

            if (TxtContraseña.Text != string.Empty & TxtUsuario.Text != string.Empty)
            {
                ActivarCargar.IsRunning = true;
                string resp = await respuestaMetodo();
                ActivarCargar.IsRunning = false;
                if (resp != "")
                {
                  await DisplayAlert("Conectado!", resp, "OK");
                    await Navigation.PushModalAsync(new Lista());

                }
                else
                    await DisplayAlert("Fallo!", "Usuario o Contraseña incorrectos", "OK");
            }
            else
                await DisplayAlert("!!!!!", "Campos vacios", "OK");
        }
        public async Task<string> respuestaMetodo()
        {
            string respuesta="";
            try
            {
                Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
                await Task.Run(() => { respuesta = service.IniciarSesion(TxtUsuario.Text.Trim(), TxtContraseña.Text.Trim()); });                
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }
    }
}
