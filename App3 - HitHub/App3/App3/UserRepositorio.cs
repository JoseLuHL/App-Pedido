using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using App3.Tablas;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace App3
{
    public class UserRepositorio
    {
        private SQLiteConnection con;

        private static UserRepositorio instancia;

        public static UserRepositorio Instancia
        {
            get
            {
                if (instancia == null)
                    throw new Exception("Debe llamar al inicializador");
                return instancia;
            }
        }

        public static void Inicializador(String filename)
        {
            if (filename == null)
                throw new ArgumentException();

            if (instancia != null)
                instancia.con.Close();
            instancia = new  UserRepositorio(filename);
        }

        private UserRepositorio(String dbpath)
        {
            con = new SQLiteConnection(dbpath);
            con.CreateTable<User>();
            con.CreateTable<Tabla_Cliente>();
            con.CreateTable<Tabla_ListaPrecio>();
            con.CreateTable<Tabla_ListaPrecioDetalle>();
            con.CreateTable<Tabla_Pedido>();
            con.CreateTable<Tabla_PedidoDetalle>();
            con.CreateTable<Tabla_Producto>();
        }

        public string EstadoMensaje;

        public int AddNewUser(string username, string emal, string password)
        {
            int resultado=0;
            try
            {
                resultado = con.Insert(new User() {
                    Username =username,
                    Email =emal,
                    Password =password
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddCliente(string identificacion, string nombre)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_Cliente()
                {
                    Cli_Identificacion = identificacion,
                    Cli_Nombre = nombre
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddListaPrecio(int codigo, string descripcion)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_ListaPrecio()
                {
                    list_Codigo = codigo,
                    List_Descripcion = descripcion
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddPedido(int numero, string cliente, DateTime fecha, int valor)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_Pedido()
                {
                    Ped_Numero = numero,
                    Ped_IdCliente = cliente,
                    Ped_Fecha = fecha,
                    Ped_Valor = valor
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddListaPrechoDetalle(int CodLista, string CodProducto, int precio)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_ListaPrecioDetalle()
                {
                    ListDet_CodLista = CodLista,
                    ListDet_CodProducto = CodProducto,
                    ListDet_Precio = precio
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddPedidoDetalle(int Numero, int NunPedido, string CodProducto, int precio, int cantidad)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_PedidoDetalle()
                {
                    PedDet_Numero = Numero,
                    PedDet_NunPedido = NunPedido,
                    PedDet_CodProducto= CodProducto,
                    PedDet_Cantidad=cantidad,
                    PedDet_Precio=precio
                    
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public int AddProducto(string codigo, string descripcion)
        {
            int resultado = 0;
            try
            {
                resultado = con.Insert(new Tabla_Producto()
                {
                    Prod_Codigo = codigo,
                    Prod_Descripcion = descripcion
                });
                EstadoMensaje = string.Format("Cantidad de filas afectadas: {0}", resultado);
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return resultado;
        }

        public IEnumerable<User> GetAlUsers()
        {
            try
            {
                return con.Table<User>();
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<User>();
        }

        public IEnumerable<Tabla_Producto>ObtenerProductos()
        {
            try
            {
                List<string> lista = new List<string>();
                return con.Table<Tabla_Producto>();

            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<Tabla_Producto>();
        }

        public IEnumerable<Tabla_Cliente> ObtenerCustomers()
        {
            try
            {
                return con.Table<Tabla_Cliente>();
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<Tabla_Cliente>();
        }

        public IEnumerable<Tabla_ListaPrecio> ObtenerListaPrecio()
        {
            try
            {
                return con.Table<Tabla_ListaPrecio>();
            }
            catch (Exception e)
            {
                EstadoMensaje = e.Message;
            }
            return Enumerable.Empty<Tabla_ListaPrecio>();
        }

        public List<Tabla_Producto> ObtenerTareaId(string id)
        {
            List<Tabla_Producto> resultado = null;
            if (con != null)
            {
                    try
                    {
                    IEnumerable<Tabla_Producto> tareasFiltradas = con.Table<Tabla_Producto>();
                        resultado = new List<Tabla_Producto>(tareasFiltradas);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            return resultado;
        }

        public List<Tabla_Producto> ObtenetProductos()
        {
            var items = new List<Tabla_Producto>();
            try
            {
                Task.Run(() =>
                {
                    items = con.Table<Tabla_Producto>().ToList();
                });
            }
            catch (Exception e)
            {
                EstadoMensaje = "Clase: " + e.ToString();
            }
            return items;
        }

        public List<Tabla_Producto> ObtenetPedidos()
        {
            var items = new List<Tabla_Producto>();
            try
            {
                Task.Run(() =>
                {
                    items = con.Table<Tabla_Producto>().ToList();
                });
            }
            catch (Exception e)
            {
                EstadoMensaje = "Clase: " + e.ToString();
            }
            return items;
        }

        public List<Tabla_Producto> ObtenetPedidosDetalle()
        {
            var items = new List<Tabla_Producto>();
            try
            {
                Task.Run(() =>
                {
                    items = con.Table<Tabla_Producto>().ToList();
                });
            }
            catch (Exception e)
            {
                EstadoMensaje = "Clase: " + e.ToString();
            }
            return items;
        }

    }
}
