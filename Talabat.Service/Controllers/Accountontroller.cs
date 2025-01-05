using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabat.Service.DTOs;
using Talabat.Service.Extensions;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<AppUser> _UserManager { get; }
        private SignInManager<AppUser> _SignInManager { get; }
        private ITokenService _TokenService { get; }
        private IMapper _Mapper { get; }

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper
            )
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _TokenService = tokenService;
            _Mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user = await _UserManager.FindByEmailAsync(loginDto.Email);
            if (user == null) 
                return Unauthorized();
            var result = await _SignInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _TokenService.CreateTokenAsync(user, _UserManager)
            });

        }
       [ HttpPost("register")]
       public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto)
        {
            if (CheckEmailExist(registerDto.Email).Result.Value) return BadRequest();
                
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.DisplayName,
                    PhoneNumber = registerDto.PhoneNumber,
                    UserName = registerDto.Email.Split("@")[0]

                }; 
                var result = await _UserManager.CreateAsync(user);
                 if (!result.Succeeded) return BadRequest();
                return Ok(new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _TokenService.CreateTokenAsync(user, _UserManager)


                });

            } 
            return BadRequest();    
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser ()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _UserManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _TokenService.CreateTokenAsync(user, _UserManager)

            });
        }
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddress ()
        {

            var user =await _UserManager.FindUserWithAdressAsync(User);
            var addressDto = _Mapper.Map<Address, AddressDto?>(user?.Address);
          
            return Ok(addressDto);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
        {
            var address = _Mapper.Map<AddressDto, Address>(UpdatedAddress);

            var user = await _UserManager.FindUserWithAdressAsync(User);
            address.Id = user.Address.Id;
            user.Address = address;

            var result = await _UserManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest();
             
            return Ok(UpdatedAddress);
        }
        [HttpGet("checkemail")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
           return await _UserManager.FindByEmailAsync(email) is not null;

        }


    }
}
 
