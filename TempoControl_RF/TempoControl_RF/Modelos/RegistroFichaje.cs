using System;
using System.Collections.Generic;
using System.Text;

namespace TempoControl_RF.Modelos
{
    public class RegistroFichaje
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
    }
}

