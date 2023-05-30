using Xunit;

public class LogicTest
{
    [Fact]
    public void TestAddFilm()
    {
        // Arrange
        FilmsLogic filmslogic = new FilmsLogic();
        string filmname = "The Maze Runner";
        string filmdescription = @"Thomas ontwaakt in een gigantisch labyrint en 
                                herinnert zich niets van zijn verleden. Samen 
                                met andere tieners probeert hij te ontsnappen.";
        string filmdate = "10-10-2024";
        string filmtime = "19:00";
        string filmroom = "1";
        FilmModel newFilm = new FilmModel(filmname, filmdescription, filmdate, filmtime, filmroom);

        // Act
        filmslogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        FilmModel addedFilm = filmslogic.GetByDateAndTime(filmdate, filmtime);

        // Assert
        Assert.Equal(newFilm.filmName, addedFilm.filmName);
        Assert.Equal(newFilm.filmDescription, addedFilm.filmDescription);
        Assert.Equal(newFilm.filmDate, addedFilm.filmDate);
        Assert.Equal(newFilm.filmTime, addedFilm.filmTime);
        Assert.Equal(newFilm.filmRoom, addedFilm.filmRoom);
        Assert.Equal(newFilm.Active, addedFilm.Active);
    }
}