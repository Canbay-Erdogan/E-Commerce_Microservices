using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Multishop.IdentityServer.Dtos;
using Multishop.IdentityServer.Models;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Multishop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto UserRegisterDto)
        {
            var value = new ApplicationUser()
            {
                Email = UserRegisterDto.Email,
                UserName = UserRegisterDto.Username,
                Name = UserRegisterDto.Name,
                Surname = UserRegisterDto.Surname,
            };

            var result = await _userManager.CreateAsync(value, UserRegisterDto.Password);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı başarıyla eklendi");
            }
            else
            {
                return Ok("Hata!! eklenemedi.. " + result.Errors.FirstOrDefault().Description);
            }
        }
    }
}
