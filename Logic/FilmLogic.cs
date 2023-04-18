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
    public bool CheckFilm(string filmname, string filmdate, string filmtime)
    {
        DateTime date;
        TimeSpan time;

        // check filmname character length
        if (filmname.Length > 50)
        {
            WriteLine("Film lengte moet minder dan 50 letters zijn!");
            return false;
        }

        // check if filmdate is the right format: DD-MM-YYYY
        bool isValidDate = DateTime.TryParseExact(filmdate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

        if (!isValidDate)
        {
            WriteLine("Foute format! Gebruik de format: DD-MM-YYYY");
            return false;
        }

        // check if filmtime is the right format: HH:MM
        bool isValidTime = TimeSpan.TryParseExact(filmtime, "hh\\:mm", CultureInfo.InvariantCulture, out time);

        if (!isValidTime)
        {
            WriteLine("Foute format! Gebruik de format: HH:MM");
            return false;
        }
        return true;
    }
    public void AddFilm(string filmname, string filmdate, string filmtime)
    {
        FilmModel newFilm = new FilmModel(filmname, filmdate, filmtime);
        _films.Add(newFilm);
        FilmsAccess.WriteAll(_films);
    }
}