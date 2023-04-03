using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionConsola
{
    class Alumno {
        public int id { get; set; }
        public int calificacionFinal { get; set; }
        public string nombre { get; set; }
        public string matricula { get; set; }

        public int CalcularPromedio(int primeraEvaluacion, int segundaEvaluacion, int terceraEvaluacion) {
            int promedio = 0;
            promedio = (primeraEvaluacion + segundaEvaluacion + terceraEvaluacion) / 3;
            return promedio;
        }

        public string ImprimeDatosAlumno() {
            var datosAlumno = "";
            datosAlumno = "ID["+this.id+"], Nombre["+this.nombre+"], Matricula["+this.matricula+"], CalificacionFInal["+this.calificacionFinal+"]";
            return datosAlumno;
        }
    }

     

    internal class Program
    {
        static string ValuesIntoString(Alumno a)
        {
            var data = "data";
            data = a.GetType().GetProperties()
                .Select(info => (info.Name, Value: info.GetValue(a, null) ?? "null"))
                .Aggregate(
                    new StringBuilder(),
                    (sb, pair) => sb.Append($"{pair.Name}:{pair.Value},"),
                    sb => sb.ToString());
            data = data.Remove(data.Length-1, 1);
            return data;
        }
        static string cambiaEspaciosPorGuionesBajos(string cadena)
        {
            cadena = cadena.Replace(" ", "_");
            return cadena;
        }
        static void Main(string[] args)
        {
            //Esta parte del codigo imprime un Hola Mundo en la consola
            Console.WriteLine("Hola Mundo");
            
            //En esta parte del codigo estamos viendo los tios de variables y datos que puede usarse en c#

            //Variable tipo entero
            int numeroEntero = 10;
            //Variable tipo String
            var cadenaPrueba = "Mi cadena de texto";
            //Variable tipo boolean
            bool isEnabled = true;

            //isEnabled = false;

            
            Console.WriteLine(cadenaPrueba);

            Console.WriteLine("Mi cadena prueba modificada es: " + cambiaEspaciosPorGuionesBajos(cadenaPrueba));

            //Si isEnabled es igual a true
            if (isEnabled) {
                //Iteraos de un numero hata el numeroEntero especificado
                for (int i = 5; i <= numeroEntero; i++) {
                    //Imprimimos en consola
                    Console.WriteLine("Esta es la linea Numero " + i);
                }
            }

            //Tipo de objeto lista, que alamacena varuas cadenas
            List<string> listadeNumeros = new List<string>();

            //El meodo ADD nos permite agregar elementos a nuestra lista
            listadeNumeros.Add("1");
            listadeNumeros.Add("2");
            listadeNumeros.Add("3");

            //El metodo remove quita un lemento especificado de nuestra lista
            listadeNumeros.Remove("2");

            //Variable tipo entero que nos va indicar elindice en el que nos encontramos
            int index = 0;
            //En esta parte iteramos sobre el contenido que existe en nuestra lista
            foreach (string numero in listadeNumeros) {
                Console.WriteLine("El numero en el indice["+index+"]  es: "+ numero);
                index++;
            }

            Console.WriteLine("El numero en el indice[1]  es: " + listadeNumeros[1]);

            Alumno alumnoTemporal = new Alumno();
            alumnoTemporal.id = 123;
            alumnoTemporal.nombre = "Mike Wazawski";
            alumnoTemporal.matricula = "121346";

            alumnoTemporal.calificacionFinal = alumnoTemporal.CalcularPromedio(10,5,0);

            Console.WriteLine("Datos de alumno: " + alumnoTemporal.ImprimeDatosAlumno());

            var valores = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var suma = 0;

            foreach (var valor in valores) {
                suma += valor;
            }

            Console.WriteLine("Suma total:" + suma.ToString());

            var pares = new List<int>();

            foreach (var valor in valores) {
                if (valor % 2 == 0) {
                    pares.Add(valor);
                }
            }

            Console.WriteLine("Numeros Pares: " + string.Join(",",pares.ToArray()));

            Console.WriteLine("Suma total con LINQ:" + valores.Sum());

            Console.WriteLine("Numeros Pares con LINQ:" + string.Join(",",valores.Where(x => x%2==0).ToList().ToArray()));

            List<Alumno> alumnos = new List<Alumno>{
                new Alumno { id = 1, nombre = "Jorge", matricula = "1111",calificacionFinal=10},
                new Alumno { id = 2, nombre = "Martha", matricula = "1112", calificacionFinal = 5 },
                new Alumno { id = 3, nombre = "Sandra", matricula = "1113", calificacionFinal = 8 }
            };

            //LINQ-Select
            var nombresAlumnos = alumnos.Select(x => x.nombre).ToList();
            //LINQ-Where
            var alumnosAprobados = alumnos.Where(x => x.calificacionFinal > 5).ToList();
            //LINQ-Fist,Last
            var primerAlumno = alumnos.First();
            var ultimoAlumno = alumnos.Last();
            //LINQ-OrderBy
            var deMenorAMayor = alumnos.OrderBy(x => x.id).ToList();
            var deMayorAMenor = alumnos.OrderByDescending(x => x.id).ToList();

            Console.WriteLine("LINKQ-Select:" + string.Join(",",nombresAlumnos.ToArray()));

            Console.WriteLine("LINKQ-Where:");
            foreach (var alumno in alumnosAprobados) {
                Console.WriteLine("Alumno-Where:" + ValuesIntoString(alumno));
            }
            Console.WriteLine("LINKQ-First:" + ValuesIntoString(primerAlumno));
            Console.WriteLine("LINKQ-Last:" + ValuesIntoString(ultimoAlumno));

            Console.WriteLine("LINKQ-OrderBy:");
            foreach (var alumno in deMenorAMayor)
            {
                Console.WriteLine("Alumno-OrderBy:" + ValuesIntoString(alumno));
            }

            Console.WriteLine("LINKQ-OrderByDescending:");
            foreach (var alumno in deMayorAMenor)
            {
                Console.WriteLine("Alumno-OrderByDescending:" + ValuesIntoString(alumno));
            }

            //LINQ-SUM
            var sumaCalificaciones = alumnos.Sum(x => x.calificacionFinal);

            //LINQ-Min,Max
            var calificacionMaxima = alumnos.Max(x => x.calificacionFinal);
            var calificacionMinima = alumnos.Min(x => x.calificacionFinal);

            //LINQ-Avergage
            var calificacionMedia = alumnos.Average(x => x.calificacionFinal);

            //LINQ-All,Any
            var todosLosAlumnosAprobados = alumnos.All(x => x.calificacionFinal > 5);
            var cuyalquierAlumnoAprobados = alumnos.Any(x => x.calificacionFinal > 5);

            Console.WriteLine("LINKQ-Sum: SumaCalificaciones=" + sumaCalificaciones);

            Console.WriteLine("LINKQ-Max: CalificacionMaxima=" + calificacionMaxima);
            Console.WriteLine("LINKQ-MIn: CalificacionMininima=" + calificacionMinima);

            Console.WriteLine("LINKQ-Average: CalificacionMedia=" + calificacionMedia);

            Console.WriteLine("LINKQ-All: " + todosLosAlumnosAprobados);
            Console.WriteLine("LINKQ-Any: " + cuyalquierAlumnoAprobados);

            Console.WriteLine("Presiona cualquier tecla alfanumerica para terminar...");
            Console.ReadKey();
        }
    }
}
