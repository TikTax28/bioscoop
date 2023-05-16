using static System.Console;

class Program
{
    public static void Main()
    {
        CreateMenus newStart = new CreateMenus();
        AdminMenus admin = new AdminMenus();
        //admin.AdminMenu();
        newStart.Begin();
        //newStart.FilmSeats();
    }
}