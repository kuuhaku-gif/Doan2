namespace Model
{
    public class DashboardStatsModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalTickets { get; set; }
        public int UpcomingTickets { get; set; }
        public int UsedTickets { get; set; }
        public int CancelledTickets { get; set; }
    }
}
