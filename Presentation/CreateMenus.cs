using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class CreateMenus
{
    AdminMenus admin = new AdminMenus();
    FilmMenus films = new FilmMenus();
    

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
                films.FilmMenu();
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
                films.FilmMenu();
                break;
            case 1:
                MainMenu();
                break;
            default:
                break;
        }
    }

    public void LogOut()
    {
        Clear();
        MainMenu();
    }

    

    private void Reservations()
    {
        Clear();
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
}