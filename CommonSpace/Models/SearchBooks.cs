namespace CommonSpace.Models
{
    public class SearchBooks
    {
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string AuthorName { get; set; } = null!;
    }
}
