namespace wBialy.Entities
{
    public class Post
    {
        public int PostId { get; set; }
#pragma warning disable CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set;  }
        public string Place { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime AddDate { get; set; }
        public string? Tags { get; set; }
        public string? Link { get; set; }
        public int? UserId { get; set; }
        public bool Confirmed { get; set; } = false;
        public virtual User User { get; set; }
#pragma warning restore CS8618 // Pole niedopuszczające wartości null musi zawierać wartość inną niż null podczas kończenia działania konstruktora. Rozważ zadeklarowanie pola jako dopuszczającego wartość null.
    }
}
