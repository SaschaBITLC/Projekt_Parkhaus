using System.ComponentModel.Design;
using System.Text;

namespace Project_Parkhaus
{
    internal class Program
    {
        //Speichert die Autodaten im Format "Zeitstempel,Ticketnummer". Jedes Element repräsentiert einen Parkplatz
        static String[] parkhaus;

        static int ersteFreieParkstelle, stunde, anzahlFreieParkplaetze;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            parkhausIntitalizieren();

            //Die Hauptschleife, endlos da keine Austrittsbedingung vorgesehen
            while (true)
            {
                Console.Clear();
                freieParkplaetzeErmitteln();
                menueAnzeigen();

                //Stunde inkrementieren
                stunde++;
            }

        }

        //Initialisiert das Array mit leeren Strings. Bei einer normalen Initialisierung sind die Strings null.
        //Leere Strings sind nötig für spätere Checks.
        static void parkhausIntitalizieren()
        {
            parkhaus = new String[5];
            for (int i = 0; i < parkhaus.Length; i++)
            {
                parkhaus[i] = string.Empty;
            }
        }

        //Anzahl der freien Parkplätze ermitteln
        static void freieParkplaetzeErmitteln()
        {
            //zuerst die Zählervariable zurücksetzen
            anzahlFreieParkplaetze = 0;
            //erste freie Parkstelle auf -1 setzen für den späteren Check
            ersteFreieParkstelle = -1;
            for (int i = 0; i < parkhaus.Length; i++)
            {
                String auto = parkhaus[i];
                Console.WriteLine(auto);
                if (auto == "")
                {
                    //Parkplatz nicht belegt, also freie Parkplätze inkrementieren und den Check für den ersten freien Parkplatz machen
                    anzahlFreieParkplaetze++;
                    if (ersteFreieParkstelle == -1)
                    {
                        ersteFreieParkstelle = i;
                    }

                }
                else
                {
                    //Parkplatz belegt

                }
            }
        }

        //Gibt den Stundenteil der gespeicheten Autodaten
        static int stundenKonvertieren(String auto)
        {
            return Convert.ToInt32(auto.Split(",")[0]);
        }

        //Gibt den Ticketnummer der gespeicheten Autodaten
        static int ticketNummerKonvertrieren(String auto)
        {
            return Convert.ToInt32(auto.Split(",")[1]);
        }

        static void menueAnzeigen()
        {
            Console.WriteLine("Herzlich Willkommen im Parkhaus Brackel\n");
            Console.WriteLine($"Freie Parkplätze: {anzahlFreieParkplaetze}");
            Console.WriteLine($"Lokale Zeit: {stunde % 24}:00\n");

            Console.WriteLine("Parken : 1 Stunde - 1 €");
            Console.WriteLine("Parken : 2 Stunden - 2 €");
            Console.WriteLine("Parken : 4 Stunden - 3 €");
            Console.WriteLine("Parken : ganzer Tag - 5 €");
            Console.WriteLine("Parken : Länger als 24 Stunden - 2.50 € / Stunde\n");

            Console.WriteLine("Bitte wählen Sie bitte die 1 fürs Parken oder die 2 für die Ausfahrt");

            // Falls keine Parkplätze, Parken nicht möglich
            if (anzahlFreieParkplaetze > 0)
            {
                Console.WriteLine("1. Parken");
            }
            else
            {
                Console.WriteLine("Es sind leider alle Parkplätze besetzt.");
            }
            Console.WriteLine("2. Ausfahrt");

            switch (optionEingabe())
            {
                case 1:
                    //Parken starten
                    parken();
                    break;
                case 2:
                    //Ausfahrt starten
                    ausfahren();
                    break;
                default:
                    Console.WriteLine("Es ist ein Fehlre aufgetreten. Versuchen Sie es bitte erneut.");
                    break;

            }

        }

