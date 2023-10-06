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
#pragma warning disable CS8603 // Możliwe zwrócenie odwołania o wartości null.
        public ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User;
#pragma warning restore CS8603 // Możliwe zwrócenie odwołania o wartości null.
#pragma warning disable CS8602 // Wyłuskanie odwołania, które może mieć wartość null.
        public int? GetUserId =>
            User is null ? null : (int?)int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
#pragma warning restore CS8602 // Wyłuskanie odwołania, które może mieć wartość null.

    }
}
