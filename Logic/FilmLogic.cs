using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Console;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class FilmsLogic
{
    private List<FilmModel> _films;

    public FilmsLogic()
    {
        _films = FilmsAccess.LoadAll();
    }


    public void UpdateList(FilmModel film)
    {
        //Find if there is already an model with the same id
        int index = _films.FindIndex(s => s.Id == film.Id);

        if (index != -1)
        {
            //update existing model
            _films[index] = film;
        }
        else
        {
            //add new model
            _films.Add(film);
        }
        FilmsAccess.WriteAll(_films);
    }

    public FilmModel GetById(int id)
    {
        return _films.Find(i => i.Id == id);
    }
    public FilmModel GetByName(string name)
    {
        return _films.Find(i => i.filmName == name);
    }
    public FilmModel GetByDateAndTime(string date, string time)
    {
        FilmModel film = _films.Find(i => i.filmDate == date && i.filmTime == time);
        return film ?? null;
    }
    public FilmModel GetByDateAndTime(string date, string time, bool active)
    {
        FilmModel film = _films.Find(i => i.filmDate == date && i.filmTime == time && i.Active == true);
        return film ?? null;
    }

    public List<FilmModel> GetAllFilms()
    {
        return _films;
    }

    //returns list of movies of name
    public List<FilmModel> GetMoviesByName(string name){
        return _films.Where(x => x.filmName == name).ToList();
    }

    public void DeleteFilm(FilmModel film)
    {
        film.Active = false;
        FilmsAccess.WriteAll(_films);
    }
    public bool CheckFilmName(string filmname)
    {
        // check if filmname is empty
        if (filmname == "")
        {
            WriteLine("Vul een naam in");
            return false;
        }
        // check filmname character length
        if (filmname.Length > 50)
        {
            WriteLine("Film lengte moet minder dan 50 letters zijn!");
            return false;
        }
        return true;
    }
    public bool CheckFilmDescription(string filmdescription)
    {
        // check if filmname is empty
        if (filmdescription == "")
        {
            WriteLine("Vul een beschrijving in");
            return false;
        }
        // check filmname character length
        if (filmdescription.Length > 200)
        {
            WriteLine("Film beschrijving mag niet langer dan 200 karakters zijn zijn!");
            return false;
        }
        return true;
    }
    public bool CheckFilmDate(string filmdate)
    {
        DateTime date;

        // check if filmdate is the right format: DD-MM-YYYY
        bool isValidDate = DateTime.TryParseExact(filmdate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        if (!isValidDate)
        {
            WriteLine("Vul een geldige datum in. (DD-MM-YYYY)");
            return false;
        }
        DateTime currentDateTime = DateTime.Now.Date;
        DateTime filmDateTime = DateTime.ParseExact(filmdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        if (currentDateTime > filmDateTime)
        {
            WriteLine("Datum is al geweest vul een nieuwe datum in.");
            return false;
        }
        return true;
    }
    public bool CheckFilmTime(string filmtime)
    {
        TimeSpan time;

        // check if filmtime is the right format: HH:MM
        bool isValidTime = TimeSpan.TryParseExact(filmtime, "hh\\:mm", CultureInfo.InvariantCulture, out time);

        if (!isValidTime)
        {
            WriteLine("Vul een geldige tijd in. (HH:MM)");
            return false;
        }
        return true;
    }
    public bool CheckFilmRoom(string filmroom)
    {
        // check if filmname is empty
        if (filmroom == "")
        {
            WriteLine("Vul een filmzaal in");
            return false;
        }
        // check filmname character length
        if (filmroom == "1" || filmroom == "2" || filmroom == "3")
        {
            return true;
        }
        else
        {
            WriteLine("Je moet een geldige zaal kiezen!");
        }
        return false;
    }

    public bool CheckDuplicates(FilmModel film)
    {
        if (_films.Any(f => f.filmDate == film.filmDate && f.filmTime == film.filmTime && f.filmRoom == film.filmRoom))
        {
            WriteLine("Film with the same details already exists.");
            return false;
        }
        
        return true;
    }

    public void AddFilm(string filmName, string filmDescription, string filmDate, string filmTime, string filmRoom)
    {
        if (filmName == null || filmDescription == null || filmDate == null || filmTime == null || filmRoom == null)
        {
            return;
        }
        // Check if a film with the same details already exists
        if (_films.Any(f => f.filmDate == filmDate && f.filmTime == filmTime && f.filmRoom == filmRoom))
        {
            WriteLine("Film with the same details already exists.");
            return;
        }

        FilmModel newFilm = new FilmModel(filmName, filmDescription, filmDate, filmTime, filmRoom);
        _films.Add(newFilm);
        FilmsAccess.WriteAll(_films);
    }

    public void UpdateFilm(FilmModel updatedFilm)
    {
        // Find the index of the film to be updated
        int index = _films.FindIndex(f => f.Id == updatedFilm.Id);

        if (index != -1)
        {
            // Update the film in the list
            _films[index] = updatedFilm;
            FilmsAccess.WriteAll(_films);
            WriteLine("Filmgegevens zijn succesvol bijgewerkt.");
        }
        else
        {
            WriteLine("Film niet gevonden. Kan filmgegevens niet bijwerken.");
        }
    }
    
    public List<FilmModel> filmsOnlyActive()
    {
        List<FilmModel> allFilmsActive = GetAllFilms();
        List<FilmModel> allFilms = GetAllFilms().ToList();
        DateTime currentDateTime = DateTime.Now;

        foreach (FilmModel film in allFilms)
        {
            if (film.Active == false)
            {
                allFilmsActive.Remove(film);
                continue;
            }

            string filmDateTimeString = film.filmDate + " " + film.filmTime; // Combine date and time strings
            DateTime filmDateTime = DateTime.ParseExact(filmDateTimeString, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);

            if (currentDateTime > filmDateTime)
            {
                allFilmsActive.Remove(film);
                film.Active = false;
            }
        }
        FilmsAccess.WriteAll(allFilms);
        return allFilmsActive;
    }
}