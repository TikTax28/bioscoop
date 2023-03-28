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
        string prompt = "Selecter een film en klik op ENTER om te bevestigen";

        string[] options = {"Film1", "...", "...", "Terug"};
        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();

        switch (SelectedIndex)
        {
            case 0:
                Film1();
                break;
            case 1:
                break;
            case 2:
                break;
                case 3:
                LoggedInMenu();
                break;
            default:
                break;
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

    public void Film1()
    {
        string prompt = @"Cocain Bear";

        string[] options = {"Cocaine Bear is gebaseerd op een waargebeurd verhaal uit 1985 over een drugssmokkelaar, wiens vliegtuig neerstort, en de zwarte beer die de zoekgeraakte cocaïne verorbert.", "Verder", "Terug"};
        Menu logMenu = new Menu(prompt, options);
        int SelectedIndex = logMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                Clear();
                Film1();
                break;
            case 1:
                Clear();
                Snacks();
                break;
            case 2:
                Clear();
                FilmMenu();
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
                break;
            case 2:
                break;
            case 3:
                Film1();        
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

    private void Admin()
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

    private void FilmsAdmin()
    {
         Clear();
        string prompt = "Selecter een film en klik op ENTER om te verwerken";
        FilmsLogic filmsLogic = new FilmsLogic();

        string[] options = new string[0];

        foreach (FilmModel allFilms in filmsLogic.GetAllFilms())
        {
            Array.Resize(ref options, options.Length + 1);
            options[options.Length - 1] = allFilms.filmName;
        }
        int length = options.Length;
        Array.Resize(ref options, length + 1);
        options[length] = "Terug";

        HashSet<string> hashSet = new HashSet<string>(options);
        options = hashSet.ToArray();

        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();

        if (SelectedIndex < options.Length - 1)
        {
            RemoveFilm(options[SelectedIndex]);
        }
        else
        {
            // Returns you to the previous screen
        }
    }

    private void AddFilm()
    {

    }

    private void RemoveFilm(string filmname)
    {
        Clear();
        string prompt = @"Weet je zeker dat je wilt verwijderen?";

        string[] options = {"Ja", "Nee, ga terug"};
        Menu menu = new Menu(prompt, options);
        int SelectedIndex = menu.Run();

        switch (SelectedIndex)
        {
            case 0:
                    FilmsLogic filmsLogic = new FilmsLogic();
                    while (filmsLogic.GetByName(filmname) != null)
                    {
                        filmsLogic.DeleteFilm(filmsLogic.GetByName(filmname));
                    }
                    FilmsAdmin();
                    break;
            case 1:
                    FilmsAdmin();
                    break;
            default:
                    break;
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