using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using App3.Tablas;

namespace App3
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PagPedido : ContentPage
	{
		public  PagPedido ()
		{
            var Productos = UserRepositorio.Instancia.ObtenerProductos();
            //PicProducto.SelectedItem = Productos[0].Prod_Descripcion;
            //var x =  CargarCombo();
            InitializeComponent ();
		}

        public void CalcularSobTotal()
        {
            int total = 0;
            for (int i = 0; i < templist.Count; i++)
            {
                total = total + templist[i].VarDetalle;
            }
            TxtSubTotal.Text = total.ToString("N0");
        }
        //int x = 0;

        List<ClsPropiedadesPedido> templist = new List<ClsPropiedadesPedido>();

        private void BtnAgregar_Clicked(object sender, EventArgs e)
        {
            if (ClienteEncontrado)
            {
                try
                {
                    if (Convert.ToInt32(TxtCantidad.Text.Trim()) > 0)
                    {
                        if (TaProductos.Rows.Count > 0)
                        {
                            string descripcion = TaProductos.Rows[0]["Prod_Descripcion"].ToString();
                            string codigo = TaProductos.Rows[0]["ListDet_CodProducto"].ToString();
                            int cantidadP = Convert.ToInt32(TaProductos.Rows[0]["Prod_Existencia"].ToString());
                            int cantidadVenta = Convert.ToInt32(TxtCantidad.Text.Trim());
                            int vardetale = Convert.ToInt32(TaProductos.Rows[0]["ListDet_Precio"].ToString()) * cantidadVenta;

                            if (cantidadVenta <= cantidadP)
                            {
                                templist.Add(new ClsPropiedadesPedido
                                {
                                    Descripcion = descripcion,
                                    Codigo = codigo,
                                    Cantidad = cantidadVenta,
                                    VarDetalle = vardetale
                                });
                                ListaEst.ItemsSource = null;
                                ListaEst.ItemsSource = templist;
                                CalcularSobTotal();
                            }
                            else
                            {
                                DisplayAlert("Alerta!", "No disponemos de esta cantidad: " + TxtCantidad.Text, "OK");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Error!", "Verificar Cantidad ", "OK");
                }
            }
            else
                DisplayAlert("Error!", "Cliente no encontrado ", "OK");

        }

        bool ClienteEncontrado=false;

        private async void TxtIdentificacion_Completed(object sender, EventArgs e)
        {
                ActvConsultarCliente.IsRunning = true;
            string respuesta = await respuestaCliente();
            if (respuesta!="")
            {
                LblNombreCliente.Text = respuesta;
                 var x = await CargarCombo();
                var msm = DisplayAlert("Exitoso!", "Cliente: " + respuesta + " " + x, "OK");
                ClienteEncontrado = true;
                ImgCliente.IsVisible = true;
            }
            else
            {
                ImgCliente.IsVisible = false;
                ClienteEncontrado = false;
               var msm = DisplayAlert("Fallo!", "Cliente no encntrado" + respuesta, "OK");
            }
            ActvConsultarCliente.IsRunning = false;
        }

        public async Task<string> respuestaCliente()
        {
            string respuesta = "";
            try
            {
                Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
                await Task.Run(() => { respuesta = service.ConsultarCliente(TxtIdentificacion.Text.Trim());});
            }
            catch (Exception )
            {
                respuesta = "";
            }
            return respuesta;
        }

        List<string> Codigo = new List<string>();

        public async Task<string> ListarCombo()
        {
            string respuesta = "";
            try
            {
                List<Tabla_Producto> datos = new List<Tabla_Producto>();
                respuesta = datos.Count<Tabla_Producto>().ToString();
                await Task.Run(() => { datos = UserRepositorio.Instancia.ObtenetProductos(); });
                datos = UserRepositorio.Instancia.ObtenetProductos();
                List<string> Descripcion = new List<string>();
                Codigo = new List<string>();

                for (int i = 0; i < datos.Count<Tabla_Producto>(); i++)
                {
                    Descripcion.Add(datos[i].Prod_Descripcion);
                    Codigo.Add(datos[i].Prod_Codigo);
                }
                PicProducto.ItemsSource = Descripcion;
                PicProducto.SelectedItem = Codigo;
               
            }
            catch (Exception ex)
            {
                respuesta = respuesta + ex.ToString();
                respuesta = respuesta + ex.Message;
            }
            return respuesta;
        }

        public async Task<string> CargarCombo()
        {
            string respuesta = "";
            try
            {
                DataTable TaProductos = new DataTable();
                Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
                await Task.Run(() => { TaProductos = service.ConsultarProductos(); });
                List<string> Descripcion = new List<string>();
                Codigo = new List<string>();
                for (int i = 0; i < TaProductos.Rows.Count; i++)
                {
                    Descripcion.Add(TaProductos.Rows[i]["Prod_Descripcion"].ToString());
                    Codigo.Add(TaProductos.Rows[i]["Prod_Codigo"].ToString());
                }
                PicProducto.ItemsSource = Descripcion;
                PicProducto.SelectedItem = Codigo;
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }

        DataTable TaProductos = new DataTable();

        public async Task<string> AgregarProductoLista(string CodProducto,string Identificacion)
        {
            string respuesta = "";
            try
            {
                TaProductos = new DataTable();
                Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
                
                await Task.Run(() => { TaProductos = service.ConsultarProductosListaPrecio(CodProducto,Identificacion); });
                string descripcion = TaProductos.Rows[0]["Prod_Descripcion"].ToString();
                int vardetale = Convert.ToInt32(TaProductos.Rows[0]["ListDet_Precio"].ToString());
                LblVrDetalle.Text = vardetale.ToString("N0");
                LblDescripcionProducto.Text = descripcion;
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }

        private async void PicProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ClienteEncontrado==true)
            {
                if (PicProducto.SelectedIndex > -1)
                {
                    int indice = PicProducto.SelectedIndex;
                    string codigoProducto = Codigo[PicProducto.SelectedIndex].ToString();
                    string respuesta = await AgregarProductoLista(codigoProducto, TxtIdentificacion.Text);                    
                    TxtCantidad.Focus();
                    TxtCantidad.Text = "1";

                    if (respuesta!="")
                        await DisplayAlert("Fallo!", "Lo sentimos conexión fallida." + "\r\n" + "Vuelva a intentarlo ", "OK");
                }
            }
            else
            {
              var msm = DisplayAlert("Fallo!", "No hay ningun cliente", "OK");
            }
        }

        private async void ListaEst_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var result = await DisplayAlert("Eliminar", "¿Dese eliminar el elemento?", "OK", "Cancel");
            if (result==true)
            {
                var elemento = e.SelectedItem as ClsPropiedadesPedido;
                try
                {
                    if (elemento!=null)
                    {
                        for (int i = 0; i < templist.Count; i++)
                        {
                            if (templist[i].Codigo == elemento.Codigo)
                            {
                                templist.RemoveAt(i);
                                ListaEst.ItemsSource = null;
                                ListaEst.ItemsSource = templist;
                                CalcularSobTotal();
                                break;
                            }
                        }
                    }                    
                }
                catch (Exception ex)
                {
                  var x =  DisplayAlert("Evento!", ex.ToString(), "OK");
                }
            }
        }
        
        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (ClienteEncontrado)
            {
                if (templist.Count>0)
            {
                
                var result = await DisplayAlert("Confirmar", "¿Dese realizar el pedido?", "OK", "Cancel");
                if (result == true)
                {
                    ActvGuardar.IsRunning = true;
                    var respuesta = await InsertarPedido();
                    var x = DisplayAlert("Evento!", "Pedido realizado correctamente", "OK");
                    LimpiarTodo();
                }
                ActvGuardar.IsRunning = false;
            }
            else
             await DisplayAlert("Evento!", "Faltan datos para cuntinuar", "OK");
            }
            else
                await DisplayAlert("Error!", "Cliente no encontrado ", "OK");
        }

        public  void LimpiarTodo()
        {
            //ImgCliente.IsVisible = false;
            templist.Clear();
            ListaEst.ItemsSource = null;
            LblDescripcionProducto.Text = "";
            LblNombreCliente.Text = "";
            LblVrDetalle.Text = "";
            TxtCantidad.Text = "";
            TxtIdentificacion.Text = "";
            TxtSubTotal.Text = "0";
            //PicProducto.
            //var x = await CargarCombo();
            //PicProducto.Title = "Seleccionar producto";
            //PicProducto.
        }

        public async Task<string> InsertarPedido()
        {
            int NumeroItem = templist.Count;
            string[] codproducto = new string[NumeroItem];
            int[] cantidad = new int[NumeroItem];
            int[] vardetalle = new int[NumeroItem];

            for (int i = 0; i < templist.Count; i++)
            {
                codproducto[i] = templist[i].Codigo;
                cantidad[i] = templist[i].Cantidad;
                vardetalle[i] = templist[i].VarDetalle;
            }
            
            List<int> lista3 = new List<int>();
            string respuesta="";
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            //service.InsertarPedido()
            await Task.Run(() => { respuesta = service.InsertarPedido(TxtIdentificacion.Text, Convert.ToDouble(TxtSubTotal.Text), true, codproducto, cantidad, vardetalle); });
            return respuesta;
        }

        private async void TxtLimpiar_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Confirmar", "¿Dese limpiar?", "OK", "Cancel");
            if (result == true)
            {
                //LimpiarTodo();
                var user = UserRepositorio.Instancia.ObtenerCustomers();
                //UserRepositorio.Instancia.ObtenetProductos()
                //await DisplayAlert("", user.Count<Tabla_Cliente>().ToString(), "OK");
                
                    await DisplayAlert("",ListarCombo().Result, "OK");
                await DisplayAlert("", UserRepositorio.Instancia.ObtenetProductos().Count.ToString(), "OK");
            }
        }

        private async void OnImageNameTapped(object sender, EventArgs e)
        {
            await CargarListaPacientes();
        }

        public async Task<string> CargarPedido(string Identificacion)
        {
            string respuesta = "";
            if (TxtIdentificacion.Text!="")
            {                
                try
                {
                    templist.Clear();
                    TaProductos = new DataTable();
                    Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();

                    //await Task.Run(() => { TaProductos = service.ConsultarPdidoCliente(Identificacion); });
                    if (TaProductos.Rows.Count > -1)
                    {
                        for (int i = 0; i < TaProductos.Rows.Count; i++)
                        {
                            string codigo = TaProductos.Rows[i]["PedDet_CodProducto"].ToString();
                            string descrip = TaProductos.Rows[i]["Prod_Descripcion"].ToString();
                            int cantidad = Convert.ToInt32(TaProductos.Rows[i]["PedDet_Cantidad"]);
                            int vardetalle = Convert.ToInt32(TaProductos.Rows[i]["PedDet_VrDetalle"]);

                            templist.Add(new ClsPropiedadesPedido { Codigo = codigo, Descripcion = descrip, Cantidad = cantidad, VarDetalle = vardetalle });
                        }
                        ListaEst.ItemsSource = null;
                        ListaEst.ItemsSource = templist;

                        string nombre = TaProductos.Rows[0]["Cli_Identificacion"].ToString();
                        string vartotal = TaProductos.Rows[0]["Ped_Valor"].ToString();

                        LblNombreCliente.Text = nombre;
                        LblVrDetalle.Text = vartotal;
                    }
                    else
                      await  DisplayAlert("Error!", "Cliente no encontrado ", "OK");

                }
                catch (Exception ex)
                {
                    respuesta = ex.ToString();
                }               
            }
            else
                await DisplayAlert("Error!", "Proporcione una identificación ", "OK");
            return respuesta;
        }

        public async Task CargarListaPacientes()
        {
            int x = 0;
            ClsCargar clsCargar = new ClsCargar();
            //await DisplayAlert("", clsCargar.GetDafaults().Count.ToString(), "OK");
            ActvConsultarCliente.IsVisible = true;
            ActvConsultarCliente.IsRunning = true;
            
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            await Task.Run(() => {tabla = service.TodosPacientes(); });
            List<ClsPropiedades> templist = new List<ClsPropiedades>();
            string[] Datos = new string[tabla.Rows.Count];

            foreach (DataRow item in tabla.Rows)
            {
                Datos[x] = item["Cli_Identificacion"].ToString() + " - " + item["NombreCompleto"].ToString();
                x++;
            }

            ActvConsultarCliente.IsVisible = false;
            ActvConsultarCliente.IsRunning = false;

            var res = await DisplayActionSheet("Clientes", "Cancelar", null, Datos);
            if (res!=null)
            {
                string[] resp = res.Split('-');
                //await DisplayAlert("", resp[0], "OK");
                TxtIdentificacion.Text = resp[0];
                string respuesta = await respuestaCliente();
                if (respuesta != "")
                {
                    LblNombreCliente.Text = respuesta;
                    var xy = await CargarCombo();
                    //var msm = DisplayAlert("Exitoso!", "Cliente: " + respuesta + " " + x, "OK");
                    ClienteEncontrado = true;
                    //ImgCliente.IsVisible = true;
                }
            }
        }
    }
} 