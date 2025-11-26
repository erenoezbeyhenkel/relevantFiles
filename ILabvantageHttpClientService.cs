using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.GateWay.GetSamplesByWorksheetId;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.HttpClients;

public interface ILabvantageHttpClientService
{
    Task<HttpResponseMessage> GetAsync(string request, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync<TPutContentType>(string request, TPutContentType putContentType, CancellationToken cancellationToken);
    Task<RootObject> GetSamplesByWorksheetIdAsync(string worksheetId, CancellationToken cancellationToken);

}
