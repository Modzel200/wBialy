namespace wBialy.Entities
{
    public class EventPost : OnSitePost
    {
        public DateTime EventDate { get; set; }
#pragma warning disable CS8618
        public List<EventTag> Tags { get; set; }
#pragma warning restore CS8618
    }
}
