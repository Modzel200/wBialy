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
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await _accountService.GenerateJwt(dto);
            return Ok(token);
        }
        [HttpGet]
        [Authorize]
        public async Task<bool> RecogniseAdmin()
        {
            var result = await _accountService.RecogniseAdmin();
            if (result)
            {
                return true;
            }
            return false;
        }
        [HttpGet("verifyemail/{token}")]
        public async Task<IActionResult> VerifyEmail([FromRoute]string token)
        {
            var result = await _accountService.VerifyEmail(token);
            if (result)
            {
                return Ok("Email verification completed");
            }
            return NotFound("Couldn't verify email");
        }
        [HttpPost("forgotpassword/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {
            await _accountService.ForgotPassword(email);
            return Ok("Reset code has been sent");
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            await _accountService.ResetPassword(resetPasswordDto);
            return Ok("Password has been reseted");
        }
    }
}
