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
        var allActiveFilms = filmsLogic.filmsOnlyActive();

        // Get all the films
        foreach (FilmModel film in allActiveFilms)
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = film.filmName;
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
                        // while (filmsLogic.GetByName(options[SelectedIndex]) != null)
                        // {
                            FilmModel filmToDelete = filmsLogic.GetByName(options[SelectedIndex]);
                            string filmToDeleteForLog = filmToDelete.filmName;
                            filmsLogic.DeleteFilm(filmToDelete);
                            //Functie aanroepen die alles logged wat er gebeurd.
                            AdminLogger.LogAdminRemoveFilm(filmToDeleteForLog);
                        // }
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
        string prompt = "Selecter een film en klik op ENTER";
        FilmsLogic filmsLogic = new FilmsLogic();
        var allFilms = filmsLogic.GetAllFilms();
        string[] options = new string[0];

        foreach (FilmModel film in allFilms) // Loop door alle films in de database
        {
            Array.Resize(ref options, options.Length + 1); // Vergroot de grootte van de opties-array met 1
            options[options.Length - 1] = film.filmName; // Voeg de naam van de film toe aan de opties-array
        }

        Array.Resize(ref options, options.Length + 1);
        options[options.Length - 1] = "Terug"; // Voeg de "Terug" optie toe aan de opties-array

        HashSet<string> hashSet = new HashSet<string>(options); // Verwijder dubbele opties uit de opties-array
        options = hashSet.ToArray(); // Zet de opties-array om naar een HashSet en vervolgens terug naar een array
        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run(); // Voer de menu uit en sla de geselecteerde index op
        if (SelectedIndex < options.Length - 1) // Als de geselecteerde index niet de "Terug" optie is
        {
            FilmModel selectedFilm = allFilms.First(f => f.filmName == options[SelectedIndex]);
            AdminFilmTimes(selectedFilm); // Roep de FilmTimes methode aan met de geselecteerde filmnaam
        }
        else
        {
            FilmsAdmin();
        }
    }

    private void AdminFilmTimes(FilmModel selectedFilm)
    {
        Clear();
        FilmsLogic filmsLogic = new FilmsLogic();
        string prompt2 = selectedFilm.filmDescription;
        
        Dictionary<string, List<string>> dateToTimes = new Dictionary<string, List<string>>(); // Maak een dictionary om elke datum te koppelen aan een lijst met beschikbare tijden
        foreach (FilmModel film in filmsLogic.GetAllFilms())
        {
            if (film.filmName == selectedFilm.filmName)
            {
                if (!dateToTimes.ContainsKey(film.filmDate)) // Als de datum nog niet aan het woordenboek is toegevoegd, voeg dan een nieuwe lege lijst toe
                {
                    dateToTimes.Add(film.filmDate, new List<string>());
                }
                dateToTimes[film.filmDate].Add(film.filmTime); // Voeg de tijd toe aan de lijst met tijden voor deze datum
            }
        }

        string[] dateOptions = dateToTimes.Keys.ToArray();
        dateOptions = dateOptions.Append("Terug").ToArray();

        while (true)
        {
            int selectedDateIndex = new Menu($"{prompt2}", dateOptions).Run();

            if (selectedDateIndex >= 0 && selectedDateIndex < dateOptions.Length - 1)
            {
                string selectedDate = dateOptions[selectedDateIndex];
                List<string> timesForSelectedDate = dateToTimes[selectedDate];

                string[] timeOptions = timesForSelectedDate.ToArray();
                timeOptions = timeOptions.Append("Terug").ToArray();

                int selectedTimeIndex = new Menu($"{selectedDate}\nKlik een tijd en klik op ENTER", timeOptions).Run();

                if (selectedTimeIndex >= 0 && selectedTimeIndex < timeOptions.Length - 1)
                {
                    string selectedTime = timeOptions[selectedTimeIndex];
                    selectedFilm.filmTime = selectedTime; // Update the selected film's time
                    ChangeFilmInfo(selectedFilm);
                }
                else if (selectedTimeIndex == timeOptions.Length - 1)
                {
                    AdminInfoFilm();
                    break;
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                }
            }
            else if (selectedDateIndex == dateOptions.Length - 1)
            {
                AdminInfoFilm();
                break;
            }
            else
            {
                Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
            }
        }
    }
    

    private void ChangeFilmInfo(FilmModel selectedFilm)
    {
        Clear();
        FilmsLogic filmsLogic = new FilmsLogic();
        string prompt = "Selecteer welke data u wilt veranderen met ENTER";
        string[] options = {
        $"Filmnaam: {selectedFilm.filmName}",
        $"Beschrijving: {selectedFilm.filmDescription}",
        $"Datum: {selectedFilm.filmDate}",
        $"Tijd: {selectedFilm.filmTime}",
        $"Zaal: {selectedFilm.filmRoom}"
        };
        Menu info = new Menu(prompt, options);
        int SelectedIndex = info.Run();

        switch (SelectedIndex)
        {
        case 0:
            // Change film name
            Clear();
            WriteLine("Voer de nieuwe filmnaam in: ");
            string newFilmName = ReadLine();
            selectedFilm.filmName = newFilmName;
            break;
        case 1:
            // Change film description
            Clear();
            WriteLine("Voer de nieuwe beschrijving in: ");
            string newFilmDescription = ReadLine();
            selectedFilm.filmDescription = newFilmDescription;
            break;
        case 2:
            // Change film date
            Clear();
            WriteLine("Voer de nieuwe datum in: ");
            string newFilmDate = ReadLine();
            selectedFilm.filmDate = newFilmDate;
            break;
        case 3:
            // Change film time
            Clear();
            WriteLine("Voer de nieuwe tijd in: ");
            string newFilmTime = ReadLine();
            selectedFilm.filmTime = newFilmTime;
            break;
        case 4:
            // Change film room
            Clear();
            WriteLine("Voer de nieuwe zaal in: ");
            string newFilmRoom = ReadLine();
            selectedFilm.filmRoom = newFilmRoom;
            break;
        }

        // Save the updated film information
        filmsLogic.UpdateFilm(selectedFilm);

        // Go back to film admin menu
        FilmsAdmin();

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