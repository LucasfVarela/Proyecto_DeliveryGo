using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DeliveryGo
{
    public class UI
    {

      public static  void BuildMenu(string titulo,List<string> opciones)
        {
             Console.Clear();
            int ancho = 40;
            int alto = opciones.Count + 4;
            DibujarMarco(ancho, alto);
            Console.SetCursorPosition((Console.WindowWidth - titulo.Length) / 2, 5);
            Console.WriteLine(titulo);

            for (int i = 0; i < opciones.Count; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - opciones[i].Length) / 2, 7+i);
                Console.WriteLine(opciones[i]);
            }
            Console.SetCursorPosition((Console.WindowWidth - 30)/2,7+ opciones.Count +2 );
        }

        static void DibujarMarco(int ancho, int alto) {

            int x = (Console.WindowWidth - ancho) / 2;
            int y = 4;
            Console.SetCursorPosition(x,y);
            Console.Write("╔" + new string('═', ancho - 2) + "╗");


            for (int i = 1; i < alto -1; i++)
            {
                Console.SetCursorPosition(x, y+i);
                Console.Write("║" + new string(' ', ancho - 2) + "║");
            }


            Console.SetCursorPosition(x, y + alto -1);
            Console.Write("╚" + new string('═', ancho - 2) + "╝");
        }

        static void MostrarMensaje(string mensaje) {
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - mensaje.Length) / 2, Console.WindowHeight/2);
            Console.WriteLine(mensaje);
            Console.ReadKey();
        }


    }
}
