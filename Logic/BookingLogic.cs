using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Console;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class BookingLogic
{
    private List<BookingModel> _bookings;

    public BookingLogic()
    {
        _bookings = BookingsAccess.LoadAll();
    }
    public void AddReservation(List<string> reserverd_seats, string filmdate, string filmtime)
    {
        FilmsLogic filmslogic = new FilmsLogic();
        FilmModel film = filmslogic.GetByDateAndTime(filmdate, filmtime);
        int account_id = AccountsLogic.CurrentAccount?.Id ?? 0;

        List<SeatModel> seats = new();
        foreach(string seat in reserverd_seats)
        {
            SeatModel new_seat = new SeatModel(seat[0].ToString(), int.Parse(seat[1].ToString()));
            seats.Add(new_seat);
        }

        BookingModel reservation = new BookingModel(account_id, film.Id, seats);
        _bookings.Add(reservation);
        BookingsAccess.WriteAll(_bookings);
    }
}