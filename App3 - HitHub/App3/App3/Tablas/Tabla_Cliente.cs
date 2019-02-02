using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.Tablas
{
    [Table("cliente")]
    public class Tabla_Cliente
    {
        [PrimaryKey, MaxLength(21)]
        public string Cli_Identificacion { get; set; }
        [MaxLength(100)]
        public string Cli_Nombre { get; set; }
    }
}
