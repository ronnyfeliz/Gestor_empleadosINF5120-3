using System;
using System.Collections.Generic;
using System.Linq;
using TempoControl_RF.Datos;
using TempoControl_RF.Modelos;

namespace TempoControl_RF.Logica
{
    public class GestorFichajes
    {
        private GestorEmpleados gestorEmpleados;
        private IRegistroRepositorio repoRegistros;

        public GestorFichajes(GestorEmpleados gestorEmpleados, IRegistroRepositorio repoRegistros)
        {
            this.gestorEmpleados = gestorEmpleados;
            this.repoRegistros = repoRegistros;
        }

        public bool RegistrarEntrada(int empleadoId)
        {
            if (empleadoId <= 0)
                return false;

            var empleado = gestorEmpleados.BuscarEmpleado(empleadoId);

            if (empleado == null || !empleado.Activo)
                return false;

            var registros = repoRegistros.ObtenerTodos();

            bool tieneAbierto = registros.Any(r =>
                r.EmpleadoId == empleadoId && r.FechaHoraSalida == null);

            if (tieneAbierto)
                return false;

            repoRegistros.RegistrarEntrada(new RegistroFichaje
            {
                EmpleadoId = empleadoId,
                FechaHoraEntrada = DateTime.Now,
                FechaHoraSalida = null
            });

            return true;
        }

        public bool RegistrarSalida(int empleadoId)
        {
            if (empleadoId <= 0)
                return false;

            var empleado = gestorEmpleados.BuscarEmpleado(empleadoId);

            if (empleado == null || !empleado.Activo)
                return false;

            var registros = repoRegistros.ObtenerTodos();

            var registroAbierto = registros
                .FirstOrDefault(r => r.EmpleadoId == empleadoId && r.FechaHoraSalida == null);

            if (registroAbierto == null)
                return false;

            repoRegistros.RegistrarSalida(empleadoId);

            return true;
        }

        public List<ReporteEmpleado> GenerarReporte(int mes, int anio)
        {
            var registros = repoRegistros.ObtenerTodos();

            var filtrados = registros
                .Where(r => r.FechaHoraEntrada.Month == mes &&
                            r.FechaHoraEntrada.Year == anio)
                .GroupBy(r => r.EmpleadoId);

            List<ReporteEmpleado> resultado = new List<ReporteEmpleado>();

            foreach (var grupo in filtrados)
            {
                var empleado = gestorEmpleados.BuscarEmpleado(grupo.Key);
                if (empleado == null)
                    continue;

                int totalDias = grupo
                    .Select(r => r.FechaHoraEntrada.Date)
                    .Distinct()
                    .Count();

                double totalHoras = grupo
                    .Where(r => r.FechaHoraSalida != null)
                    .Sum(r => (r.FechaHoraSalida.Value - r.FechaHoraEntrada).TotalHours);

                resultado.Add(new ReporteEmpleado
                {
                    NombreEmpleado = empleado.NombreCompleto,
                    TotalDiasTrabajados = totalDias,
                    TotalHorasTrabajadas = totalHoras
                });
            }

            return resultado;
        }
    }
}