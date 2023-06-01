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

        string[] options = {"Inloggen of registreren", "Ga door zonder account", "Contact", "Exit"};
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
                Clear();
                Contact();
                break;
            case 3:
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
        string prompt = "";
        try
        {
            prompt = $"Welkom {AccountsLogic.CurrentAccount.FullName}";
        }
        catch
        {
            prompt = $"Welkom gastgebruiker";
        }


        string[] options = {"Films", "Uitloggen", "Exit"};
        Menu logMenu = new Menu(prompt, options);
        int SelectedIndex = logMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                films.FilmMenu();
                break;
            case 1:
                LogOut();
                break;
            case 2:
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

    public void Contact()
    {
    string prompt = " Onze bioscoop is gevestigd in Rotterdam en heeft 3 moderne zalen en 950 comfortabele zitplaatsen.\r\n " +
    "Het filmaanbod is heel breed en ons personeel staat altijd klaar om je te helpen.\r\n " +
    "Maak je filmuitje compleet met een snack die in de bioscoop beschikbaar zijn.\r\n " +
    "De bioscoop is met de fiets, het openbaar vervoer of met de auto goed bereikbaar.\r\n " +
    "Parkeren kan voordelig rondom de bioscoop en de trein- en bushalte zijn op loopafstand van de bioscoop.\r\n " +
    "\r\n"+
    "Contactgegevens\r\n" +
    "Telefoon: 063729462\r\n" +
    "E-mail: bioscoopRotterdam@gmail.com\r\n" +
    "Website: www.bioscoopRotterdam.nl";
    string[] options = {"Terug"};
        Menu logMenu = new Menu(prompt, options);
        int SelectedIndex = logMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                MainMenu();
                break;
        }
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