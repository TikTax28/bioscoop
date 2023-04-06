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
        string[] options = new string[0];

        foreach (FilmModel allFilms in filmsLogic.GetAllFilms())
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = allFilms.filmName;
        }

        // Shift all elements one place to the right
        Array.Resize(ref options, options.Length + 1);

        // Add "Terug" at the end
        options[options.Length - 1] = "Terug";

        HashSet<string> hashSet = new HashSet<string>(options);
        options = hashSet.ToArray();
        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();
        if (SelectedIndex < options.Length - 1)
        {
            FilmTimes(options[SelectedIndex]);
        }
        else
        {
            LoggedInMenu();
        }
    }

    private void FilmTimes(string filmName)
    {
        Clear();
        FilmsLogic filmsLogic = new FilmsLogic();
        string prompt2 = "Kies een tijd en klik op ENTER";


        // Create a dictionary that maps each date to a list of times for the selected film
        Dictionary<string, List<string>> dateToTimes = new Dictionary<string, List<string>>();
        foreach (FilmModel film in filmsLogic.GetAllFilms())
        {
            if (film.filmName == filmName)
            {
                if (!dateToTimes.ContainsKey(film.filmDate))
                {
                    dateToTimes.Add(film.filmDate, new List<string>());
                }
                dateToTimes[film.filmDate].Add(film.filmTime);
            }
        }

        // Create options array for the date menu
        string[] dateOptions = dateToTimes.Keys.ToArray();
        dateOptions = dateOptions.Append("Terug").ToArray();

        while (true)
        {
            int selectedDateIndex = new Menu($"{prompt2}", dateOptions).Run() - 1; // subtract one to get index

            if (selectedDateIndex >= 0 && selectedDateIndex < dateOptions.Length - 1)
            {
                // Show time options for the selected date
                string selectedDate = dateOptions[selectedDateIndex];
                List<string> timesForSelectedDate = dateToTimes[selectedDate];
                int selectedTimeIndex = new Menu($"{selectedDate}\nKlik een tijd en klik op ENTER", timesForSelectedDate.ToArray()).Run() - 1; // subtract one to get index
                
                if (selectedTimeIndex >= 0 && selectedTimeIndex < timesForSelectedDate.Count)
                {
                    // valid time selected, do something
                    string selectedTime = timesForSelectedDate[selectedTimeIndex];
                    // ...
                }
                else
                {
                    // Invalid time index selected, go back to date selection
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


    private void Reservations()
    {
        Clear();
        WriteLine("Geen reserveringen");
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
        Clear();
        WriteLine("Voeg een film toe");
        WriteLine();
        WriteLine("Filmnaam: ");
        string ?filmname = ReadLine();
        WriteLine("Filmdatum: ");
        string ?filmdate = ReadLine();
        WriteLine("Filmtijd: ");
        string ?filmtime = ReadLine();
        FilmsLogic filmslogic = new FilmsLogic();
        if (filmslogic.CheckFilm(filmname, filmdate, filmtime))
        {
            filmslogic.AddFilm(filmname, filmdate, filmtime);
            Clear();
            var temp = new CreateMenus();
            temp.FilmsAdmin();
        }
        else
        {
            WriteLine("Probeer opnieuw.");
            ReadLine();
            Clear();
            var temp = new CreateMenus();
            temp.FilmsAdmin();
        }
    }

    public void AdminRemoveFilm()
    {
        Clear();
        string prompt = "Selecter een film en klik op ENTER om te verwijderen";
        FilmsLogic filmsLogic = new FilmsLogic();

        string[] options = new string[0];

        foreach (FilmModel allFilms in filmsLogic.GetAllFilms())
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = allFilms.filmName;
        }

        // Shift all elements one place to the right
        Array.Resize(ref options, options.Length + 2);
        for (int i = options.Length - 2; i >= 0; i--)
        {
            options[i + 1] = options[i];
        }

        // Add "Terug" at the end
        options[options.Length - 1] = "Terug";

        HashSet<string> hashSet = new HashSet<string>(options);
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
                        while (filmsLogic.GetByName(options[SelectedIndex]) != null)
                        {
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