namespace Project_Parkhaus
{
    internal class Program
    {
        //Speichert die Autodaten im Format "Zeitstempel,Ticketnummer". Jedes Element repräsentiert einen Parkplatz
        static String[] parkhaus;

        static int ersteFreieParkstelle, stunde, anzahlFreieParkplaetze;
        static void Main(string[] args)
        {
            parkhausIntitalizieren();

            //Die Hauptschleife, endlos da keine Austrittsbedingung vorgesehen
            while (true)
            {
                freieParkplaetzeErmitteln();

               
                //Stunde inkrementieren
                stunde++;
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

        //Initialisiert das Array mit leeren Strings. Bei einer normalen Initialisierung sind die Strings null.
        //Leere Strings sind nötig für spätere Checks.
        static void parkhausIntitalizieren()
        {
            parkhaus = new String[100];
            for (int i = 0; i < parkhaus.Length; i++)
            {
                parkhaus[i] = string.Empty;
            }
        }

    }

}
