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

    [Fact]
    public void TestLogin()
    {
        // Arrange
        var accountsLogic = new AccountsLogic();
        string email = "test@example.com";
        string password = "password123";
        accountsLogic.AddAccount(email, password, "test", false);

        // Act
        var actualAccount = accountsLogic.CheckLogin("test@example.com", "password123");

        // Assert
        Assert.Equal(email, actualAccount.EmailAddress);
        Assert.Equal(password, actualAccount.Password);
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
        Assert.Equal(filmname, addedFilm.filmName);

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
}