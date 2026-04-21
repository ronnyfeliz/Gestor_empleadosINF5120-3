using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using TempoControl_RF.Logica;

namespace TempoControl_RF.Modelos
{
    public class ReporteEmpleado
    {
        public string NombreEmpleado { get; set; }
        public int TotalDiasTrabajados { get; set; }
        public double TotalHorasTrabajadas { get; set; }
    }
}