        //Gibt eine 1 oder eine 2 zurück, ansonsten restartet rekursiv bis eine gültige Eingabe erfolgt
        static int optionEingabe()
        {
            string eingabe = Console.ReadLine()!;
            //Prüfen ob die eingabe gültig ist
            if (eingabe != null && eingabe.Length > 0 && char.IsDigit(eingabe[0]) && (int.Parse(eingabe) == 1 || int.Parse(eingabe) == 2))
            {
                //Eingabe convertieren
                int nummer = int.Parse(eingabe);
                return nummer;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe! Geben sie eine 1 oder eine 2 ein!");
                optionEingabe();
            }
            return -1;
        }

        //"parkt" das Auto auf einm Parkplatz und teilt ihm eine Ticketnummer zu
        static void parken()
        {
            int ticketNummer = ersteFreieParkstelle + 1;
            parkhaus[ersteFreieParkstelle] = stunde.ToString() + "," + ticketNummer.ToString();
            Console.WriteLine($"Ihre Parkplatznummer: {ersteFreieParkstelle} \n" +
                $"Ihre Ticketnummer: {ticketNummer}\nBitte mit Eingabetaste bestätigen!");

            Console.ReadLine();
        }

        static void ausfahren()
        {
            int ticket = ticketNummerValidieren();
            //bei uns entspricht die Ticketnummer dem Index im parkaus-Array+1. Also Kalkulationen entsprechend anpassen
            int stundenGeparkt = stunde - stundenKonvertieren(parkhaus[ticket - 1]);
            // 1 Stunde   1 €
            // 2 Stunden  2 €
            // 4 Stunden  3 €
            // ganzer Tag 5 €
            // Länger als 24 Stunden - 2.50/Stunde

            //den geschuldeten Geldbetrag ausrechnen
            if (stundenGeparkt <= 1)
            {
                Console.WriteLine($"Sie parkten {stundenGeparkt} Stunden und Ihr Parkpreis beträgt 1€");
            }
            else if (stundenGeparkt <= 2)
            {
                Console.WriteLine($"Sie parkten {stundenGeparkt} Stunden und Ihr Parkpreis beträgt 2€");
            }
            else if(stundenGeparkt <= 4)
            {
                Console.WriteLine($"Sie parkten {stundenGeparkt} Stunden und Ihr Parkpreis beträgt 3€");
            }
            else if(stundenGeparkt <= 24)
            {
                Console.WriteLine($"Sie parkten {stundenGeparkt} Stunden und Ihr Parkpreis beträgt 5€");
            }
            else
            {
                Console.WriteLine($"Sie parkten {stundenGeparkt} Stunden und Ihr Parkpreis beträgt {stundenGeparkt * 2.5:F2}€");
            }

            Console.WriteLine("\nBitte mit Eingabetaste bestätigen!\n");

            Console.ReadLine();

            //Parkplatz räumen
            parkhaus[ticket - 1] = "";

        }

        //Fordert den Nutzer auf die Ticketnummer einzugeben und prüft diese. Falls ungültig, fordert den Nutzer auf es nochmal zu versuchen
        static int ticketNummerValidieren()
        {
            Console.WriteLine("Geben Sie bitte Ihre Ticketnummer ein!(Nur Zahlen von 1 bis 100 zulässig");
            string eingabe = Console.ReadLine()!;
            //Prüfen ob die eingabe gültig ist
            if (eingabe != null && char.IsDigit(eingabe[0]) && (int.Parse(eingabe) >= 1 || int.Parse(eingabe) <= 100))
            {
                //Eingabe convertieren
                int nummer = int.Parse(eingabe);
                for (int i = 0; i < parkhaus.Length; i++)
                {
                    if (nummer == ticketNummerKonvertrieren(parkhaus[i]))
                    {
                        //ticket gefunden!
                        return nummer;
                    }
                }
                //Kein Ticket mit der eingegebenen Nummer gefunden. Starte die Eingabe erneut.
                Console.WriteLine("Ticketnummer nicht gefunden. Bitte versuchen Sie es erneut!");
                ticketNummerValidieren();
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe! Nur Zahlen von 1 bis 100 zulässig!");
                ticketNummerValidieren();
            }
            return -1;
        }
    }
}
