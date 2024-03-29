using static System.Console;

class AdminMenus
{
    public void AdminMenu()
    {
        string prompt = "Selecteer een optie en klik op ENTER om te bevestigen";

        string[] options = {"Films beheren", "Uitloggen"};
        Menu Admin = new Menu(prompt, options);
        int SelectedIndex = Admin.Run();

        switch (SelectedIndex)
        {
            case 0:
                FilmsAdmin();
                break;
            case 1:
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
                            FilmModel filmToDelete = filmsLogic.GetByName(options[SelectedIndex]);
                            string filmToDeleteForLog = filmToDelete.filmName;
                            filmsLogic.DeleteFilm(filmToDelete);
                            //Functie aanroepen die alles logged wat er gebeurd.
                            AdminLogger.LogAdminRemoveFilm(filmToDeleteForLog);
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

        Dictionary<string, List<FilmModel>> dateToTimes = new Dictionary<string, List<FilmModel>>();
        foreach (FilmModel film in filmsLogic.GetAllFilms())    
        {
            if (film.filmName == selectedFilm.filmName)
            {
                string date = film.filmDate;
                if (!dateToTimes.ContainsKey(date))
                {
                    dateToTimes[date] = new List<FilmModel>();
                }
                dateToTimes[date].Add(film);
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
                List<FilmModel> filmsForSelectedDate = dateToTimes[selectedDate];

                string[] timeOptions = filmsForSelectedDate.Select(f => f.filmTime).ToArray();
                timeOptions = timeOptions.Append("Terug").ToArray();

                int selectedTimeIndex = new Menu($"{selectedDate}\nKlik een tijd en klik op ENTER", timeOptions).Run();

                if (selectedTimeIndex >= 0 && selectedTimeIndex < timeOptions.Length - 1)
                {
                    string selectedTime = timeOptions[selectedTimeIndex];
                    FilmModel selectedFilmModel = filmsForSelectedDate.FirstOrDefault(f => f.filmTime == selectedTime);

                    if (selectedFilmModel != null)
                    {
                        selectedFilm.filmTime = selectedTime;
                        ChangeFilmInfo(selectedFilmModel);
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                    }
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
        $"Tijd: {selectedFilm.filmTime}"
        };
        Menu info = new Menu(prompt, options);
        int SelectedIndex = info.Run();

        switch (SelectedIndex)
        {
        case 0:
            // Change film name
            while (true)
            {
                Clear();
                WriteLine("Voer de nieuwe filmnaam in: ");
                string newFilmName = ReadLine();
                string oldFilmName = selectedFilm.filmName;
                if (filmsLogic.CheckFilmName(newFilmName))
                {
                    selectedFilm.filmName = newFilmName;
                    //Functie aanroepen die alles logged wat er gebeurd.
                    AdminLogger.LogAdminChangeFilmname(oldFilmName, newFilmName);
                    break;
                }

            }
            break;
        case 1:
            // Change film description
            while (true)
            {
                Clear();
                WriteLine("Voer de nieuwe beschrijving in: ");
                string newFilmDescription = ReadLine();
                string oldFilmDescription = selectedFilm.filmDescription;
                if (filmsLogic.CheckFilmDescription(newFilmDescription))
                {
                    selectedFilm.filmDescription = newFilmDescription;
                    AdminLogger.LogAdminChangeFilmDescription(selectedFilm.filmName, oldFilmDescription, newFilmDescription);
                    break;
                }
            }
            break;
        case 2:
            // Change film date
            while (true)
            { 
                Clear();
                WriteLine("Voer de nieuwe datum in: ");
                string newFilmDate = ReadLine();
                string oldFilmDate = selectedFilm.filmDate;
                if (filmsLogic.CheckFilmDate(newFilmDate))
                {
                    selectedFilm.filmDate = newFilmDate;
                    AdminLogger.LogAdminChangeFilmDescription(selectedFilm.filmName, oldFilmDate, newFilmDate);
                    break;
                }
            }
            break;
        case 3:
            // Change film time
            while (true)
            {
                Clear();
                WriteLine("Voer de nieuwe tijd in: ");
                string newFilmTime = ReadLine();
                string oldFilmTime = selectedFilm.filmTime;
                if (filmsLogic.CheckFilmTime(newFilmTime))
                {
                    selectedFilm.filmTime = newFilmTime;
                    AdminLogger.LogAdminChangeFilmTime(selectedFilm.filmName, oldFilmTime, newFilmTime);
                    break;
                }
            }
            break;
        }

        // Save the updated film information
        filmsLogic.UpdateFilm(selectedFilm);

        // Go back to film admin menu
        AdminInfoFilm();
    }
}