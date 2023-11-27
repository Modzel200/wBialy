namespace wBialy.Entities
{
    public class LFPost : Post
    {
#pragma warning disable CS8618
        public List<LFTag> Tags { get; set;}
        public bool Found { get; set;}
#pragma warning restore CS8618
    }
}
