using Microsoft.AspNetCore.Mvc;
using Toro.Accounting.Api.Dtos;
using Toro.Accounting.Application.Commands;
using Toro.Accounting.Application.Contracts.Commands;

namespace Toro.Accounting.Controllers;

[ApiController]
[Route("spb")]
public class SpbController : ControllerBase
{
    protected readonly ICommandDispatcher _commandDispatcher;

    public SpbController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [Route("events")]
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(MakeDepositCommandResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> MakeDeposit(DepositEventDetails depositDetails)
    {
        var command = new MakeDepositCommand(depositDetails.@event, depositDetails.target.account, depositDetails.origin.cpf, depositDetails.amount);
        var response = await _commandDispatcher.Dispatch(command);

        if (response.ValidationErrors.Any())
        {
            return BadRequest(response.ValidationErrors);
        }

        return NoContent();
    }
}
