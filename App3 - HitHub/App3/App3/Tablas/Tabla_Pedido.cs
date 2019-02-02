using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("pedido")]
   public class Tabla_Pedido
    {
        [PrimaryKey, AutoIncrement]
        public int Ped_Numero { get; set; }
        [MaxLength(100)]        
        public string Ped_IdCliente { get; set; }
        [MaxLength(100)]
        public DateTime Ped_Fecha { get; set; }
        [MaxLength(100)]
        public int Ped_Valor { get; set; }
        [MaxLength(100)]
        public string Ped_Estado { get; set; }
    }
}
