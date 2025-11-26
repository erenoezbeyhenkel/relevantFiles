using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.HdpData.DatabricksQuery;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.HttpClients
{
    public interface IHdpHttpClientService
    {
        Task<QueryDetail> GetQueryDetailByIdAsync(string queryId, CancellationToken cancellationToken);
        Task<QueryDetail> GetQueryDetailByNameAsync(string queryId, CancellationToken cancellationToken);
        Task<bool> StartWareHouseAsync(CancellationToken cancellationToken);
        Task<QueryResult> SubmitQueryAsync(string queryText, CancellationToken cancellationToken);
        Task<QueriesDatabricks> GetAllQueriesAsync(CancellationToken cancellationToken);

    }
}
