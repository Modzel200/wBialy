﻿namespace wBialy.Entities
{
    public class GastroPost : OnSitePost
    {
        public string Day { get; set; }
#pragma warning disable CS8618
        public List<GastroTag> Tags { get; set; }
#pragma warning restore CS8618
    }
}
