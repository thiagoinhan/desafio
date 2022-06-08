using Toro.Accounting.Application.Dtos;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Querys.GetAccountDetails
{
    public class GetAccountDetailsQuery : IQuery<AccountDetails>
    {
        public GetAccountDetailsQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; private set; }
    }
}
