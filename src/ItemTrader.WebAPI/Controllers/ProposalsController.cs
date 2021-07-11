using ItemTrader.Application.Common.Exceptions;
using ItemTrader.Application.Common.Models;
using ItemTrader.Application.Proposals.Commands;
using ItemTrader.Application.Proposals.Dto;
using ItemTrader.Application.Proposals.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ProposalDto>> GetSingle(int id)
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
                return CreatedAtAction("GetSingle", new { id = result.Id }, result);
            }
            catch (ProposalItemException ex)
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
            catch(ProposalItemException pie)
            {
                return BadRequest(pie.Message);
            }
        }
    }
}
