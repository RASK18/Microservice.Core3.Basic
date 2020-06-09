using Microservice.Core3.Basic.Configurations.Exceptions;
using Microservice.Core3.Basic.Data.Dto;
using Microservice.Core3.Basic.Literals;
using Microservice.Core3.Basic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Microservice.Core3.Basic.Controllers
{
    [ApiController]
    [Route(Config.ApiController)]
    [Produces(Config.ApplicationJson)]
    public class ValueController : ControllerBase
    {
        private readonly ValueService _valueService;

        public ValueController(ValueService valueService) => _valueService = valueService;

        [HttpGet("RandomExceptionVoid", Name = "RandomExceptionVoid")]
        public IActionResult RandomExceptionVoid()
        {
            throw new Exception();
        }

        [HttpGet("RandomExceptionMessage", Name = "RandomExceptionMessage")]
        public IActionResult RandomExceptionMessage()
        {
            throw new Exception("This is a unhandled exception jeje");
        }

        [HttpGet("CustomExceptionVoid", Name = "CustomExceptionVoid")]
        [ProducesResponseType(typeof(CustomExceptionDto), StatusCodes.Status409Conflict)]
        public IActionResult CustomExceptionVoid()
        {
            _valueService.CustomExceptionVoid();
            return NoContent();
        }

        [HttpGet("CustomExceptionMessage", Name = "CustomExceptionMessage")]
        [ProducesResponseType(typeof(CustomExceptionDto), StatusCodes.Status409Conflict)]
        public IActionResult CustomExceptionMessage()
        {
            _valueService.CustomExceptionMessage();
            return NoContent();
        }

        [HttpGet("Normal", Name = "Normal")]
        [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK)]
        public IActionResult Normal([FromQuery] int id)
        {
            ValueDto result = new ValueDto("This is a normal response");
            return Ok(result);
        }

    }
}