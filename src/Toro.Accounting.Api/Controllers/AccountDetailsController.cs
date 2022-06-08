using Microsoft.AspNetCore.Mvc;
using Toro.Accounting.Application.Contracts.Querys;
using Toro.Accounting.Application.Dtos;
using Toro.Accounting.Application.Querys.GetAccountDetails;

namespace Toro.Accounting.Controllers;

[ApiController]
[Route("account")]
public class AccountDetailsController : ControllerBase
{
    protected readonly IQueryDispatcher _queryDispatcher;

    public AccountDetailsController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }

    [Route("details")]
    [HttpGet()]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AccountDetails>> GetLoggedUserAccountDetails(string userId)
    {
        //Apenas para fins de simplificação, o ID do usuário será obtido via URL. Porém numa aplicação real, isso representaria uma falha de segurança
        //caso não fosse tratado corretamente
        var accountDetails = await _queryDispatcher.Dispatch(new GetAccountDetailsQuery(userId));

        if (accountDetails == null)
            return NotFound();

        return Ok(accountDetails);
    }
}
