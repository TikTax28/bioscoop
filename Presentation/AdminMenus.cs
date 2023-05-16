using static System.Console;

class AdminMenus
{
    public void AdminMenu()
    {
        string prompt = "Selecteer een optie en klik op ENTER om te bevestigen";

        string[] options = {"Films beheren", "Stoelen beheren", "Snacks beheren", "Beheer klantgegevens", "Rapport printen", "Uitloggen"};
        Menu Admin = new Menu(prompt, options);
        int SelectedIndex = Admin.Run();

        switch (SelectedIndex)
        {
            case 0:
                FilmsAdmin();
                break;
            case 1:
                SeatsAdmin();
                break;
            case 2:
                SnacksAdmin();
                break;
            case 3:
                UserDetails();
                break;
            case 4:
                Rapport();
                break;
            case 5:
                CreateMenus menus = new CreateMenus();
                menus.LogOut();
                break;
            default:
                break;
        }
    }

    public void FilmsAdmin()
    {
        string prompt = "Selecteer een optie en klik op ENTER om te bevestigen";

        string[] options = {"Films toevoegen", "Films verwijderen", "Film informatie aanpassen", "Terug"};
        Menu Admin = new Menu(prompt, options);
        int SelectedIndex = Admin.Run();

        switch (SelectedIndex)
        {
            case 0:
                AdminAddFilm();
                break;
            case 1:
                AdminRemoveFilm();
                break;
            case 2:
                AdminInfoFilm();
                break;
            case 3:
                AdminMenu();
                break;
            default:
                break;
        }
    }

    public void AdminAddFilm()
    {
        FilmsLogic filmslogic = new FilmsLogic();
        Clear();

        string ?filmname;
        string ?filmdescription;
        string ?filmdate;
        string ?filmtime;
        string ?filmroom;

        WriteLine("Voeg een film toe");
        WriteLine();
        while (true)
            {
            WriteLine("Filmnaam: ");
            filmname = ReadLine();
            if (filmslogic.CheckFilmName(filmname)) break;
        }
        while (true)
            {
            WriteLine("Film beschrijving: ");
            filmdescription = ReadLine();
            if (filmslogic.CheckFilmDescription(filmdescription)) break;
        }
        while (true)
        {
            WriteLine("Filmdatum: ");
            filmdate = ReadLine();
            if (filmslogic.CheckFilmDate(filmdate)) break;
        }
        while (true)
        {
            WriteLine("Filmtijd: ");
            filmtime = ReadLine();
            if (filmslogic.CheckFilmTime(filmtime)) break;
        }
        WriteLine();
        while (true)
        {
            string prompt = "Kies een filmzaal: ";
            // There are only 3 rooms available
            string[] options = {"Zaal 1 (150 stoelen)", "Zaal 2 (300 stoelen)", "Zaal 3 (500 stoelen)"};
            Menu Films = new Menu(prompt, options);
            int SelectedIndex = Films.Run();
            switch (SelectedIndex)
            {
                case 0:
                    filmroom = "1";
                    break;
                case 1:
                    filmroom = "2";
                    break;
                case 2:
                    filmroom = "3";
                    break;
                default:
                    filmroom = "";
                    break;
            }
            if (filmslogic.CheckFilmRoom(filmroom)) break;
        }
        // Add the film
        filmslogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        //Functie aanroepen die alles logged wat er gebeurd.
        AdminLogger.LogAdminAddFilm(filmname, filmdate, filmtime, filmroom);
        Clear();
        var temp = new CreateMenus();
        FilmsAdmin();
    
    }

    public void AdminRemoveFilm()
    {
        Clear();
        string prompt = "Selecter een film en klik op ENTER om te verwijderen";
        FilmsLogic filmsLogic = new FilmsLogic();

        string[] options = new string[0];

        // Get all the films
        foreach (FilmModel allFilms in filmsLogic.GetAllFilms())
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = allFilms.filmName;
        }

        // increse the array size
        Array.Resize(ref options, options.Length + 2);
        for (int i = options.Length - 2; i >= 0; i--)
        {
            options[i + 1] = options[i];
        }

        // Add "Terug" at the end
        options[options.Length - 1] = "Terug";

        // Remove the duplicates using hashset
        HashSet<string> hashSet = new HashSet<string>(options);
        // Turn it back to an array
        options = hashSet.ToArray();

        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();

        if (SelectedIndex < options.Length - 1)
        {
            Clear();
            prompt = @"Weet je zeker dat je wilt verwijderen?";

            string[] options2 = {"Ja", "Nee, ga terug"};
            Menu menu = new Menu(prompt, options2);
            int SelectedIndex2 = menu.Run();

            switch (SelectedIndex2)
            {
                case 0:
                        // Use the name of the selected option to get the film info
                        while (filmsLogic.GetByName(options[SelectedIndex]) != null)
                        {
                            FilmModel filmToDelete = filmsLogic.GetByName(options[SelectedIndex]);
                            string filmToDeleteForLog = filmToDelete.filmName;
                            filmsLogic.DeleteFilm(filmToDelete);
                            //Functie aanroepen die alles logged wat er gebeurd.
                            AdminLogger.LogAdminRemoveFilm(filmToDeleteForLog);
                        }
                        AdminRemoveFilm();
                        break;
                case 1:
                        AdminRemoveFilm();
                        break;
                default:
                        break;
            }
        }
        else
        {
            FilmsAdmin();
        }
    }

    public void AdminInfoFilm()
    {
        Clear();
        // Laad de gegevens uit JSON-bestand
        var filmsLogic = new FilmsLogic();
        var films = filmsLogic.GetAllFilms();

        // Vraag om  ID van de film die moet worden bijgewerkt
        Console.Write("Voer het ID in van de film die moet worden bijgewerkt: \n");
        var id = int.Parse(Console.ReadLine());

        foreach(FilmModel film in films)
        {
            if (film.Id == id)
            {
                Console.Write("Voer de nieuwe beschrijving in: ");
                var newDescription = Console.ReadLine();
                film.filmDescription = newDescription;
                FilmsAccess.WriteAll(films);
                AdminLogger.LogAdminChangeFilmDescription(film.filmName, newDescription);
            }
        }
        FilmsAdmin();
    }

    private void ChangeFilm()
    {
        
    }

     private void SeatsAdmin()
    {
        
    }

     private void SnacksAdmin()
    {
        
    }

     private void UserDetails()
    {
        
    }

     private void Rapport()
    {
        
    }
}