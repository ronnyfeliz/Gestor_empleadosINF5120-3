using System;
using System.Collections.Generic;
using TempoControl_RF.Datos;
using TempoControl_RF.Modelos;

namespace TempoControl_RF.Logica
{
    public class GestorEmpleados
    {
        private IEmpleadoRepositorio repo;

        public GestorEmpleados(IEmpleadoRepositorio repo)
        {
            this.repo = repo;
        }

        public bool AgregarEmpleado(Empleado nuevo)
        {
            if (nuevo == null) return false;
            if (nuevo.Id <= 0) return false;

            if (repo.ObtenerPorId(nuevo.Id) != null)
                return false;

            nuevo.Activo = true;
            repo.Agregar(nuevo);

            return true;
        }

        public bool ActualizarEmpleado(Empleado actualizado)
        {
            if (actualizado == null) return false;
            if (actualizado.Id <= 0) return false;

            if (repo.ObtenerPorId(actualizado.Id) == null)
                return false;

            repo.Actualizar(actualizado);

            return true;
        }

        public Empleado BuscarEmpleado(int id)
        {
            if (id <= 0) return null;

            return repo.ObtenerPorId(id);
        }

        public bool DesactivarEmpleado(int id)
        {
            if (id <= 0) return false;

            Empleado empleado = repo.ObtenerPorId(id);

            if (empleado == null) return false;
            if (!empleado.Activo) return false;

            repo.Desactivar(id);

            return true;
        }

        public bool ActivarEmpleado(int id)
        {
            if (id <= 0) return false;

            Empleado empleado = repo.ObtenerPorId(id);

            if (empleado == null) return false;

            if (empleado.Activo) return false;

            repo.Activar(id);

            return true;
        }

        public List<Empleado> ListarEmpleados()
        {
            return repo.ObtenerTodos();
        }
    }
}