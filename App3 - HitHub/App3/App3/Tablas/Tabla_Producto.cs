using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("producto")]
    public class Tabla_Producto
    {
        [PrimaryKey,MaxLength(10)]
        public string Prod_Codigo { get; set; }
        [MaxLength(100)]
        public string Prod_Descripcion { get; set; }
    }
}
