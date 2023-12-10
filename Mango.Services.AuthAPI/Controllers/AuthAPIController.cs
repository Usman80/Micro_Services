﻿using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }
        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            //var errorMessage = await _authService.Register(model);
            //if(!string.IsNullOrEmpty(errorMessage))
            //{
            //    _response.IsSuccess = false;
            //    _response.Message= errorMessage;    
            //    return BadRequest(_response);
            //}
            //return Ok(_response);
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            //await _messageBus.PublishMessage(model.Email, _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue"));
            return Ok(_response);
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
        #endregion

        #region AssignRole
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            //var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            //if (!assignRoleSuccessful)
            //{
            //    _response.IsSuccess = false;
            //    _response.Message = "Error Encountered";
            //    return BadRequest(_response);
            //}
            //return Ok(_response);
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        #endregion
    }
}