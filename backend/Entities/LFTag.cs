namespace wBialy.Entities
{
    public class LFTag : Tag
    {
#pragma warning disable CS8618
        public string Name { get; set; }
        public List<LFPost>? Posts { get; set; }
#pragma warning restore CS8618
    }
}
