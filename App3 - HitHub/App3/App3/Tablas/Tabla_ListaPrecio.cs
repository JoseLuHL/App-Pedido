using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("listaprecio")]
   public class Tabla_ListaPrecio
    {
        [PrimaryKey]
        public int list_Codigo { get; set; }
        [MaxLength(100)]
        public string List_Descripcion { get; set; }        
    }
}
