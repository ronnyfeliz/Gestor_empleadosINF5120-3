using System;
using System.Collections.Generic;
using System.Text;
using TempoControl_RF.Modelos;

namespace TempoControl_RF.Datos
{
    public interface IRegistroRepositorio
    {
        void RegistrarEntrada(RegistroFichaje r);
        void RegistrarSalida(int empleadoId);
        List<RegistroFichaje> ObtenerTodos();
    }
}