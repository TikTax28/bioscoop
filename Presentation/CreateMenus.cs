using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class CreateMenus
{
    public void Begin()
    {
        RunMainMenu();
    }

    private void RunMainMenu()
    {
        string prompt = @"Welkom bij het bioscoopreserveringssysteem!
Met dit reserveringssysteem kunt u door de nieuwste filmlijsten bladeren, 
uw gewenste films, data, tijden en stoelen selecteren en een reservering maken. 
De kaart(en) en de factuur worden na de betaling per email naar u verzonden.
Volg de aanwijzingen op dit scherm en ons systeem zal u door de rest leiden.";

        string[] options = {"Inloggen", "Ga door zonder account", "Maak een account aan", "Exit"};
        Menu mainMenu = new Menu(prompt, options);
        int SelectedIndex = mainMenu.Run();

        switch (SelectedIndex)
        {
            case 0:
                    Clear();
                    LogIn();
                    break;
            case 1:
                    Continue();
                    break;
            case 2:
                    CreateAcc();
                    break;
            case 3:
                    ExitGame();
                    break;
            default:
                    break;
        }
    }

    private void ExitGame()
    {
        WriteLine("\nPress any key to exit...");
        ReadKey(true);
        Environment.Exit(0);
    }

    private void LogIn()
    {
        UserLogin.Start();
        Clear();
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
                    Films();
                    break;
            case 1:
                    Reservations();
                    break;
            case 2:
                    LogOut();
                    break;
            case 3:
                    ExitGame();
                    break;
            default:
                    break;
        }
    }

    private void LogOut()
    {
        Clear();
        RunMainMenu();
    }

    private void Films()
    {
        string prompt = "Selecter een film en klik op ENTER om te bevestigen";

        string[] options = {"Film1", "Film2", "Film3", "Film4"};
        Menu Films = new Menu(prompt, options);
        int SelectedIndex = Films.Run();
    }

    private void Reservations()
    {
        Clear();
        WriteLine("Geen reserveringen");
    }

    private void Continue()
    {
        Clear();
    }

    private void CreateAcc()
    {
        Clear();
        WriteLine("Gebruikersnaam");
        WriteLine("Wachtwoord");
    }
}