using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.dto
{
    public class RolRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public bool Estatus { get; set; } = true;
    }
}