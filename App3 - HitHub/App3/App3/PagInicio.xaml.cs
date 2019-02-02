using App3.Paginas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagInicio : ContentPage
	{
		public PagInicio ()
		{
			InitializeComponent ();          

        }

        private async void BtnCargarDatos_Clicked(object sender, EventArgs e)
        {

            try
            {
                //StaCargando.IsVisible = true;
                //BtnCargarDatos.IsVisible = false;
                //var msg = await InsertarDatos();
                //StaCargando.IsVisible = false;
                //BtnCargarDatos.IsVisible = true;
                //var res = DisplayAlert("Respuesta", msg, "OK");
                await Navigation.PushModalAsync(new PagPedido());
            }

            catch (Exception )
            {
                //StaCargando.IsVisible = false;
                //BtnCargarDatos.IsVisible = true;
                //var res = DisplayAlert("Respuesta", "Lo sentimos no se ha podido cargar, Revise su conexión", "OK");
                await Navigation.PushModalAsync(new PagPedido());
                //throw new Exception();
            }
        }

        public async Task<string> InsertarDatos()
        {
                string msg = "";
                ClsCargar cargar = new ClsCargar();
                await Task.Run(() => { msg = cargar.ListCustomers();});
                await Task.Run(() => { msg = msg + " " + cargar.ListPrece(); });
                await Task.Run(() => { msg = msg + " " + cargar.ListProduct(); });
                return msg;
        }

        private async void BtnReciente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Page1());
        }

        private async void BtnEstadoCuenta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PagResumen());
        }
    }
}