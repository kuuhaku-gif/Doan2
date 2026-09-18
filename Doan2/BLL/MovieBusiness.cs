using BLL.Interfaces;
using Doan2.dal.interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class MovieBusiness : IMovieBusiness
    {
        private readonly IMovieRepository _res;

        public MovieBusiness(IMovieRepository res)
        {
            _res = res;
        }

        public List<MovieModel> GetAllMovies()
        {
            return _res.GetAllMovies();
        }

        public MovieModel GetMovieById(int id)
        {
            return _res.GetMovieById(id);
        }

        public bool CreateMovie(MovieModel movie)
        {
            return _res.CreateMovie(movie);
        }

        public bool DeleteMovie(int id)
        {
            return _res.DeleteMovie(id);
        }
    }
}