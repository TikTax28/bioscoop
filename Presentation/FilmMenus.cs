using static System.Console;


class FilmMenus
{
    public void FilmMenu()
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
            CreateMenus menus = new CreateMenus();
            menus.LoggedInMenu(); // Ga terug naar het ingelogde menu
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
                int selectedTimeIndex = new Menu($"{selectedDate}\nKlik een tijd en klik op ENTER", timesForSelectedDate.ToArray()).Run();
                
                if (selectedTimeIndex >= 0 && selectedTimeIndex < timesForSelectedDate.Count)
                {
                    // valid time selected, show seats..
                    string selectedTime = timesForSelectedDate[selectedTimeIndex];
                    FilmSeats(selectedFilm.filmName, selectedDate, selectedTime);
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

    public void FilmSeats(string selectedFilmName, string selectedDate, string selectedTime)
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
                case ConsoleKey.Enter:
                    InfoFilmReservation(reservedSeats, selectedFilmName, selectedDate, selectedTime);
                    break;
            }
            WriteLine();
        }
    }

    private void InfoFilmReservation(List<string> reservedSeats, string selectedFilmName, string selectedDate, string selectedTime)
    {
        Clear();
        string prompt = $"Info film:\nFilmnaam: {selectedFilmName}\nFilmdatum {selectedDate}\nFilmtijd: {selectedTime}";
        string[] options = {"Reserveren", "Ga terug naar stoelen kiezen"};
        Menu menu = new Menu(prompt, options);
        int SelectedIndex = menu.Run();

        switch (SelectedIndex)
        {
            case 0:
                BookingLogic bookinglogic = new BookingLogic();
                bookinglogic.AddReservation(reservedSeats, selectedDate, selectedTime);
                FilmMenu();
                break;
            case 1:
                break;

        }
    }
}