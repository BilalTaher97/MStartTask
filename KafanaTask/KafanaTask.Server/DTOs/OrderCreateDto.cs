namespace KafanaTask.Server.DTOs
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = null!;
    }
}
