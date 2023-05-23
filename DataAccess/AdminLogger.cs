static class AdminLogger
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/adminLog.csv"));

    public static void LogAdminAddFilm(string filmname, string filmdate, string filmtime, string filmroom)
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft een film toegevoegd met als naam: {filmname}, datum: {filmdate}, tijd: {filmtime} en in zaal: {filmroom}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminRemoveFilm(string filmname)
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft de film: {filmname} op non-actief gezet.";

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
    public static void LogAdminChangeFilmname(string filmname, string newFilmname)
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft de filmnaam van: {filmname} veranderd naar: {newFilmname}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminChangeFilmDescription(string filmname, string filmDescription, string newFilmDescription) //naam van functie aanpassen wanneer functie aangemaakt is.
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} de beschrijving veranderd van: {filmDescription} naar: {newFilmDescription}";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminChangeFilmDate(string filmname, string filmDate, string newFilmDate) //naam van functie aanpassen wanneer functie aangemaakt is.
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} de datum aangepast van: {filmDate} naar: {newFilmDate}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminChangeFilmTime(string filmname, string filmTime, string newFilmTime) //naam van functie aanpassen wanneer functie aangemaakt is.
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} de tijd aangepast van: {filmTime} naar: {newFilmTime}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }

    public static void LogAdminChangeFilmRoom(string filmname, string filmRoom, string newFilmRoom) //naam van functie aanpassen wanneer functie aangemaakt is.
    {
        // Neerzetten wat er precies gelogd moet worden.
        string Adminname = AccountsLogic.CurrentAccount.FullName;
        string data = $"Admin {Adminname} heeft bij de film: {filmname} de zaal aangepast van: {filmRoom} naar: {newFilmRoom}.";

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(string.Join(",", data));
        }
    }
}