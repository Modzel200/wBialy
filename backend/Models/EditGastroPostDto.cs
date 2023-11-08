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
        public string Location { get; set; }
        public string Day { get; set; }
        public List<GastroTagDto> Tags { get; set; }
        public string? Link { get; set; }
#pragma warning restore CS8618
    }
}
