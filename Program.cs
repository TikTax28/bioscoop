﻿using static System.Console;

class Program
{
    public static void Main()
    {
        AdminMenus admin = new AdminMenus();
        CreateMenus newStart = new CreateMenus();
        FilmMenus filmmenu = new FilmMenus();
        //admin.AdminMenu();
        newStart.Begin();
        //filmmenu.FilmMenu();
    }
}