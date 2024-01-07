using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using wBialy.Entities;
using wBialy.Exceptions;
using wBialy.Models;

namespace wBialy.Services
{
    public interface IAccountService
    {
        Task<string> GenerateJwt(LoginDto dto);
        Task RegisterUser(RegisterUserDto dto);
        Task<bool> RecogniseAdmin();
        Task<bool> VerifyEmail(string token);
        Task ForgotPassword(string email);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
    }

    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextService _userContextService;
        private readonly IEmailSenderService _emailSenderService;
        public AccountService(AppDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IUserContextService userContextService, IEmailSenderService emailSenderService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _userContextService = userContextService;
            _emailSenderService = emailSenderService;
        }
        public async Task RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                VerificationToken = CreateRandomToken(),
                LikedPosts = new List<Post>()
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            newUser.Role = await _context.Roles.SingleOrDefaultAsync(x => x.Name == "Unconfirmed");
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            await _emailSenderService.SendEmailAsync(dto.Email, "Confirm your email", "http://wbialyamogus-001-site1.atempurl.com/api/account/verifyemail/"+$"{newUser.VerificationToken}");
        }
        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Email == dto.Email && x.Role.Name != "Unconfirmed");
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password or email not confirmed");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
        public async Task<bool> RecogniseAdmin()
        {
            var userId = _userContextService.GetUserId;
            var userAsAdmin = await _context.Users.SingleOrDefaultAsync(x => x.Role.Name == "Admin" && x.UserId == userId);
            if (userAsAdmin is not null)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        public async Task<bool> VerifyEmail(string token)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => token == x.VerificationToken && x.Role.Name == "Unconfirmed");
            if(user is null)
            {
                return await Task.FromResult(false);
            }
            user.Role = await _context.Roles.SingleOrDefaultAsync(x => x.Name == "User");
            _context.Update(user);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        public async Task ForgotPassword(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && (x.ResetPasswordTimeExpires == null || DateTime.Now > x.ResetPasswordTimeExpires));
            if(user is null)
            {
                throw new NotFoundException("User not found");
            }
            var code = CreateRandomToken();
            while(await _context.Users.AnyAsync(x => x.PasswordResetToken == code))
            {
                code = CreateRandomToken();
            }
            user.PasswordResetToken = code;
            user.ResetPasswordTimeExpires = DateTime.Now.AddHours(3);
            _context.Update(user);
            await _context.SaveChangesAsync();
            await _emailSenderService.SendEmailAsync(email, "Your code to reset password", "Here's your code to reset password: \n" + $"{code}");
        }
        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PasswordResetToken == resetPasswordDto.ResetToken && x.ResetPasswordTimeExpires > DateTime.Now);
            if(user == null)
            {
                throw new NotFoundException("Invalid or expired token");
            }
            var newPasswordHash = _passwordHasher.HashPassword(user, resetPasswordDto.Password);
            user.PasswordHash = newPasswordHash;
            user.PasswordResetToken = null;
            user.ResetPasswordTimeExpires = null;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
