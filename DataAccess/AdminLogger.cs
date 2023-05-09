static class AdminLogger
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/adminLog.csv"));

    public static void LogAdminAddFilm(string filmname, string filmdate, string filmtime)
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft een film toegevoegd met als naam: {filmname}, datum: {filmdate} en als tijd: {filmtime}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminRemoveFilm(string filmname)
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft de film: {filmname} verwijderd.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminChangeFilmDescription(string filmname) //naam van functie aanpassen wanneer functie aangemaakt is.
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} een aanpassing gedaan bij de beschrijving van de film.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }
    /*
    public static void LogSeatsAdmin() //functie voor wanneer admin stoelen aanpast.
    {
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} op: {filmdate} om: {filmtime} een aanpassing gedaan bij de volgende stoelen: {listofchairs}.";

        
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }
    */
}