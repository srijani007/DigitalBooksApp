namespace CommonSpace.Models
{
    public class PaymentDetails
    {
        public string BuyerEmail { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public int BookId { get; set; }
        public string? Price { get; set; }
        public string? Title { get; set; }
        public string? Logo { get; set; }
    }
}
