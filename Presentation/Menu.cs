using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

class Menu
{
    private int SelectedIndex;
    private string[] Options;
    private string Prompt;

    public Menu(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectedIndex = 0; // Geselecteerde optie begint bij de eerste optie
    }

    private void DisplayOptions()
    {
        WriteLine(Prompt); // Toon de prompt
        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix;

            if (i == SelectedIndex)
            {
                prefix = "*"; // Voeg een asterisk toe aan de geselecteerde optie
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White; // Geef de geselecteerde optie een witte achtergrondkleur
            }
            else
            {
                prefix = " ";
                BackgroundColor = ConsoleColor.Black; 
                ForegroundColor = ConsoleColor.White; // Geef de niet-geselecteerde opties een zwarte achtergrondkleur
            }

            WriteLine($"{prefix} << {currentOption} >>"); // Toon de opties met de prefix en pijltjes eromheen
        }
        ResetColor(); // Reset de kleuren van de console
    }
    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Clear(); // Maak de console leeg
            DisplayOptions(); // Toon de beschikbare opties
            ConsoleKeyInfo keyInfo = ReadKey(true); // Wacht op een toetsaanslag
            keyPressed = keyInfo.Key; // Haal de toets die is ingedrukt op


            if (keyPressed == ConsoleKey.UpArrow) // Ga naar de vorige optie als de pijl-omhoog toets wordt ingedrukt
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = Options.Length - 1; // Ga terug naar de laatste optie als de eerste optie geselecteerd is en de pijl-omhoog toets wordt ingedrukt
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow) // Ga naar de volgende optie als de pijl-omlaag toets wordt ingedrukt
            {
                SelectedIndex++;
                if (SelectedIndex == Options.Length)
                {
                    SelectedIndex = 0; // Ga terug naar de eerste optie als de laatste optie geselecteerd is en de pijl-omlaag toets wordt ingedrukt
                }
            }

        } while (keyPressed != ConsoleKey.Enter); // Blijf doorgaan totdat de Enter-toets wordt ingedrukt

        return SelectedIndex; // Geef de index van de geselecteerde optie terug
    }
}