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
        // Using the AddFilm method
        filmslogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        FilmModel addedFilm = filmslogic.GetByDateAndTime(filmdate, filmtime);

        // Assert
        // Check if the film is equal with the added film
        // Note: The properties are compared seperately
        // because ID is always unique
        Assert.Equal(newFilm.filmName, addedFilm.filmName);
        Assert.Equal(newFilm.filmDescription, addedFilm.filmDescription);
        Assert.Equal(newFilm.filmDate, addedFilm.filmDate);
        Assert.Equal(newFilm.filmTime, addedFilm.filmTime);
        Assert.Equal(newFilm.filmRoom, addedFilm.filmRoom);
        Assert.Equal(newFilm.Active, addedFilm.Active);
    }

    [Fact]
    public void TestChangeFilm()
    {
        var filmsLogic = new FilmsLogic();
        string filmname = "shrek";
        string filmdescription = "shrek the movie";
        string filmdate = "14-06-2023";
        string filmtime = "15:00";
        string filmroom = "1";
        filmsLogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        var addedFilm = filmsLogic.GetByDateAndTime(filmdate,filmtime);

        addedFilm.filmName = "top gun";
        filmsLogic.UpdateFilm(addedFilm);
        var changedFilm = filmsLogic.GetByName("top gun");
        Assert.Equal(changedFilm.filmName, "top gun");
    }

    [Fact]
    public void TestRegisterUser()
    {
        // Arrange
        var accountsLogic = new AccountsLogic();
        string email = "test@voorbeeld.com";
        string password = "wachtwoord123a";
        string fullName = "Tom Cruise";
        accountsLogic.AddAccount(email, password, fullName, false);

        // Act
        var actualAccount = accountsLogic.CheckLogin(email, password);

        // Assert
        Assert.Equal(email, actualAccount.EmailAddress);
        Assert.Equal(password, actualAccount.Password);
        Assert.Equal(fullName, actualAccount.FullName);
    }
    
    [Fact]
    public void TestLoginNull()
    {
        // Arrange
        var accountsLogic = new AccountsLogic();

        // Act
        var actualAccount = accountsLogic.CheckLogin("test@example.com", "wrongpassword");

        // Assert
        Assert.Null(actualAccount);

    }

    [Fact]
    public void testLogAdminAddFilm()
    {
        // Arrange
        AccountsLogic.CurrentAccount = new AccountModel{FullName = "Tom Cruise"}; //tijdelijk currentaccount zetten.
        string filmName = "Test Film";
        string filmDate = "06-05-2023";
        string filmTime = "18:00";
        string filmRoom = "1";
        string expectedLog = $"Admin Tom Cruise heeft een film toegevoegd met als naam: {filmName}, datum: {filmDate}, tijd: {filmTime} en in zaal: {filmRoom}.";

        // Act
        AdminLogger.LogAdminAddFilm(filmName, filmDate, filmTime, filmRoom);

        // Assert
        string[] logs = System.IO.File.ReadAllLines(AdminLogger.path);
        Assert.Contains(expectedLog, logs);
    }

    [Fact]
    public void testLogAdminRemoveFilm()
    {
        // Arrange
        AccountsLogic.CurrentAccount = new AccountModel{FullName = "Tom Cruise"}; //tijdelijk currentaccount zetten.
        string filmName = "Test Film";
        string expectedLog = $"Admin Tom Cruise heeft de film: {filmName} op non-actief gezet.";

        // Act
        AdminLogger.LogAdminRemoveFilm(filmName);

        // Assert
        string[] logs = System.IO.File.ReadAllLines(AdminLogger.path);
        Assert.Contains(expectedLog, logs);
    }

    [Fact]
    public void CheckDuplicatesForAddFilm()
    {
        //arrange
        var filmsLogic = new FilmsLogic();
        string filmname = "TestMovie";
        string filmdescription = @"TestDescription";
        string filmdate = "10-10-2025";
        string filmtime = "19:00";
        string filmroom = "1";
        FilmModel newFilm = new FilmModel(filmname, filmdescription, filmdate, filmtime, filmroom);

        // Act
        //We add a movie twice and then load a list of movies containing only the name of the added movie
        filmsLogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        filmsLogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        int amount_of_movies = filmsLogic.GetMoviesByName(newFilm.filmName).Count;

        //Assert
        //if number of movies is higher than 1 (meaning there are duplicate movies) 
        //then the AddFilm function has added a duplicate movie
        Assert.Equal(amount_of_movies, 1);
    }

    [Fact]
    public void CheckNullForAddFilm()
    {
        //arrange
        var filmsLogic = new FilmsLogic();
        
        // Act
        var exception = Record.Exception(() => filmsLogic.AddFilm(null, null, null, null, null));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void TestRemoveFilm()
    {
        // Arrange
        FilmsLogic filmslogic = new FilmsLogic();
        string filmname = "Titanic";
        string filmdescription = @"The titanic is a ship";
        string filmdate = "10-12-2024";
        string filmtime = "20:00";
        string filmroom = "3";
        FilmModel newFilm = new FilmModel(filmname, filmdescription, filmdate, filmtime, filmroom);

        // Act
        filmslogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        FilmModel addedFilm = filmslogic.GetByDateAndTime(filmdate, filmtime);
        filmslogic.DeleteFilm(addedFilm);


        // Assert
        Assert.Null(filmslogic.GetByDateAndTime(filmdate, filmtime, newFilm.Active)); // return null if film not found
    }

    [Fact]
    public void TestReserveSeat()
    {
        // Arrange
        BookingLogic bookinglogic = new BookingLogic();
        FilmsLogic filmslogic = new FilmsLogic();
        // We need the film info to know which film we want to reserve seats for
        string filmname = "Indiana Jones";
        string filmdescription = "Indiana Jones on adventure";
        string filmdate = "30-6-2023";
        string filmtime = "17:00";
        string filmroom = "3";

        List<string> reservedSeats = new List<string>();
        reservedSeats.Add("A1");
        reservedSeats.Add("B2");
        reservedSeats.Add("F6");

        // We need this list of seatmodel so we can do assert later
        List<SeatModel> seats = new List<SeatModel>();
        seats.Add(new SeatModel("A", 1));
        seats.Add(new SeatModel("B", 2));
        seats.Add(new SeatModel("F", 6));

        // Act
        filmslogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        FilmModel addedFilm = filmslogic.GetByDateAndTime(filmdate, filmtime);

        bookinglogic.AddReservation(reservedSeats, addedFilm.filmDate, addedFilm.filmTime);
        BookingModel bookedFilm = bookinglogic.GetById(addedFilm.Id);
        
        // Assert
        Assert.Equal(seats, bookedFilm.Seats);
    }
 

    [Fact]
    void TestGetAllFilms()
    {
        // Arrange
        var logic = new FilmsLogic();
        var film1 = new FilmModel { Id = 1, filmName = "Film 1" };
        var film2 = new FilmModel { Id = 2, filmName = "Film 2" };
        logic.UpdateList(film1);
        logic.UpdateList(film2);

        // Act
        var result = logic.GetAllFilms();

        // Assert
        Assert.Equal(20, result.Count);
        Assert.Contains(film1, result);
        Assert.Contains(film2, result);
    }

       [Fact]
    void TestDuplicatesForAddFilm2()
    {
        //arrange
        var filmsLogic = new FilmsLogic();
        string filmname = "TestFilm2";
        string filmdescription = @"This is a test discription";
        string filmdate = "10-10-2029";
        string filmtime = "08:00";
        string filmroom = "3";
        FilmModel newFilm = new FilmModel(filmname, filmdescription, filmdate, filmtime, filmroom);

        // Act
        //We add a movie twice and then load a list of movies containing only the name of the added movie
        filmsLogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        filmsLogic.AddFilm(filmname, filmdescription, filmdate, filmtime, filmroom);
        int amount_of_movies = filmsLogic.GetMoviesByName(newFilm.filmName).Count;

        //Assert
        //if number of movies is higher than 1 (meaning there are duplicate movies) 
        //then the AddFilm function has added a duplicate movie
        Assert.Equal(amount_of_movies, 1);
    }

        [Fact]
        void TestNullForAddFilm(){
            //arrange
            var filmsLogic = new FilmsLogic();
            
            // Act
            var exception = Record.Exception(() => filmsLogic.AddFilm(null, null, null, null, null));

            // Assert
            Assert.Null(exception);
        }
}
