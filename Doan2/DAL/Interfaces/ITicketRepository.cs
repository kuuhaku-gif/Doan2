using System.Collections.Generic;
using Model;

namespace DAL.Interfaces
{
    public interface ITicketRepository
    {
        List<TicketModel> GetTicketsByUser(int userId);
        bool CreateTicket(TicketModel model);
    }
}