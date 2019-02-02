using App3.Tablas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App3
{
  public  class ClsCargar
    {
        //private class VarClase = private new class();
        public List<ClsPropiedades> GetDafaults()
        {
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            tabla = service.TablaReistros();
            List<ClsPropiedades> templist = new List<ClsPropiedades>();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                templist.Add(new ClsPropiedades
                {
                    NombreCompletoPro = tabla.Rows[i]["Pac_Nombre1"].ToString() + " " + tabla.Rows[i]["Pac_Apellido1"].ToString(),
                    DocumentoPro = tabla.Rows[i][1].ToString(),
                });
            }
            tabla.Dispose();
            return templist;
        }

        //private class VarClase = private new class();
        public String ListProduct()
        {
            string EstadoMensaje = "Product: ";
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            //tabla = service.AllProduct();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                String codigo = tabla.Rows[i]["Prod_Codigo"].ToString();
                String descripcion = tabla.Rows[i]["Prod_Descripcion"].ToString();

                UserRepositorio.Instancia.AddProducto(codigo, descripcion);
                EstadoMensaje = EstadoMensaje + " - " + UserRepositorio.Instancia.EstadoMensaje;
            }
            tabla.Dispose();
            return EstadoMensaje;
        }

        //private class VarClase = private new class();
        public String ListCustomers()
        {
            string EstadoMensaje = "Customers: ";
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            //tabla = service.AllCustomers();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                String identificacion = tabla.Rows[i]["Cli_Identificacion"].ToString();
                String nombrecompleto = tabla.Rows[i]["NombreCompleto"].ToString();

                UserRepositorio.Instancia.AddCliente(identificacion, identificacion);
                EstadoMensaje = EstadoMensaje + " - " + UserRepositorio.Instancia.EstadoMensaje;
            }
            tabla.Dispose();
            return EstadoMensaje;
        }

        public  string ListPrece()
        {
            string EstadoMensaje = "Lista_Prece: ";
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            //tabla = service.AllListPrece();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                int Codigo = Convert.ToInt32(tabla.Rows[i]["List_Codigo"]);
                String Descripcion = tabla.Rows[i]["List_Descripcion"].ToString();

                UserRepositorio.Instancia.AddListaPrecio(Codigo, Descripcion);
                EstadoMensaje = EstadoMensaje + " - " + UserRepositorio.Instancia.EstadoMensaje;
            }
            tabla.Dispose();
            return EstadoMensaje;
        }

        public string CargarPedido()
        {
            string EstadoMensaje = "Lista_Prece: ";
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            //tabla = service.AllListPrece();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                int Codigo = Convert.ToInt32(tabla.Rows[i]["List_Codigo"]);
                String Descripcion = tabla.Rows[i]["List_Descripcion"].ToString();

                UserRepositorio.Instancia.AddListaPrecio(Codigo, Descripcion);
                EstadoMensaje = EstadoMensaje + " - " + UserRepositorio.Instancia.EstadoMensaje;
            }
            tabla.Dispose();
            return EstadoMensaje;
        }

        public List<Tabla_Pedido> CargarPedidoCliente(string identificacion)
        {
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            tabla = service.ConsultarMultiple(identificacion,"pendiente");
            List<Tabla_Pedido> templist = new List<Tabla_Pedido>();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                string cliente = tabla.Rows[i]["Cli_Identificacion"].ToString() ;
                int NumPedido = Convert.ToInt32( tabla.Rows[i]["Ped_Numero"]);
                DateTime fecha = Convert.ToDateTime( tabla.Rows[i]["Ped_Fecha"]);
                string valor = tabla.Rows[i]["Ped_Valor"].ToString();
                string estado = tabla.Rows[i]["Esta_Descripcion"].ToString();

                templist.Add(new Tabla_Pedido
                {
                    Ped_Numero = NumPedido,
                    Ped_IdCliente = cliente,
                    Ped_Fecha = fecha,
                    Ped_Valor = Convert.ToInt32( valor),
                    Ped_Estado = estado
                });
            }
            tabla.Dispose();
            return templist;
        }

        public List<ClsPropiedadesPedido> CargarPedidoDetalle(string NumeroPedido)
        {
            DataTable tabla = new DataTable();
            Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
            tabla = service.ConsultarMultiple(NumeroPedido, "pedido");
            List<ClsPropiedadesPedido> templist = new List<ClsPropiedadesPedido>();

            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                string cliente = tabla.Rows[i]["Prod_Descripcion"].ToString();
                string NumPedido = tabla.Rows[i]["Prod_Codigo"].ToString();
                string fecha = tabla.Rows[i]["PedDet_Precio"].ToString();
                string valor = tabla.Rows[i]["PedDet_Cantidad"].ToString();

                templist.Add(new ClsPropiedadesPedido
                {
                    Codigo = NumPedido,
                    Descripcion = cliente,
                    VarDetalle = Convert.ToInt32( fecha),
                    Cantidad = Convert.ToInt32(valor),
                    
                });
            }
            tabla.Dispose();
            return templist;
        }

        //public string ()
        //{
        //    string EstadoMensaje = "Lista_Prece: ";
        //    DataTable tabla = new DataTable();
        //    Droid.ServicioWeb.Service1 service = new Droid.ServicioWeb.Service1();
        //    tabla = service.ConsultarMultiple("55555","pendiente");
        //    for (int i = 0; i < tabla.Rows.Count; i++)
        //    {
        //        int Codigo = Convert.ToInt32(tabla.Rows[i]["List_Codigo"]);
        //        String Descripcion = tabla.Rows[i]["List_Descripcion"].ToString();

        //        UserRepositorio.Instancia.AddListaPrecio(Codigo, Descripcion);
        //        EstadoMensaje = EstadoMensaje + " - " + UserRepositorio.Instancia.EstadoMensaje;
        //    }
        //    tabla.Dispose();
        //    return EstadoMensaje;
        //}

    }
}
