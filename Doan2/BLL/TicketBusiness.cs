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

        public bool CheckInTicket(string ticketCode)
        {
            return _res.CheckInTicket(ticketCode);
        }
        public List<TicketModel> GetTicketsByUser(int userId)
        {
            return _res.GetTicketsByUser(userId);
        }
        private string GenerateRandomTicketCode()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string randomPart = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            return $"TK-{datePart}-{randomPart}";
        }

        public bool CreateTicket(TicketModel model)
        {

            // Tự động sinh mã vé ngẫu nhiên nếu client không gửi hoặc để trống
            if (string.IsNullOrWhiteSpace(model.TicketCode))
            {
                model.TicketCode = GenerateRandomTicketCode();
            }

            // Gán trạng thái mặc định ban đầu là upcoming nếu chưa có
            if (string.IsNullOrWhiteSpace(model.Status))
            {
                model.Status = "upcoming";
            }

            return _res.CreateTicket(model);
        }
        public List<string> GetBookedSeats(int movieId, string cinemaRoom, DateTime showtime)
        {
            return _res.GetBookedSeats(movieId, cinemaRoom, showtime);
        }
    }
}