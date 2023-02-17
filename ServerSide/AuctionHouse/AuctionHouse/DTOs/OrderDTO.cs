namespace AuctionHouse.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public DateTime DateOrdered { get; set; }

        public bool IsOrderActive { get; set; }

        public bool IsOrderCompleted { get; set; }

        public ItemResponse ItemResponse { get; set; }
    }
}
