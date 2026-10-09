using System.Collections.Generic;
using Model;

namespace DAL.Interfaces
{
    public interface IShowtimeRepository
    {
        List<ShowtimeModel> GetByMovieId(int movieId);
        ShowtimeModel? GetById(int showtimeId);
        bool Create(ShowtimeModel model);
    }
}