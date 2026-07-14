using System.ComponentModel.Design;
using System.Text;

namespace Project_Parkhaus
{
    internal class Program
    {
        static String[] parkhaus = new String[100];
        static int ersteFreieParkstelle, aktuelleStunde;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            menueAnzeigen();
        }
        static void menueAnzeigen()
        {
            /*
            Console.WriteLine("1. Auto einparken");
            Console.WriteLine("2. Auto ausparken");
            Console.WriteLine("3. Parkhausstatus anzeigen");
            Console.WriteLine("4. Beenden");
            */


            // IF / Else -Verzweigung 

            
            Console.WriteLine("Herzlich Willkommen im Parkhaus Brackel\n");
            Console.WriteLine("Parken : 1 Stunde   1 € \n");
            Console.WriteLine("Parken : 2 Stunden  2 € \n");
            Console.WriteLine("Parken : 4 Stunden  3 € \n");
            Console.WriteLine("Parken : ganzer Tag 5 € \n");
            Console.WriteLine("Bitte wählen Sie eine der folgenden Optionen:");
            

            Console.WriteLine("1. Parken");
            Console.WriteLine("2. Ausfahrt");
          

            // ELSE TEIL
            
            
            Console.WriteLine("Es sind leider alle Parkplätze besetzt.");
        }

        
        
    }
}
