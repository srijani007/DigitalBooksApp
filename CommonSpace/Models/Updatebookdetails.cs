using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CommonSpace.Models
{
    public class Updatebookdetails
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public string AuthorName { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public string Content { get; set; } = null!;
        public string? Active { get; set; }
        public string? Logo { get; set; }

    }
}
