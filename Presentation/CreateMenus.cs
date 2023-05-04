using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class CreateMenus
{
    public void Begin()
    {
        MainMenu();
    }

    private void MainMenu()
    {
        string prompt = @"Welkom bij het bioscoopreserveringssysteem!
Met dit reserveringssysteem kunt u door de nieuwste filmlijsten bladeren, 
uw gewenste films, data, tijden en stoelen selecteren en een reservering maken. 
De kaart(en) en de factuur worden na de betaling per email naar u verzonden.
Volg de aanwijzingen op dit scherm en ons systeem zal u door de rest leiden.";

        string[] options = {"Inloggen", "Ga door zonder account", "Exit"};
        Menu mainMenu = new Menu(prompt, options);
        int SelectedIndex = mainMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                Clear();
                LogIn();
                break;      
            case 1:
                Clear();
                Guest();
                break;
            case 2:
                Exit();
                break;
            default:
                break;
        }
    }

    public void LogIn()
    {
        string prompt = @"Kies om in te loggen of een nieuw account aan te maken";
        string[] options = {"Inloggen", "Maak een account aan", "Terug"};
        Menu LogIn = new Menu(prompt, options);
        int SelectedIndex = LogIn.Run();

        switch (SelectedIndex)
        {
            case 0:
                Clear();
                User.LogIn();
                break;
            case 1:
                Clear();
                User.CreateAcc();
                break;
            case 2:
                MainMenu();
                break;
            default:
                break;
        }
    }
    

    public void LoggedInMenu()
    {
        string prompt = @"Welkom bij het bioscoopreserveringssysteem!
Met dit reserveringssysteem kunt u door de nieuwste filmlijsten bladeren, 
uw gewenste films, data, tijden en stoelen selecteren en een reservering maken. 
De kaart(en) en de factuur worden na de betaling per email naar u verzonden.
Volg de aanwijzingen op dit scherm en ons systeem zal u door de rest leiden.";

        string[] options = {"Films", "Reserveringen", "Uitloggen", "Exit"};
        Menu logMenu = new Menu(prompt, options);
        int SelectedIndex = logMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                FilmMenu();
                break;
            case 1:
                Reservations();
                break;
            case 2:
                LogOut();
                break;
            case 3:
                Exit();
                break;
            default:
                break;
        }
    }

    private void Guest()
    {
        string prompt = @"Welkom bij het bioscoopreserveringssysteem!
Met dit reserveringssysteem kunt u door de nieuwste filmlijsten bladeren, 
uw gewenste films, data, tijden en stoelen selecteren en een reservering maken. 
De kaart(en) en de factuur worden na de betaling per email naar u verzonden.
Volg de aanwijzingen op dit scherm en ons systeem zal u door de rest leiden.";

        string[] options = {"Films", "Terug"};
        Menu logMenu = new Menu(prompt, options);
        int SelectedIndex = logMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                FilmMenu();
                break;
            case 1:
                MainMenu();
                break;
            default:
                break;
        }
    }

    private void LogOut()
    {
        Clear();
        MainMenu();
    }

    private void FilmMenu()
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
            FilmTimes(selectedFilm); // Roep de FilmTimes methode aan met de geselecteerde filmnaam
        }
        else
        {
            LoggedInMenu(); // Ga terug naar het ingelogde menu
        }


    }

    private void FilmTimes(FilmModel selectedFilm)
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
                // Toon het menu met tijdopties voor de geselecteerde datum
                string selectedDate = dateOptions[selectedDateIndex];
                List<string> timesForSelectedDate = dateToTimes[selectedDate];
                int selectedTimeIndex = new Menu($"{selectedDate}\nKlik een tijd en klik op ENTER", timesForSelectedDate.ToArray()).Run() - 1; // subtract one to get index
                
                if (selectedTimeIndex >= 0 && selectedTimeIndex < timesForSelectedDate.Count)
                {
                    // valid time selected, show seats..
                    string selectedTime = timesForSelectedDate[selectedTimeIndex];
                    // ...
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                }
            }
            else if (selectedDateIndex == dateOptions.Length - 1)
            {
                // Go back to previous menu when 'Terug' is selected
                FilmMenu();
                break;
            }
            else
            {
                Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
            }
        }

    }

    public void FilmSeats()
    {
        Clear();
        bool running = true;
        int currentRow = 0;
        int currentColumn = 0;
        int numRows = 10;
        int numColumns = 10;

        // Initialize unreserved seats
        bool[,] seats = new bool[numRows, numColumns];

        // Use list to keep track of the seats reserved
        List<string> reservedSeats = new List<string>();

        while (running) {
            Clear();
            WriteLine("Selecteer een stoel (gebruik de pijltjestoetsen om te bewegen, spatiebalk om te reserveren of Esc om te verlaten):");
            WriteLine();

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numColumns; col++)
                {
                    if (row == currentRow && col == currentColumn)
                    {
                        ForegroundColor = ConsoleColor.Green;
                    }
                    else if (seats[row, col])
                    {
                        ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.White;
                    }

                    string seatNumber = "";

                    // Calculate seat number based on row and column
                    if (col < 9)
                    {
                        seatNumber += (char)('A' + row);
                        seatNumber += (col + 1).ToString();
                    }
                    else
                    {
                        seatNumber += (char)('A' + row + 1);
                        seatNumber += (col - 8).ToString();
                    }

                    Write((seats[row, col]) ? seatNumber + " " : seatNumber + " ");
                }
                WriteLine();
            }
            WriteLine();
            // Print out reserved seats
            WriteLine($"Gereserveerde stoelen: {string.Join(", ", reservedSeats)}");

            ConsoleKeyInfo keyInfo = ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    currentRow = Math.Max(0, currentRow - 1);
                    break;
                case ConsoleKey.DownArrow:
                    currentRow = Math.Min(numRows - 1, currentRow + 1);
                    break;
                case ConsoleKey.LeftArrow:
                    currentColumn = Math.Max(0, currentColumn - 1);
                    break;
                case ConsoleKey.RightArrow:
                    currentColumn = Math.Min(numColumns - 1, currentColumn + 1);
                    break;
                case ConsoleKey.Spacebar:
                    if (!seats[currentRow, currentColumn])
                    {
                        seats[currentRow, currentColumn] = true;
                        ForegroundColor = ConsoleColor.White;

                        string reservedSeat = "";

                        // Calculate reserved seat number based on row and column
                        if (currentColumn < 9)
                        {
                            reservedSeat += (char)('A' + currentRow);
                            reservedSeat += (currentColumn + 1).ToString();
                        }
                        else
                        {
                            reservedSeat += (char)('A' + currentRow + 1);
                            reservedSeat += (currentColumn - 8).ToString();
                        }

                        reservedSeats.Add(reservedSeat);

                        WriteLine($"Stoel {reservedSeat} gereserveerd.");
                    }
                    else
                    {
                        seats[currentRow, currentColumn] = false;
                        ForegroundColor = ConsoleColor.White;

                        string reservedSeat = "";

                        // Calculate reserved seat number based on row and column
                        if (currentColumn < 9)
                        {
                            reservedSeat += (char)('A' + currentRow);
                            reservedSeat += (currentColumn + 1).ToString();
                        }
                        else
                        {
                            reservedSeat += (char)('A' + currentRow + 1);
                            reservedSeat += (currentColumn - 8).ToString();
                        }

                        reservedSeats.Remove(reservedSeat);
                    }
                    break;
                case ConsoleKey.Escape:
                    WriteLine("Verlaten...");
                    running = false;
                    break;
            }
            WriteLine();
        }
    }

    private void Reservations()
    {
        Clear();
        WriteLine("Geen reserveringen");
    }


    public void Snacks()
    {
        string prompt = "Selecter een snack en klik op ENTER om te bevestigen";

        string[] options = {"Cola", "...", "...", "Terug"};
        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();

        switch (SelectedIndex)
        {
            case 0:
                Snacks();
                break;
            case 1:
                Snacks();
                break;
            case 2:
                Snacks();
                break;
            case 3:        
                break;
            default:
                break;
        }
    }

    private void Exit()
    {
        WriteLine("\nPress any key to exit...");
        ReadKey(true);
        Environment.Exit(0);
    }

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
                LogOut();
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
                //AdminInfoFilm();
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
        Clear();
        var temp = new CreateMenus();
        temp.FilmsAdmin();
    
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
                            // Delete the film that is selected
                            filmsLogic.DeleteFilm(filmsLogic.GetByName(options[SelectedIndex]));
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