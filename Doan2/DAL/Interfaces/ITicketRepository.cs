using System.Collections.Generic;
using Model;

namespace DAL.Interfaces
{
    public interface ITicketRepository
    {
        List<TicketModel> GetTicketsByUser(int userId);
        List<string> GetBookedSeats(int movieId, string cinemaRoom, DateTime showtime);
        bool CheckInTicket(string ticketCode);
        bool CreateTicket(TicketModel model);
        
    }
}