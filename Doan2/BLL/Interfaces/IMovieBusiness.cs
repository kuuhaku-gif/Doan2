
using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IMovieBusiness
    {
        List<MovieModel> GetAllMovies();
        MovieModel GetMovieById(int id);
        bool CreateMovie(MovieModel movie);
        bool DeleteMovie(int id);
    }
}