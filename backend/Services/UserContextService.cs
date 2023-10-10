using System.Security.Claims;

namespace wBialy.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User;
        public int? GetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);

    }
}
