using App3.Paginas;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App3
{
    public partial class App : Application
    {
        public App(String filename)
        {
            InitializeComponent();
            UserRepositorio.Inicializador(filename);
            //MainPage = new  PagInicio();
            MainPage = new NavigationPage(new LoginUser());

        }

        protected async override void  OnStart()
        {
            try
            {
                var res = await EstablecerConexionServicio();
            }
            catch (Exception)
            {
            }
            // Handle when your app starts
            //if (res != "")
            //{
            //    DisplayAlert("", x.ToString(), "OK");
            //}
        }

        protected  override void  OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public async Task<string> EstablecerConexionServicio()
        {
            string res = "";
            try
            {
                Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
                await Task.Run(() => { service.ConsultarCliente(""); });
            }
            catch (Exception)
            {
                res = "No se ha podido establecer conexión";
            }
            return res;
        }
    }
}
