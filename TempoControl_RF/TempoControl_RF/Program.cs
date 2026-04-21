// Ronny Feliz 100704427 INF5120-3

using System;
using TempoControl_RF.Datos;
using TempoControl_RF.Logica;
using TempoControl_RF.Modelos;

class Program
{
    static void Main(string[] args)
    {
        // REPOSITORIOS DE DATOS
        IEmpleadoRepositorio repoEmp = new EmpleadoRepositorioSQLite();
        IRegistroRepositorio repoReg = new RegistroRepositorioSQLite();

        // GESTORES DE LOGICA
        GestorEmpleados gestorEmp = new GestorEmpleados(repoEmp);
        GestorFichajes gestorFich = new GestorFichajes(gestorEmp, repoReg);

        int opcion = -1;

        do
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("GESTION DE NOMINA SALARIAL");
            Console.WriteLine("____________________________________________");
            Console.ResetColor();

            Console.WriteLine("1. AGREGAR EMPLEADO");
            Console.WriteLine("2. LISTAR EMPLEADOS");
            Console.WriteLine("3. REGISTRAR ENTRADA");
            Console.WriteLine("4. REGISTRAR SALIDA");
            Console.WriteLine("5. GENERAR REPORTE");
            Console.WriteLine("6. ACTIVAR EMPLEADO");
            Console.WriteLine("7. DESACTIVAR EMPLEADO");
            Console.WriteLine("0. SALIR");

            Console.WriteLine("____________________________________________");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("ELIJA UNA OPCION: ");
            Console.ResetColor();

            while (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("ENTRADA INVALIDA. INTENTE DE NUEVO: ");
                Console.ResetColor();
            }

            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("INGRESE DATOS DEL EMPLEADO");

                    int id;
                    Console.Write("ID: ");
                    while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
                    {
                        Console.Write("ID INVALIDO. INTENTE DE NUEVO: ");
                    }

                    Console.Write("NOMBRE: ");
                    string nombre = Console.ReadLine();

                    Console.Write("DEPARTAMENTO: ");
                    string depto = Console.ReadLine();

                    Console.Write("POSICION: ");
                    string pos = Console.ReadLine();

                    Empleado emp = new Empleado
                    {
                        Id = id,
                        NombreCompleto = nombre,
                        Departamento = depto,
                        Posicion = pos
                    };

                    Console.WriteLine(
                        gestorEmp.AgregarEmpleado(emp)
                        ? "EMPLEADO AGREGADO CORRECTAMENTE"
                        : "ERROR AL AGREGAR EMPLEADO"
                    );

                    break;

                case 2:
                    var lista = gestorEmp.ListarEmpleados();

                    Console.WriteLine("LISTA DE EMPLEADOS");

                    foreach (var e in lista)
                    {
                        Console.WriteLine($"{e.Id} - {e.NombreCompleto} - {(e.Activo ? "ACTIVO" : "INACTIVO")}");
                    }
                    break;

                case 3:
                    int idEntrada;
                    Console.Write("ID EMPLEADO: ");
                    while (!int.TryParse(Console.ReadLine(), out idEntrada) || idEntrada <= 0)
                    {
                        Console.Write("ID INVALIDO: ");
                    }

                    Console.WriteLine(
                        gestorFich.RegistrarEntrada(idEntrada)
                        ? "ENTRADA REGISTRADA"
                        : "ERROR AL REGISTRAR ENTRADA"
                    );
                    break;

                case 4:
                    int idSalida;
                    Console.Write("ID EMPLEADO: ");
                    while (!int.TryParse(Console.ReadLine(), out idSalida) || idSalida <= 0)
                    {
                        Console.Write("ID INVALIDO: ");
                    }

                    Console.WriteLine(
                        gestorFich.RegistrarSalida(idSalida)
                        ? "SALIDA REGISTRADA"
                        : "ERROR AL REGISTRAR SALIDA"
                    );
                    break;

                case 5:
                    int mes;
                    Console.Write("MES (1-12): ");
                    while (!int.TryParse(Console.ReadLine(), out mes) || mes < 1 || mes > 12)
                    {
                        Console.Write("MES INVALIDO: ");
                    }

                    int anio;
                    Console.Write("AÑO: ");
                    while (!int.TryParse(Console.ReadLine(), out anio) || anio < 2000)
                    {
                        Console.Write("AÑO INVALIDO: ");
                    }

                    var reporte = gestorFich.GenerarReporte(mes, anio);

                    Console.WriteLine("REPORTE MENSUAL");

                    foreach (var r in reporte)
                    {
                        Console.WriteLine($"{r.NombreEmpleado} | DIAS: {r.TotalDiasTrabajados} | HORAS: {r.TotalHorasTrabajadas:F2}");
                    }
                    break;

                case 6:
                    int idActv;
                    Console.Write("ID DEL EMPLEADO: ");

                    while (!int.TryParse(Console.ReadLine(), out idActv) || idActv <= 0)
                    {
                        Console.Write("ID INVALIDO: ");
                    }

                    Console.WriteLine(
                        gestorEmp.ActivarEmpleado(idActv)
                        ? "EMPLEADO ACTIVADO"
                        : "ERROR AL ACTIVAR EMPLEADO"
                    );

                    break;

                case 7:
                    int idDesact;
                    Console.Write("ID DEL EMPLEADO: ");
                    while (!int.TryParse(Console.ReadLine(), out idDesact) || idDesact <= 0)
                    {
                        Console.Write("ID INVALIDO: ");
                    }

                    Console.WriteLine(
                        gestorEmp.DesactivarEmpleado(idDesact)
                        ? "EMPLEADO DESACTIVADO"
                        : "ERROR AL DESACTIVAR EMPLEADO"
                    );
                    break;

                case 0:
                    Console.WriteLine("SALIENDO DEL SISTEMA...");
                    break;

                default:
                    Console.WriteLine("OPCION INVALIDA");
                    break;
            }

            Console.WriteLine("\nPRESIONE UNA TECLA PARA CONTINUAR...");
            Console.ReadKey();

        } while (opcion != 0);
    }
}