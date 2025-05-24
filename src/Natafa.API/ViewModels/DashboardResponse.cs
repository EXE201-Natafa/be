namespace Natafa.Api.ViewModels
{
    public class DashboardResponse
    {
        public int TotalProducts { get; set; }  
        public int TotalOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int TotalUsers { get; set; }
    }
}
