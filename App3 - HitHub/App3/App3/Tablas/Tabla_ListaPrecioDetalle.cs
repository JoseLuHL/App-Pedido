using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("listapreciodetalle")]
   public class Tabla_ListaPrecioDetalle
    {
        [PrimaryKey]
        public int ListDet_CodLista { get; set; }
        [MaxLength(10)]
        public string ListDet_CodProducto { get; set; }
        public int ListDet_Precio { get; set; }
    }
}
