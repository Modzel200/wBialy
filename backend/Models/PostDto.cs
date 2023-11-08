using wBialy.Entities;

namespace wBialy.Models
{
    public class PostDto
    {
#pragma warning disable CS8618 
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Place { get; set; }
        public string Location { get; set; }
        public DateTime? EventDate { get; set; }
        public string Day { get; set; }
        public string? Tags { get; set; }
        public string? Link { get; set; }
        public bool Confirmed { get; set; }
#pragma warning restore CS8618 
    }
}
