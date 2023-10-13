using System.Net;
using wBialy.Entities;

namespace wBialy
{
#pragma warning disable IDE1006 // Style nazewnictwa
    public class wBialySeeder
#pragma warning restore IDE1006 // Style nazewnictwa
    {
        private readonly AppDbContext _context;
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }

                if (!_context.LFTags.Any())
                {
                    var lftags = GetLFTags();
                    _context.LFTags.AddRange(lftags);
                    _context.SaveChanges();
                }
                if (!_context.EventTags.Any())
                {
                    var eventtags = GetEventTags();
                    _context.EventTags.AddRange(eventtags);
                    _context.SaveChanges();
                }
                if (!_context.GastroTags.Any())
                {
                    var gastrotags = GetGastroTags();
                    _context.GastroTags.AddRange(gastrotags);
                    _context.SaveChanges();
                }
            }
        }
        public wBialySeeder(AppDbContext context)
        {
            _context = context;
        }
        private static IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
        private static IEnumerable<LFTag> GetLFTags()
        {
            var tags = new List<LFTag>()
            {
                new LFTag()
                {
                    Name = "telefon"
                },
                new LFTag()
                {
                    Name = "portfel"
                },
            };
            return tags;
        }
        private static IEnumerable<EventTag> GetEventTags()
        {
            var tags = new List<EventTag>()
            {
                new EventTag()
                {
                    Name = "klub"
                },
                new EventTag()
                {
                    Name = "pub"
                },
            };
            return tags;
        }
        private static IEnumerable<GastroTag> GetGastroTags()
        {
            var tags = new List<GastroTag>()
            {
                new GastroTag()
                {
                    Name = "burger"
                },
                new GastroTag()
                {
                    Name = "pizza"
                },
            };
            return tags;
        }
    }
}
