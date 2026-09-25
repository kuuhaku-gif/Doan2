using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface ITicketBusiness
    {
        List<TicketModel> GetTicketsByUser(int userId);
        bool CreateTicket(TicketModel model);
    }
}