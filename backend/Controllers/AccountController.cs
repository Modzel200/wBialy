using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using wBialy.Entities;
using wBialy.Models;
using wBialy.Services;

namespace wBialy.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _accountService.RegisterUser(dto);
            return await Task.FromResult(Ok());
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await _accountService.GenerateJwt(dto);
            return await Task.FromResult(Ok(token));
        }
        [HttpGet]
        [Authorize]
        public async Task<bool> RecogniseAdmin()
        {
            var result = await _accountService.RecogniseAdmin();
            if (result)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
        [HttpGet("verifyemail/{token}")]
        public async Task<IActionResult> VerifyEmail([FromRoute]string token)
        {
            var result = await _accountService.VerifyEmail(token);
            if (result)
            {
                return await Task.FromResult(Ok("Email verification completed"));
            }
            return await Task.FromResult(NotFound("Couldn't verify email"));
        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            await _accountService.ForgotPassword(email);
            return await Task.FromResult(Ok("Reset code has been sent"));
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _accountService.ResetPassword(resetPasswordDto);
            return await Task.FromResult(Ok("Password has been reseted"));
        }
    }
}
