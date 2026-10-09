using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface IShowtimeBusiness
    {
        List<ShowtimeModel> GetByMovieId(int movieId);
        ShowtimeModel? GetById(int showtimeId);
        bool Create(ShowtimeModel model);
    }
}