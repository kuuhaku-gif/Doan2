using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface ITicketBusiness
    {
        List<TicketModel> GetTicketsByUser(int userId);
        bool CreateTicket(TicketModel model);
        bool CheckInTicket(string ticketCode);
        List<string> GetBookedSeats(int movieId, string cinemaRoom, DateTime showtime);
        List<TicketModel> GetAllTickets();
        DashboardStatsModel GetDashboardStats();
    }
}