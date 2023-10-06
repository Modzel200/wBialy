﻿namespace wBialy.Entities
{
    public class User
    {
#pragma warning disable CS8618
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Post>? Posts { get; set; }
#pragma warning restore CS8618
    }
}
