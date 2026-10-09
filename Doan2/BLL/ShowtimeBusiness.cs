using System;
using System.Collections.Generic;
using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class ShowtimeBusiness : IShowtimeBusiness
    {
        private readonly IShowtimeRepository _res;

        public ShowtimeBusiness(IShowtimeRepository res)
        {
            _res = res;
        }

        public List<ShowtimeModel> GetByMovieId(int movieId)
        {
            return _res.GetByMovieId(movieId);
        }

        public ShowtimeModel? GetById(int showtimeId)
        {
            return _res.GetById(showtimeId);
        }

        public bool Create(ShowtimeModel model)
        {
            if (model.StartTime <= DateTime.Now)
            {
                throw new Exception("Thời gian suất chiếu phải ở tương lai!");
            }

            if (model.TicketPrice <= 0)
            {
                throw new Exception("Giá vé phải lớn hơn 0!");
            }

            return _res.Create(model);
        }
    }
}