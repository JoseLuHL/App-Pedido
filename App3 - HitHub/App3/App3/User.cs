using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3
{
    [Table("user")]
   public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100),Unique]
        public string Username { get; set; }
        [MaxLength(100),Unique]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
