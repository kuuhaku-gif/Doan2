using System;
using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class TicketBusiness : ITicketBusiness
    {
        private readonly ITicketRepository _res;

        public TicketBusiness(ITicketRepository res)
        {
            _res = res;
        }

        public List<TicketModel> GetTicketsByUser(int userId)
        {
            return _res.GetTicketsByUser(userId);
        }

        public bool CreateTicket(TicketModel model)
        {
            // Tự sinh mã vé ngẫu nhiên nếu chưa có
            if (string.IsNullOrEmpty(model.TicketCode))
            {
                model.TicketCode = "#CNM" + new Random().Next(1000, 9999);
            }
            return _res.CreateTicket(model);
        }
    }
}