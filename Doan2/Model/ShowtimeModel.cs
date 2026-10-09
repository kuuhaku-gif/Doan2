using System;

namespace Model
{
    public class ShowtimeModel
    {
        public int ShowtimeId { get; set; }
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string CinemaRoom { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string? Status { get; set; }
    }
}