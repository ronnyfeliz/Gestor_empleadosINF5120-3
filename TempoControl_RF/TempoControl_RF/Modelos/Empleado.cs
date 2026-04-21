using System;
using System.Collections.Generic;
using System.Text;

namespace TempoControl_RF.Modelos
{
    public class Empleado
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Departamento { get; set; }
        public string Posicion { get; set; }
        public bool Activo { get; set; }
    }
}

