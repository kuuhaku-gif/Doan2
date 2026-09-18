using DAL.Helper;
using Doan2.dal.interfaces;
using Model;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public MovieRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<MovieModel> GetAllMovies()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_movie_get_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return dt.ConvertTo<MovieModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MovieModel GetMovieById(int id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_movie_get_by_id", "@MovieId", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                var list = dt.ConvertTo<MovieModel>();
                return list.Count > 0 ? list[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateMovie(MovieModel movie)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_movie_create",
                    "@Title", movie.Title,
                    "@Category", movie.Category,
                    "@Duration", movie.Duration,
                    "@Poster", movie.Poster,
                    "@Description", movie.Description,
                    "@ReleaseDate", movie.ReleaseDate,
                    "@IsActive", movie.IsActive);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMovie(int id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteNonQuery(out msgError, "sp_movie_delete", "@MovieId", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}