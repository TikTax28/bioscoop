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
    public List<FilmModel> GetAllFilms()
    {
        return _films;
    }
    public void DeleteFilm(FilmModel film)
    {
        _films.Remove(film);
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
            WriteLine("Film beschrijving mag niet te lang zijn!");
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
            WriteLine("Foute format! Gebruik de format: DD-MM-YYYY");
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
            WriteLine("Foute format! Gebruik de format: HH:MM");
            return false;
        }
        return true;
    }
    public void AddFilm(string filmname, string filmdescription, string filmdate, string filmtime)
    {
        FilmModel newFilm = new FilmModel(filmname, filmdescription, filmdate, filmtime);
        _films.Add(newFilm);
        FilmsAccess.WriteAll(_films);
    }
}
