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
        // Make an instance of FilmsLogic
        FilmsLogic filmslogic = new FilmsLogic();

        // Get the Film Model of the selected film using film date and time
        // using only name can result in duplicates
        FilmModel film = filmslogic.GetByDateAndTime(filmdate, filmtime);

        // Get the account id of the account used
        // Note: if you log in without an account, account id will set 0 by default
        int account_id = AccountsLogic.CurrentAccount?.Id ?? 0;

        // Create list of SeatModel
        List<SeatModel> seats = new();

        // Loop through the list of reserved seats
        foreach(string seat in reserverd_seats)
        {
            // Create a SeatModel based on the reserved seat
            SeatModel new_seat = new SeatModel(seat[0].ToString(), int.Parse(seat[1].ToString()));
            // Add it to the SeatModel list
            seats.Add(new_seat);
        }

        // Use account id, film id and list of seats to create a Booking Model
        BookingModel reservation = new BookingModel(account_id, film.Id, seats);

        // Add the reservation
        _bookings.Add(reservation);

        // Write the change to JSON
        BookingsAccess.WriteAll(_bookings);
    }
}