using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("pedidodetalle")]
   public class Tabla_PedidoDetalle
    {
        [PrimaryKey]
        public int PedDet_Numero { get; set; }
        public int PedDet_NunPedido { get; set; }
        [MaxLength(10)]
        public string PedDet_CodProducto { get; set; }
        public int PedDet_Precio { get; set; }
        public int PedDet_Cantidad { get; set; }
    }
}
