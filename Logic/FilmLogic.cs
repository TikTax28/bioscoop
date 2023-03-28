using System;
using System.Collections.Generic;
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
}