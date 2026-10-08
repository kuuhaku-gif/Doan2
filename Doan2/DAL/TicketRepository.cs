using System;
using System.Collections.Generic;
using DAL.Helper;
using DAL.Interfaces;
using Model;

namespace DAL
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public TicketRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }


        public List<TicketModel> GetTicketsByUser(int userId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ticket_get_by_user",
                    "@UserId", userId);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return dt.ConvertTo<TicketModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckInTicket(string ticketCode)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_ticket_checkin",
                    "@TicketCode", ticketCode);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CreateTicket(TicketModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_ticket_create",
                    "@TicketCode", model.TicketCode,
                    "@UserId", model.UserId,
                    "@MovieId", model.MovieId,
                    "@CinemaRoom", model.CinemaRoom,
                    "@Showtime", model.Showtime,
                    "@Seats", model.Seats,
                    "@TicketType", model.TicketType,
                    "@Price", model.Price,
                    "@Status", model.Status ?? "upcoming");

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<string> GetBookedSeats(int movieId, string cinemaRoom, DateTime showtime)
        {
            string msgError = "";
            var bookedSeats = new List<string>();
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ticket_get_booked_seats",
                    "@MovieId", movieId,
                    "@CinemaRoom", cinemaRoom,
                    "@Showtime", showtime);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        // Cột Seats lưu chuỗi như "A1, A2" -> tách nhỏ ra từng ghế
                        string seatStr = row["Seats"]?.ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(seatStr))
                        {
                            var seats = seatStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
                            foreach (var s in seats)
                            {
                                bookedSeats.Add(s.Trim());
                            }
                        }
                    }
                }

                return bookedSeats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}