using System.ComponentModel.Design;
using System.Text;

namespace Project_Parkhaus
{
    internal class Program
    {
        //Speichert die Autodaten im Format "Zeitstempel,Ticketnummer". Jedes Element repräsentiert einen Parkplatz
        static string[]? parkhaus;

        static int ersteFreieParkstelle, stunde, anzahlFreieParkplaetze;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            parkhausIntitalizieren();

            //Die Hauptschleife, endlos da keine Austrittsbedingung vorgesehen
            while (true)
            {
                freieParkplaetzeErmitteln();
                menueAnzeigen();

                //Stunde inkrementieren
                stunde++;
            }
        }

        //Initialisiert das Array mit leeren Strings. Bei einer normalen Initialisierung sind die Strings null.
        //Leere Strings werden für die null-Sucherhet bei späteren Checks verwendet.
        static void parkhausIntitalizieren()
        {
            parkhaus = new string[100];
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
                string auto = parkhaus[i];
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
        static int stundenKonvertieren(string auto)
        {
            return Convert.ToInt32(auto.Split(",")[0]);
        }

        //Gibt den Ticketnummer der gespeicheten Autodaten
        static int ticketNummerKonvertrieren(string auto)
        {
            return Convert.ToInt32(auto.Split(",")[1]);
        }

        //Zeigt die aktuelle Parkplatzbelegung in einem 10xn Raster an. n hängt von der Anzahl der Parkplätze ab.
        //Belegte Parkplätze werden mit einem Auto-Emoji dargestellt, freie Parkplätze mit ihrer Nummer in eckigen Klammern.
        static void zeigeParkplatz()
        {
            // Überschrift im passenden Stil vorbereiten
            string titel = "AKTUELLE PARKPLATZBELEGUNG";
            int nutzBreite = 38;
            string rahmenLinie = new string('═', nutzBreite + 2);
            string formatiertTitel = titel.PadRight(nutzBreite);

            Console.WriteLine($"╔{rahmenLinie}╗");
            Console.WriteLine($"║ {formatiertTitel} ║");
            Console.WriteLine($"╚{rahmenLinie}╝\n");

            // Wir laufen durch unser Parkhaus-Array
            for (int i = 0; i < parkhaus.Length; i++)
            {
                // Check, ob der Platz belegt ist (String ist nicht leer)
                bool istBelegt = !string.IsNullOrEmpty(parkhaus[i]);

                // Symbol bestimmen: Auto-Emoji bei belegt, sonst die Parkplatznummer
                // (i + 1).ToString("D2") macht aus Platz 1 -> "01", Platz 2 -> "02" usw., damit alles symmetrisch bleibt
                string symbol = istBelegt ? " 🚗 " : $"[{(i + 1).ToString("D2")}]";

                // Das Symbol auf der Konsole ausgeben
                Console.Write(symbol + " ");

                // Umbruch-Logik: Nach jeweils 10 Parkplätzen wird eine neue Reihe angefangen
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("\n");
        }

        static void menueAnzeigen()
        {
            Console.Clear();

            // 1. HEADER-BEREICH (Doppelrahmen)
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          HERZLICH WILLKOMMEN IM PARKHAUS BRACKEL       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝\n");

            // 2. STATUS-BEREICH
            string formatierteZeit = $"{(stunde % 24):00}:00";
            Console.Write("  Lokale Zeit: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(formatierteZeit);
            Console.ResetColor();

            Console.Write("  Freie Parkplätze: ");
            if (anzahlFreieParkplaetze > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{anzahlFreieParkplaetze} / {parkhaus!.Length} Plätze frei");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VOLLBESETZT");
            }
            Console.ResetColor();
            Console.WriteLine();

            //Zeige die Parkplatzbelegung
            zeigeParkplatz();

            // 3. TARIF-BEREICH (Doppelrahmen)
            Console.WriteLine("╔═══════════════════ UNSERE TARIFE ══════════════════════╗");
            Console.WriteLine("║  • 1 Stunde            : 1,00 €                        ║");
            Console.WriteLine("║  • 2 Stunden           : 2,00 €                        ║");
            Console.WriteLine("║  • 4 Stunden           : 3,00 €                        ║");
            Console.WriteLine("║  • Ganzer Tag          : 5,00 €                        ║");
            Console.WriteLine("║  • Länger als 24 Std.  : 2,50 € / Stunde               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            // 4. OPTIONEN-BEREICH
            Console.WriteLine("Bitte wählen Sie eine Option:");
            Console.WriteLine("══════════════════════════════════════════════════════════");

            bool parkenMoeglich = anzahlFreieParkplaetze > 0;
            bool ausfahrtMoeglich = anzahlFreieParkplaetze < parkhaus.Length;

            if (parkenMoeglich)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [1] Parken");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("  [-] Parken nicht möglich (Parkhaus ist voll)");
            }

            if (ausfahrtMoeglich)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("  [2] Ausfahrt");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [-] Keine Autos im Parkhaus zum Ausfahren");
            }
            Console.ResetColor();

            Console.WriteLine("══════════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.Write("\nIhre Auswahl > ");

            // 5. EINGABE & LOGIK (Auswertung und Validierung der Logik-Zustände)
            int eingabe = optionEingabe();

            switch (eingabe)
            {
                case 1:
                    if (parkenMoeglich)
                    {
                        parken();
                    }
                    else
                    {
                        ZeigeFehler("Das Parkhaus ist leider voll. Sie können derzeit nicht parken.");
                        menueAnzeigen(); // Zurück zum Menü für einen neuen Versuch
                    }
                    break;

                case 2:
                    if (ausfahrtMoeglich)
                    {
                        ausfahren();
                    }
                    else
                    {
                        ZeigeFehler("Es parken gerade keine Autos!");
                        menueAnzeigen(); // Zurück zum Menü für einen neuen Versuch
                    }
                    break;

                case -1:
                    //Eingabe war ungültig, das Menü wurde bereits neu gezeichnet, daher einfach zurückkehren
                    break;
                default:
                    // Wird theoretisch nicht erreicht, da optionEingabe nur 1 oder 2 zurücklassen darf
                    ZeigeFehler("Ungültige Auswahl.");
                    menueAnzeigen();
                    break;
            }
        }

        // Hilfsmethode für Fehleranzeige im passenden Stil. Passt sich an die Länge der Nachricht an.
        static void ZeigeFehler(string nachricht)
        {
            int innenBreite = nachricht.Length + 10;
            string rahmenLinie = new string('═', innenBreite);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n╔{rahmenLinie}╗");
            Console.WriteLine($"║ FEHLER: {nachricht} ║");
            Console.WriteLine($"╚{rahmenLinie}╝");
            Console.ResetColor();

            Console.WriteLine("\nDrücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }

        // Liest Nutzereingaben. Gibt garantiert eine gültige 1 oder 2 zurück.
        static int optionEingabe()
        {
            string eingabe = Console.ReadLine()!;

            // Prüfen, ob die Eingabe eine 1 oder 2 ist
            if (eingabe == "1" || eingabe == "2")
            {
                return int.Parse(eingabe);
            }
            else
            {
                ZeigeFehler("Ungültige Eingabe! Geben Sie bitte eine 1 oder eine 2 ein!");

                // Da die Eingabe fehlerhaft war, müssen wir das Menü neu zeichnen,
                // damit der User sieht, was er eigentlich auswählen durfte.
                menueAnzeigen();
                return -1;
            }
        }

        //"parkt" das Auto auf einm Parkplatz und teilt ihm eine Ticketnummer zu
        static void parken()
        {
            int ticketNummer = ersteFreieParkstelle;
            parkhaus![ersteFreieParkstelle] = stunde.ToString() + "," + ticketNummer.ToString();

            // Die Daten ohne Rahmen-Formatierung. +1, da die Anzeige für den Nutzer bei 1 beginnt, nicht bei 0.
            string titel = "TICKET ERFOLGREICH ERSTELLT";
            string zeile1 = "• Ihr Parkplatz: " + (ersteFreieParkstelle + 1);
            string zeile2 = "• Ihr Ticket   : Nr. " + (ticketNummer + 1);

            // Wir ermitteln die Länge des längsten Textes
            int maxTextLaenge = Math.Max(titel.Length, Math.Max(zeile1.Length, zeile2.Length));

            // Die exakte Breite, die der Text im Inneren des Rahmens einnehmen soll (inklusive 2 Leerzeichen Puffer)
            int nutzBreite = maxTextLaenge + 2;

            // Die Rahmenlinien oben, mitte und unten entsprechen genau der Nutzbreite plus den 2 äußeren Leerzeichen
            string rahmenLinie = new string('═', nutzBreite + 2);

            // Wir füllen die Zeilen exakt auf die Nutzbreite auf
            string formatiertTitel = titel.PadRight(nutzBreite);
            string formatiertZeile1 = zeile1.PadRight(nutzBreite);
            string formatiertZeile2 = zeile2.PadRight(nutzBreite);

            // Ausgabe (Die Leerzeichen links und rechts vom Text sind jetzt fest im String verdrahtet)
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"╔{rahmenLinie}╗");
            Console.WriteLine($"║ {formatiertTitel} ║");
            Console.WriteLine($"╠{rahmenLinie}╣");
            Console.WriteLine($"║ {formatiertZeile1} ║");
            Console.WriteLine($"║ {formatiertZeile2} ║");
            Console.WriteLine($"╚{rahmenLinie}╝");
            Console.ResetColor();

            Console.WriteLine("\nBitte mit der Eingabetaste bestätigen...");
            Console.ReadLine();
        }

        static void ausfahren()
        {
            int ticket = ticketNummerValidieren();
            int stundenGeparkt = stunde - stundenKonvertieren(parkhaus[ticket]);
            double preis = 0;

            //den geschuldeten Geldbetrag ausrechnen
            if (stundenGeparkt <= 1)
            {
                preis = 1.00;
            }
            else if (stundenGeparkt <= 2)
            {
                preis = 2.00;
            }
            else if (stundenGeparkt <= 4)
            {
                preis = 3.00;
            }
            else if (stundenGeparkt <= 24)
            {
                preis = 5.00;
            }
            else
            {
                preis = stundenGeparkt * 2.50;
            }

            // Die Rohdaten ohne Rahmen-Formatierung
            string titel = "PARKQUITTUNG";
            string zeile1 = $"• Parkdauer : {stundenGeparkt} Stunde(n)";
            string zeile2 = $"• Parkpreis : {preis.ToString("F2")} €";

            // Wir ermitteln die Länge des längsten Textes
            int maxTextLaenge = Math.Max(titel.Length, Math.Max(zeile1.Length, zeile2.Length));

            // Die exakte Breite, die der Text im Inneren des Rahmens einnehmen soll (inklusive 2 Leerzeichen Puffer)
            int nutzBreite = maxTextLaenge + 2;

            // Die Rahmenlinien oben, mitte und unten entsprechen genau der Nutzbreite plus den 2 äußeren Leerzeichen
            string rahmenLinie = new string('═', nutzBreite + 2);

            // Wir füllen die Zeilen exakt auf die Nutzbreite auf
            string formatiertTitel = titel.PadRight(nutzBreite);
            string formatiertZeile1 = zeile1.PadRight(nutzBreite);
            string formatiertZeile2 = zeile2.PadRight(nutzBreite);

            // Quittungsausgabe
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔{rahmenLinie}╗");
            Console.WriteLine($"║ {formatiertTitel} ║");
            Console.WriteLine($"╠{rahmenLinie}╣");
            Console.WriteLine($"║ {formatiertZeile1} ║");
            Console.WriteLine($"║ {formatiertZeile2} ║");
            Console.WriteLine($"╚{rahmenLinie}╝");
            Console.ResetColor();

            Console.WriteLine("\nBitte drücken Sie die Eingabetaste um den Zahlvorgang abzuschließen! Wir wünschen Ihnen eine schöne Weiterfahrt!\n");
            Console.ReadLine();

            //Parkplatz räumen
            parkhaus[ticket] = "";
        }

        static int ticketNummerValidieren()
        {
            string titel = "AUSFAHRTSKONTROLLE";

            // Einfache Box-Berechnung analog zu den anderen Funktionen
            int nutzBreite = titel.Length + 2;
            string rahmenLinie = new string('═', nutzBreite + 2);
            string formatiertTitel = titel.PadRight(nutzBreite);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔{rahmenLinie}╗");
            Console.WriteLine($"║ {formatiertTitel} ║");
            Console.WriteLine($"╚{rahmenLinie}╝");
            Console.ResetColor();
            Console.Write("\nBitte geben Sie Ihre Ticketnummer ein (1-100) > ");

            string eingabe = Console.ReadLine()!;

            //Prüfen ob die eingabe gültig ist
            if (eingabe != null && eingabe.Length > 0 && char.IsDigit(eingabe[0]) && int.Parse(eingabe) >= 1 && int.Parse(eingabe) <= 100)
            {
                //Eingabe convertieren. -1, da die Nutzereingaben mit 1 und nicht mit 0 anfangen!
                int nummer = int.Parse(eingabe) - 1;
                for (int i = 0; i < parkhaus.Length; i++)
                {
                    if (parkhaus[i] != null && parkhaus[i] != "" && nummer == ticketNummerKonvertrieren(parkhaus[i]))
                    {
                        //ticket gefunden!
                        return nummer;
                    }
                }
                //Kein Ticket mit der eingegebenen Nummer gefunden. Starte die Eingabe erneut.
                ZeigeFehler("Ticketnummer nicht vergeben oder ungültig!");
                return ticketNummerValidieren();
            }
            else
            {
                ZeigeFehler("Ungültige Eingabe! Nur Zahlen von 1 bis 100 zulässig!");
                return ticketNummerValidieren();
            }
        }
    }
}
