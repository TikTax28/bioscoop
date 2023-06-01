using static System.Console;


class FilmMenus
{
    public void FilmMenu()
    {
        Clear();
        string prompt = "Selecter een film en klik op ENTER";
        FilmsLogic filmsLogic = new FilmsLogic();
        var allFilms = filmsLogic.filmsOnlyActive();
        string[] options = new string[0];

        foreach (var film in allFilms) // Loop door alle films in de database
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
                // Toon het menu met tijdopties voor de geselecteerde datum
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
                        FilmSeats(selectedFilmModel);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
                    }
                }
                else if (selectedTimeIndex == timeOptions.Length - 1)
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
            else if (selectedDateIndex == dateOptions.Length - 1)
            {
                FilmMenu();
                break;
            }
            else
            {
                Console.WriteLine("Ongeldige keuze. Probeer opnieuw.");
            }
        }
    }

    public void FilmSeats(FilmModel selectedFilm)
    {
        Clear();
        FilmsLogic filmslogic = new FilmsLogic();
        FilmModel film = filmslogic.GetByDateAndTime(selectedFilm.filmDate, selectedFilm.filmTime);

        bool running = true;
        int currentRow = 0;
        int currentColumn = 0;
        int numRows;
        int numColumns;
        string screen;

        // film room 1 with 150 seats
        if (film.filmRoom == "1")
        {
            screen = "\n -------------------- Screen --------------------\n";
            numRows = 10;
            numColumns = 15;
        }
        // film room 2 with 300 seats
        else if (film.filmRoom == "2")
        {
            screen = "\n ------------------------------ Screen ------------------------------\n";
            numRows = 15;
            numColumns = 20;
        }
        // film room 3 with 500 seats
        else if (film.filmRoom == "3")
        {
            screen = "\n ---------------------------------------- Screen ----------------------------------------\n";
            numRows = 20;
            numColumns = 25;
        }
        else
        {
            screen = "\n -------------------- Screen --------------------\n";
            numRows = 10;
            numColumns = 15;
        }

        // Initialize unreserved seats
        bool[,] seats = new bool[numRows, numColumns];
        // this array is for the seats reserved by OTHER PEOPLE
        bool[,] seatsAlreadyReserved = new bool[numRows, numColumns];

        BookingLogic bookinglogic = new BookingLogic();
        List<BookingModel> reservations = bookinglogic.GetListById(film.Id);
        foreach (BookingModel reservation in reservations)
        {
            foreach (SeatModel seat in reservation.Seats)
            {
                // gets the right letter based on index
                int row = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(seat.Row);
                // -1 so it changes to zero based index
                int column = seat.Seat - 1;

                if (row >= 0 && row < numRows && column >= 0 && column < numColumns)
                {
                    // Mark the seat as reserved
                    seatsAlreadyReserved[row, column] = true;
                }
            }
        }

        // Use list to keep track of the seats reserved
        List<string> reservedSeats = new List<string>();

        while (running) {
            Clear();
            WriteLine("Selecteer een stoel (gebruik de pijltjestoetsen om te bewegen, spatiebalk om te reserveren of Esc om te verlaten):");
            WriteLine();
            WriteLine(screen);

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
                    else if (seatsAlreadyReserved[row, col])
                    {
                        ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.White;
                    }

                    string seatNumber = "";

                    // Calculate seat number based on row and column
                    if (col < numColumns)
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

            if (reservedSeats.Count == 9)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine("\nJe kan niet meer dan 9 stoelen reserveren!");
                ResetColor();
            }

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
                    if (!seats[currentRow, currentColumn] && !seatsAlreadyReserved[currentRow, currentColumn] && reservedSeats.Count < 9)
                    {
                        seats[currentRow, currentColumn] = true;
                        ForegroundColor = ConsoleColor.White;

                        string reservedSeat = "";

                        // Calculate reserved seat number based on row and column
                        if (currentColumn < numColumns)
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
                        if (currentColumn < numColumns)
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
                    FilmMenu();
                    running = false;
                    break;
                case ConsoleKey.Enter:
                    if (reservedSeats.Count > 0)
                    InfoFilmReservation(reservedSeats, selectedFilm);
                    running = false;
                    break;
                default:
                    break;
            }
            WriteLine();
        }
    }

    private void InfoFilmReservation(List<string> reservedSeats, FilmModel selectedFilm)
    {
        Clear();
        // The film info
        string prompt = $"Info film:\nFilmnaam: {selectedFilm.filmName}\nFilmdatum {selectedFilm.filmDate}\nFilmtijd: {selectedFilm.filmTime}";
        // The options you can choose
        string[] options = {"Reserveren", "Ga terug naar stoelen kiezen"};
        Menu menu = new Menu(prompt, options);
        int SelectedIndex = menu.Run();

        switch (SelectedIndex)
        {
            case 0:
                BookingLogic bookinglogic = new BookingLogic(); 
                // Call the method AddReservation to do the logic
                bookinglogic.AddReservation(reservedSeats, selectedFilm.filmDate, selectedFilm.filmTime);
                // Once reservation is added, go back to the film menu
                CreateMenus menus = new CreateMenus();
                menus.LoggedInMenu(); // Ga terug naar het ingelogde menu
                break;
            case 1:
                FilmSeats(selectedFilm);
                break;
            default:
                break;

        }
    }
}
