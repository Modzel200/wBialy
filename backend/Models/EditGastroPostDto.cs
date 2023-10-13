using wBialy.Entities;

namespace wBialy.Models
{
    public class EditGastroPostDto
    {
#pragma warning disable CS8618
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Place { get; set; }
        public DateTime Day { get; set; }
        public List<GastroTag> Tags { get; set; }
        public string? Link { get; set; }
#pragma warning restore CS8618
    }
}
