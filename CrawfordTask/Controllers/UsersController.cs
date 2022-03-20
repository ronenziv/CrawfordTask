using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using CrawfordTask.Common.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CrawfordTask.Common.Services;
using CrawfordTask.Common.Entities;
using CrawfordTask.Common.Models.Users;
using Microsoft.AspNetCore.Http;

namespace CrawfordTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private ICrawfordService _crawfordService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            ICrawfordService crawfordService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _crawfordService = crawfordService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// authenticate ticket to be created
        /// </summary>
        /// <response code="204">authenticate ticket created</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            try
            {
                var user = _crawfordService.Authenticate(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info and authentication token
                return Ok(new
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Token = tokenString
                });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// return all Users
        /// </summary>
        /// <response code="200">Array of User objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public IActionResult GetAll()
        {
            var users = _crawfordService.GetAllUsers();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        /// <summary>
        /// return user by ID
        /// </summary>
        /// <response code="200">User object</response>
        /// <response code="404">Specified User object does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var user = _crawfordService.GetUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }
    }
}
