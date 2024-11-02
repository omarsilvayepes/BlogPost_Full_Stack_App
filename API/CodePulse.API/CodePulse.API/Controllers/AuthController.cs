﻿using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository
            )
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            //Create IdentityUser object

            var user = new IdentityUser
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
            };

            var identityResult=await _userManager.CreateAsync(user,request.Password);
            if(identityResult.Succeeded)
            {
                //Add role to user (Reader)
                identityResult = await _userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok(identityResult);
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser=await _userManager.FindByEmailAsync(request.Email);
            if(identityUser is not null)
            {
                var checkPasswordResult=await _userManager.CheckPasswordAsync(identityUser, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    //create token and response
                    var response = new LoginResponseDto
                    {
                        Email=request.Email,
                        Roles=roles.ToList(),
                        Token=_tokenRepository.CreateJwtToken(identityUser, roles.ToList())
                    };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");
            return ValidationProblem(ModelState);
        }
    }
}
