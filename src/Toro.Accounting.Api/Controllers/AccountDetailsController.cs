using Microsoft.AspNetCore.Mvc;
using Toro.Accounting.Api.Dtos;
using Toro.Accounting.Application.Commands;

namespace Toro.Accounting.Controllers;

[ApiController]
[Route("account")]
public class AccountDetailsController : ControllerBase
{
    public AccountDetailsController()
    {
    }

    [Route("details")]
    [HttpGet()]
    [ProducesResponseType(typeof(AccountDetails), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLoggedUserAccountDetails(string userId)
    {
        //Apenas para fins de simplificação, o ID do usuário será obtido via URL. Porém numa aplicação real, isso representaria uma falha de segurança
        //caso não fosse tratado corretamente
        return Ok(new AccountDetails("352", "001", "123456", "Monkey D. Luffy", 200));
    }
}
