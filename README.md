# adonet-db-videogame

Vogliamo realizzare un programma che permetta di gestire i videogiochi che verranno utilizzati nei tornei.

Creiamo quindi una console app che all'avvio mostra un menu con la possibilità di :
- 1 inserire un nuovo videogioco
- 2 ricercare un videogioco per id
- 3 ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input
- 4 cancellare un videogioco
- 5 chiudere il programma

In base alla voce selezionata richiedere le informazioni necessarie per effettuare l'operazione desiderata.

Prevedere tutti i controlli di validazione dei dati (ad esempio non si può inserire un videogioco senza specificare il nome).
In caso di errori lanciare un'eccezione (che deve essere gestita a più alto livello).

Creare una classe VideogameManager che esponga tutti i metodi necessari al programma (es. InserisciVideogame(…), GetVideogameById(..), etc…).

Creare una classe Videogame da passare come parametro al metodo di inserimento nuovo videogioco e che venga restituita dai metodi di ricerca.
