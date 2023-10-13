namespace wBialy.Entities
{
    public class GastroTag : Tag
    {
#pragma warning disable CS8618
        public string Name { get; set; }
        public List<GastroPost> Posts { get; set; }
#pragma warning restore CS8618
    }
}
