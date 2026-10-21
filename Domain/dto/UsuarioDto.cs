using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto
{
    public class UsuarioDto
    {
        public int PkUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string NombreRol { get; set; } = string.Empty;
    }
}