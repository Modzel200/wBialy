namespace wBialy.Entities
{
    public class EventTag : Tag
    {
#pragma warning disable CS8618
        public string Name { get; set; }
        public List<EventPost> Posts { get; set; }
#pragma warning restore CS8618
    }
}
