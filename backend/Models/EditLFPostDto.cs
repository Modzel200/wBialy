using wBialy.Entities;

namespace wBialy.Models
{
    public class EditLFPostDto
    {
#pragma warning disable CS8618
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string Place { get; set; }
        public List<LFTagDto> Tags { get; set; }
#pragma warning restore CS8618
    }
}
