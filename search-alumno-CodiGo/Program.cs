using System;
using System.Net;
using System.IO;

namespace search_alumno_CodiGo
{
    class Program
    {
        static string minombre;
        static string[] alumnos = { "72117500", "74119617", "77657476", "71736721", "70652084", "71521063", "75211693", "43474669", "73131641", "43470033", "70748476", "72815887", "74690111", "47862719", "72313903", "72406945" };

        static void Main(string[] args)
        {
            String dni;
            //string[] alumnos = { "72117500", "74119617", "77657476", "71736721", "70652084", "71521063", "75211693", "43474669", "73131641", "43470033", "70748476", "72815887", "74690111", "47862719", "72313903", "72406945" };

            Console.WriteLine("=== VERIFICACIÓN DE ALUMNO ===");
            Console.Write("Ingrese el número de DNI: ");
            dni = Console.ReadLine().ToString();

           
            bool isAlumno;

            // Llamada a la función buscar
            isAlumno = buscarAlumno(dni);
            if (isAlumno)
            {
                    
                Console.WriteLine("Estamos validando en RENIEC");

                Imprimir(DividirNombres(ConsultarReniec(dni)));

                ImprimirLista(alumnos);

            }
            else
            {
                while(isAlumno == false) { 
                    Console.WriteLine("Lo sentimos no es alumno de Código");
                    Console.Write("Vuelva a ingresar DNI: ");
                    dni = Console.ReadLine().ToString();
                    isAlumno = buscarAlumno(dni);
                    if (isAlumno)
                    {

                        Console.WriteLine("Estamos validando en RENIEC");

                        Imprimir(DividirNombres(ConsultarReniec(dni)));

                        ImprimirLista(alumnos);

                    }
                }
            }

            
        }

        //Alexander y Luis
        static bool buscarAlumno(String dni)
        {
            //string[] alumnos = {"72117500", "74119617", "77657476", "71736721", "70652084", "71521063", "75211693", "43474669", "73131641", "43470033", "70748476", "72815887", "74690111", "47862719", "72313903", "72406945"}; 
            int cont = 0;
            //string dni = Console.ReadLine();
            for (int i = 0; i < alumnos.Length; i++)
            {
                if (dni == alumnos[i])
                {
                    cont++;
                }
            }
            if (cont == 1)
                return true;
            else
                return false;

        }

        //POR : TICONA QUISPE,JORGE ERNESTO//
        static string ConsultarReniec(string dni)
        {
            string html = string.Empty;
            string url = "http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI=" + dni;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;

        }

        static void Imprimir(string[] names)
        {

            string ApPaterno = names[0];
            string ApMaterno = names[1];
            string nombress = names[2];

            string name = nombress;
            var separar = name.Split(" ");
            //Console.WriteLine(separar.Length);//
            Console.WriteLine("===========Bienvenido===========");
            Console.WriteLine("Nombres: {0} - #{1} Nombre(s) ", nombress, separar.Length);
            Console.WriteLine("Apellidos: {0} {1} -#{2} Apellidos", ApPaterno, ApMaterno, names.Length - 1);

        }

        static string[] DividirNombres(string nombres)
        {
            
            var names = nombres.Split('|');
            
            return names;
        }

        static void ImprimirLista(string[] lista)
        {
            string alumno;
            //string[] lista = alumnos;

            Console.WriteLine();
            Console.WriteLine("=============REPORTE=============");
            for (int i = 0; i < lista.Length; i++)
            {
                alumno = lista[i];
                string nombres = ConsultarReniec(alumno);
                string[] fullname = DividirNombres(nombres);
                Imprimir(fullname);
            }
        }

    }
}
