﻿using Microservice.Core3.Basic.Data.Dto;
using Microservice.Core3.Basic.Literals;
using Microservice.Core3.Basic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microservice.Core3.Basic.Configurations.Exceptions;

#pragma warning disable CA1822 // Mark members as static
namespace Microservice.Core3.Basic.Controllers
{
    [ApiController]
    [Route(Config.ApiController)]
    [Produces(Config.ApplicationJson)]
    public class ValueController : ControllerBase
    {
        private readonly ValueService _valueService;

        public ValueController(ValueService valueService) => _valueService = valueService;

        [HttpGet("RandomExceptionVoid")]
        public Task<IActionResult> RandomExceptionVoid()
        {
            throw new Exception();
        }

        [HttpGet("RandomExceptionMessage")]
        public Task<IActionResult> RandomExceptionMessage()
        {
            throw new Exception("This is a unhandle exception jeje");
        }

        [HttpGet("CustomExceptionVoid")]
        [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status409Conflict)]
        public void CustomExceptionVoid()
        {
            _valueService.CustomExceptionVoid();
        }

        [HttpGet("CustomExceptionMessage")]
        [ProducesResponseType(typeof(ExceptionDto), StatusCodes.Status409Conflict)]
        public void CustomExceptionMessage()
        {
            _valueService.CustomExceptionMessage();
        }

        [HttpGet("Normal")]
        [ProducesResponseType(typeof(ValueDto), StatusCodes.Status200OK)]
        public ValueDto Normal([FromQuery] int id)
        {
            return new ValueDto("This is a normal response");
        }

    }
}