using DAL.Helper;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public ShowtimeRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<ShowtimeModel> GetByMovieId(int movieId)
        {
            string msgError = "";
            var list = new List<ShowtimeModel>();
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_showtime_get_by_movie",
                    "@MovieId", movieId);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new ShowtimeModel
                        {
                            ShowtimeId = Convert.ToInt32(row["ShowtimeId"]),
                            MovieId = Convert.ToInt32(row["MovieId"]),
                            MovieTitle = row["MovieTitle"]?.ToString(),
                            CinemaRoom = row["CinemaRoom"]?.ToString() ?? "",
                            StartTime = Convert.ToDateTime(row["StartTime"]),
                            TicketPrice = Convert.ToDecimal(row["TicketPrice"]),
                            Status = row["Status"]?.ToString()
                        });
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ShowtimeModel? GetById(int showtimeId)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_showtime_get_by_id",
                    "@ShowtimeId", showtimeId);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new ShowtimeModel
                    {
                        ShowtimeId = Convert.ToInt32(row["ShowtimeId"]),
                        MovieId = Convert.ToInt32(row["MovieId"]),
                        MovieTitle = row["MovieTitle"]?.ToString(),
                        CinemaRoom = row["CinemaRoom"]?.ToString() ?? "",
                        StartTime = Convert.ToDateTime(row["StartTime"]),
                        TicketPrice = Convert.ToDecimal(row["TicketPrice"]),
                        Status = row["Status"]?.ToString()
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(ShowtimeModel model)
        {
            string msgError = "";
            try
            {
                _dbHelper.ExecuteNonQuery(out msgError, "sp_showtime_create",
                    "@MovieId", model.MovieId,
                    "@CinemaRoom", model.CinemaRoom,
                    "@StartTime", model.StartTime,
                    "@TicketPrice", model.TicketPrice);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}