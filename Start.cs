using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class Start
{
    public void Begin()
    {
        RunMainMenu();
    }

    private void RunMainMenu()
    {
        string prompt = @"Welkom bij het bioscoopreserveringssysteem! \n
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
        string prompt = "Inloggen";
        string[] options = {"Gebruikersnaam", "Wachtwoord"};
        Menu LogIn = new Menu(prompt, options);
        int SelectedIndex = LogIn.Run();
        
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