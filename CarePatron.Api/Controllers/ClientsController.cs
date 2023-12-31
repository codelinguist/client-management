﻿using CarePatron.ClientManagement.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarePatron.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ClientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("Search")]
        public async Task<IActionResult> Get([FromQuery]SearchClients.Query query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddClient([FromBody] AddClient.Command query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpPut("Edit")]
        public async Task<IActionResult> EditClient([FromBody] EditClient.Command query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }


        [HttpPut("SetAsVIP")]
        public async Task<IActionResult> SetAsVIP([FromBody] SetAsVIP.Command query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
