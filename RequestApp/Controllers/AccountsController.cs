using Application.Helpers;
using AutoMapper;
using Domain.Models;
using Dto;
using Dto.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace RequestApp.Controllers
{
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<Employee> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<Employee> userManager, JwtHandler jwtHandler,
            IMapper mapper) 
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto  loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
            var userRoles =await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token, UserId = user.Id.ToString(), UserName = user.FullName });
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(p => new ApiError
                {
                    ErrorCode = ((int)HttpStatusCode.BadRequest),
                    ErrorMessage = p.ErrorMessage,
                    ErrorDetails = string.Empty
                })).ToList();

                return BadRequest(errors);
            }
            var user = _mapper.Map<Employee>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var err = new ApiError
                {
                    ErrorCode = ((int)HttpStatusCode.BadRequest),
                    ErrorMessage = $"This {userForRegistration.Email} email address already exist",
                    ErrorDetails = result.Errors.FirstOrDefault().Description
                };
                return BadRequest(err);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
                {
                    {"token", token },
                    {"email", user.Email }
                };
            IdentityResult roleresult = await _userManager.AddToRoleAsync(user, userForRegistration.Role);
            return StatusCode(201);
        }
    }
}
