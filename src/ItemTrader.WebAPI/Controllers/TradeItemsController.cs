using System;
using System.Net.Mail;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.TradeItems.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentValidation;
using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.TradeItems.Commands;
using ItemTrader.Application.TradeItems.Queries;

namespace ItemTrader.Api.Controllers
{
    public class TradeItemsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TradeItemDto>> Get(int id)
        {
            try
            {
                return await Mediator.Send(new GetSingleTradeItemQuery { TradeItemId = id });
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<TradeItemDto>>> Get([FromQuery] GetTradeItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreateTradeItemCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return CreatedAtAction("Get", result);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTradeItemCommand {TradeItemId = id});
                return NoContent();
            }
            catch (NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch (ValidationException ve)
            {
                return BadRequest(ve.Message);
            }
        }
    }
}
