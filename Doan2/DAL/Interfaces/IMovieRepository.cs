
using Model;
using System.Collections.Generic;

namespace Doan2.dal.interfaces
{
    public interface IMovieRepository
    {
        List<MovieModel> GetAllMovies();
        MovieModel GetMovieById(int id);
        bool CreateMovie(MovieModel movie);
        bool DeleteMovie(int id);
    }
}