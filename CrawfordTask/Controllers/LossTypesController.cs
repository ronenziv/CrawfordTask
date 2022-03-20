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
using CrawfordTask.Common.Models.LossTypes;
using Microsoft.AspNetCore.Http;

namespace CrawfordTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LossTypesController : ControllerBase
    {
        private ICrawfordService _crawfordService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public LossTypesController(
            ICrawfordService crawfordService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _crawfordService = crawfordService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// return all LossTypes
        /// </summary>
        /// <response code="200">Array of LossType objects</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LossTypeModel))]
        public IActionResult GetAll()
        {
            var lossTypes = _crawfordService.GetAllLossTypes();
            var model = _mapper.Map<IList<LossTypeModel>>(lossTypes);
            return Ok(model);
        }

        /// <summary>
        /// return user by ID
        /// </summary>
        /// <response code="200">LossType object</response>
        /// <response code="404">Specified LossType object does not exist</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LossTypeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var lossTypes = _crawfordService.GetLossTypeById(id);

            if (lossTypes == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<LossTypeModel>(lossTypes);
            return Ok(model);
        }
    }
}
