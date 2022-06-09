using Toro.Accounting.Application.Dtos;
using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Querys
{
    public class GetAccountsQuery : IQuery<List<AccountDetails>>
    {
        public GetAccountsQuery()
        {
        }
    }
}
