using System.Globalization;

namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VideogameMenager manager = new VideogameMenager();
            int choice;

            do
            {
                // MENU DI SELEZIONE
                Console.WriteLine("\n**************\nMenu:");
                Console.WriteLine("1. Inserire un nuovo videogioco");
                Console.WriteLine("2. Ricerca un videogioco per ID");
                Console.WriteLine("3. Ricerca tutti i videogiochi aventi il nome contenente una determinata stringa");
                Console.WriteLine("4. Cancella un videogioco");
                Console.WriteLine("5. Chiudere il programma\n**************\n");
                Console.Write("Digita un numero per effettuare una scelta: ");

                // SE L'INPUT è DIVERSO DA UN INTERO MOSTRA UN MESSAGGIO DI ERRORE E CONTINUA LA SCELTA
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Scelta non valida. Riprova.");
                    continue;
                }

                switch (choice)
                {
                    // INSERIMENTO DI UN NUOVO VIDEOGIOCO
                    case 1:
                        Console.WriteLine("Inserisci i dettagli del videogioco:");
                        // NOME CON IL RISPETTIVO CONTROLLO
                        Console.Write("Nome: ");
                        string name = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Il nome non può essere vuoto. Riprova.");
                            Console.Write("Nome: ");
                            name = Console.ReadLine();
                        }
                        // DESCRIZIONE CON IL CORRISPETTIVO CONTROLLO
                        Console.Write("Descrizione: ");
                        string overview = Console.ReadLine();
                        while (string.IsNullOrWhiteSpace(overview))
                        {
                            Console.WriteLine("La descrizione non può essere vuota. Riprova.");
                            Console.Write("Descrizione: ");
                            overview = Console.ReadLine();
                        }
                        // DATA DI RILASCIO CON IL RISPETTIVO CONTROLLO
                        string releaseDate = null;
                        while (true)
                        {
                            Console.Write("Data di rilascio (dd-MM-yyyy): ");
                            releaseDate = Console.ReadLine();
                            // CONTROLLO CAMPO VUOTO
                            if (string.IsNullOrWhiteSpace(releaseDate))
                            {
                                Console.WriteLine("La data di rilascio non può essere vuota. Riprova.");
                                continue;
                            }
                            // CONTROLLO FORMATO DATA
                            if (DateTime.TryParseExact(releaseDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Formato data non valido. Inserisci la data nel formato dd-MM-yyyy. Riprova.");
                            }
                        }
                        // CREAZIONE DI CRETED AT E UPDATED AT
                        DateTime createdAt = DateTime.Now;
                        DateTime updatedAt = DateTime.Now;
                        // ID DELLA SOFTWARE HOUSE E RISPETTIVO CONTROLLO
                        Console.Write("ID della software house: ");
                        int softwareHouseId;
                        while (!int.TryParse(Console.ReadLine(), out softwareHouseId))
                        {
                            Console.WriteLine("ID non valido. Riprova.");
                            Console.Write("ID della software house: ");
                        }
                        // INSERIMENTO DEL GIOCO NELLA TABELLA
                        VideogameMenager.InsertVideogame(name, overview, releaseDate, createdAt, updatedAt, softwareHouseId);
                        break;
                    
                    // RICERCA GIOCO TRAMITE ID
                    case 2:
                        Console.Write("Inserisci l'ID per cercare il videogioco: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var videogame = manager.GetVideogameById(id);
                            if (videogame != null)
                            {
                                Console.WriteLine($"Videogioco trovato: {videogame.Name} \nRilasciato il: {videogame.ReleaseDate}");
                            }
                            else
                            {
                                Console.WriteLine("Nessun videogioco trovato con l'ID specificato.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inserito non valido. Riprova con attenzione.");
                        }
                        break;

                    // RICERCA DEL VIDEOGIOCO CON IL NOME O PARTE DI ESSO
                    case 3:
                        Console.Write("Inserisci il nome del videogioco o parte di esso da cercare: ");
                        string searchGame = Console.ReadLine();
                        var gamesFound = manager.GetVideogamesByName(searchGame);
                        if (gamesFound.Count > 0)
                        {
                            Console.WriteLine("Videogiochi trovati:");
                            foreach (var game in gamesFound)
                            {
                                Console.WriteLine($"Nome: {game.Name} \nDesscrizione: {game.Overview} \nData di rilascio: {game.ReleaseDate}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nessun videogioco trovato in base a ciò che hai scritto.");
                        }
                        break;

                    

                    case 5:
                        Console.WriteLine("Programma chiuso con usccesso. Ciao ciao!");
                        break;

                    default:
                        Console.WriteLine("Oops! Forse hai digidato qualcosa di errato. Riprova.");
                        break;
                }

            } while (choice != 5);
        }
    }
}
