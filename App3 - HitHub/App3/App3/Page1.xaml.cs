using App3.Tablas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace App3.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
            InitializeComponent ();
            try
            {
                Activador.IsVisible = true;
                var x = pedidos();
                Activador.IsVisible = false;
            }
            catch (Exception)
            {
            }            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //Activador.IsVisible = true;
            //var x = await pedidos();
            //Activador.IsVisible = false;
        }

        public async Task<string> pedidos()
        {
           
            string resp = "";
            try
            {
                List<Tabla_Pedido> pedidos = new List<Tabla_Pedido>();
                ClsCargar clsCargar = new ClsCargar();
                await Task.Run (() => {pedidos = clsCargar.CargarPedidoCliente("55555");});
                ListaEst.ItemsSource = null;
                LstPedido.ItemsSource = pedidos;
                resp = resp.ToString();
            }
            catch (Exception ex)
            {
                resp = ex.ToString();
            }
            return resp;
        }

        public async Task<string> pedidosDetalle(string numero)
        {
            string resp = "";
            try
            {
                List<ClsPropiedadesPedido> pedidos = new List<ClsPropiedadesPedido>();
                ClsCargar clsCargar = new ClsCargar();
                await Task.Run(() => { pedidos = clsCargar.CargarPedidoDetalle(numero); });
                ListaEst.ItemsSource = null;
                ListaEst.ItemsSource = pedidos;
                resp = resp.ToString();
            }
            catch (Exception ex)
            {
                resp = ex.ToString();
            }
            return resp;
        }

        string Estado = "";

        public void MensajeEstado (string estado)
        {
            //string mensje = "";
            LblMensaje.Text = "";
            if (estado== "PENDIENTE")
            {
                LblMensaje.Text = "SU PEDIDO SE ENCUENTRA EN PROCESO. TIEMPO ESTEIMADO: 48 A 72 HORAS";
            }
            else if (estado == "DESPACHADO")
            {
                LblMensaje.Text = "SU PEDIDO HA SIDO DESPACHADO. TIEMPO ESTEIMADO: 24 A 48 HORAS";
            }
            else
            {
                LblMensaje.Text = "SU PEDIDO HA SIDO ENTREGADO. TIEMPO ESTEIMADO: 0 A 0 HORAS";
            }
        }

        private async void LstPedido_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var elemento = e.SelectedItem as Tabla_Pedido;
            try
            {
                if (elemento != null)
                {
                    string numero = elemento.Ped_Numero.ToString();
                    Estado = elemento.Ped_Estado;
                    ActDetallePedido.IsVisible = true;
                    await pedidosDetalle(numero);
                    MensajeEstado(Estado.Trim());
                }
            }
            catch (Exception ex)
            {
                var x = DisplayAlert("Evento!", ex.ToString(), "OK");
            }
        }

        private void BtnCancelarPediso_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Mensaje","Nuestro equino no ha programado esta opción", "OK");
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application / pdf", stream);
        }
    }
}