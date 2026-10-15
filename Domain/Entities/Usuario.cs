using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int PkUsuario { get; set; }

        public string Nombre { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }

        public Rol? Roles { get; set; }
    }
}