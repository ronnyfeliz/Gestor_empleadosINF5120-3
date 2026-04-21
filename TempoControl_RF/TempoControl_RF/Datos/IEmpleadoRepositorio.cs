using System;
using System.Collections.Generic;
using System.Text;
using TempoControl_RF.Modelos;

namespace TempoControl_RF.Datos
{
    public interface IEmpleadoRepositorio
    {
        void Agregar(Empleado empleado);
        void Actualizar(Empleado empleado);
        Empleado ObtenerPorId(int id);
        List<Empleado> ObtenerTodos();
        void Desactivar(int id);
        void Activar(int id);
    }
}