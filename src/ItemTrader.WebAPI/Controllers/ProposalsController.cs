using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.Proposals.Commands;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.Proposals.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentValidation;

namespace ItemTrader.Api.Controllers
{
    public class ProposalsController : ApiControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PaginatedList<ProposalDto>>> Get([FromQuery] GetProposalsWithPaginationQuery query)
        {
            try
            {
                return await Mediator.Send(query);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProposalDto>> Get(int id)
        {
            try
            {
                return await Mediator.Send(new GetProposalQuery { ProposalId = id});
            }
            catch(NotFoundException nfe)
            {
                return NotFound(nfe.Message);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> Create(CreateProposalCommand command)
        {
            try
            {
                var result = await Mediator.Send(command);
                return CreatedAtAction("Get", new {id = result.Id}, result);
            }
            catch (Exception ex) when (ex is ProposalItemException || ex is ValidationException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{proposalId}/Status")]
        [Authorize]
        public async Task<ActionResult<ProposalDto>> UpdateStatus(UpdateProposalStatusCommand command, int proposalId)
        {
            try
            {
                command.ProposalId = proposalId;
                var result = await Mediator.Send(command);
                return Ok(result);
            }
            catch(Exception ex) when (ex is ProposalItemException || ex is ValidationException)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
