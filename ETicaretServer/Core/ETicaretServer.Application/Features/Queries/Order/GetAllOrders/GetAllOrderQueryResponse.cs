namespace ETicaretServer.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrderQueryResponse
    {
        public string OrderCode { get; set; }
        public string Username { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
