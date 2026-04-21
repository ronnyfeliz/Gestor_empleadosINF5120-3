# Gestor de Empleados INF5120-3

Sistema de gestión de empleados desarrollado en C# (.NET) como parte del curso INF5120-3.

Este programa permite controlar el registro de entrada y salida de empleados, la administración de su información y la generación de reportes mensuales de horas trabajadas.

---

## Funcionalidades principales

### Gestión de empleados (CRUD)
- Registrar nuevos empleados
- Consultar lista de empleados
- Actualizar información por ID
- Activar y desactivar empleados (sin eliminarlos)

### Control de fichaje
- Registro de entrada (hora de inicio)
- Registro de salida (hora de fin)
- Validación de empleados activos
- Evita fichajes duplicados

### Reportes
- Generación de reporte mensual
- Total de días trabajados por empleado
- Total de horas trabajadas en el mes
- Filtrado por mes y año

---

## Arquitectura del sistema

El proyecto está estructurado en capas:

- Capa de Presentación: Menú de consola
- Capa de Lógica de Negocio: Reglas del sistema
- Capa de Acceso a Datos: Repositorios con SQLite
- Capa de Dominio: Entidades (Empleado, RegistroFichaje, ReporteEmpleado)

---

## Base de datos

El sistema utiliza SQLite como motor de base de datos:

- Ligera y portable
- No requiere servidor externo
- Persistencia de datos automática

---

## Tecnologías utilizadas

- C# (.NET)
- SQLite
- LINQ
- Patrón Repositorio (Repository Pattern)
- Programación orientada a objetos (POO)

---

## Cómo ejecutar el proyecto

1. Clonar el repositorio

```bash
git clone https://github.com/tuusuario/Gestor_empleadosINF5120-3.git
