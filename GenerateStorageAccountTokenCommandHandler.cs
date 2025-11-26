using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.Token;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.StorageAccountToken.Generate;

public sealed class GenerateStorageAccountTokenCommandHandler(IUnitOfWork unitOfWork,
                                                              IStorageAccountAccessService storageAccountAccessProvider,
                                                              IOptions<StorageAccountOptions> options,
                                                              IDateTimeProvider dateTimeProvider,
                                                              IEmailService emailService) : ICommandHandler<GenerateStorageAccountTokenCommand, GenerateStorageAccountTokenCommandResponse>
{
    public async Task<ErrorOr<GenerateStorageAccountTokenCommandResponse>> Handle(GenerateStorageAccountTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var storageAccountOptions = options.Value;
            var storageAccountToken = await unitOfWork.StorageAccountTokens.FindOneAsync(x => x.Name == storageAccountOptions.Name, cancellationToken: cancellationToken);

            //If no token exists
            if (Guard.Against.IsNullOrEmpty(storageAccountToken?.EncryptedToken))
            {
                //Create new token
                var newSasTokenResult = await storageAccountAccessProvider.CreateAccountSasToken();
                if (Guard.Against.IsNullOrEmpty(newSasTokenResult))
                    return Errors.Infrastructure.CreationError("Sas token from Azure");

                var newSasTokenEntity = new Domain.Entities.StorageAccount.StorageAccountToken
                {
                    Name = storageAccountOptions.Name,
                    EncryptedToken = newSasTokenResult.Encrypt(),
                    GeneratedBy = request.GeneratedBy
                };

                await unitOfWork.StorageAccountTokens.InsertOneAsync(newSasTokenEntity, cancellationToken);
                var createNewtokenResult = await unitOfWork.SaveChangesAsync(cancellationToken);

                if (createNewtokenResult != 1)
                    return Errors.Infrastructure.CreationError("Sas token in db");

                return new GenerateStorageAccountTokenCommandResponse(newSasTokenResult, storageAccountOptions.ServiceUrl);
            }

            //If token exists
            var decodedSasToken = storageAccountToken.EncryptedToken.Decrypt();

            //If token is valid
            if (IsTokenValid(decodedSasToken))
                return new GenerateStorageAccountTokenCommandResponse(decodedSasToken, storageAccountOptions.ServiceUrl);

            //If token is not valid
            var createdSasToken = await storageAccountAccessProvider.CreateAccountSasToken();
            if (Guard.Against.IsNullOrEmpty(createdSasToken))
                return Errors.Infrastructure.CreationError("Sas token from Azure");

            //delete from db
            await unitOfWork.StorageAccountTokens.DeleteByIdAsync(storageAccountToken.Id, cancellationToken);
            var deleteResult = await unitOfWork.SaveChangesAsync(cancellationToken);
            if (deleteResult != 0)
                return Errors.Infrastructure.DeleteError("Sas token");

            var newSasToken = new Domain.Entities.StorageAccount.StorageAccountToken
            {
                Name = storageAccountOptions.Name,
                EncryptedToken = createdSasToken.Encrypt(),
                GeneratedBy = request.GeneratedBy
            };

            //insert new valid one.
            await unitOfWork.StorageAccountTokens.InsertOneAsync(newSasToken, cancellationToken);
            var createResult = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (createResult != 1)
                return Errors.Infrastructure.CreationError("Sas token in db");

            return new GenerateStorageAccountTokenCommandResponse(createdSasToken, storageAccountOptions.ServiceUrl);
        }
        catch (Exception ex)
        {
            //This catch blog will be removed if we dont get any email from here for couple of months.
            var message = new StringBuilder();
            message.AppendLine("-----------------------------Message-----------------------------");
            message.AppendLine(ex.Message);
            message.AppendLine("-----------------------------Message-----------------------------");
            message.AppendLine("-----------------------------StackTrace-----------------------------");
            message.AppendLine(ex.StackTrace);
            message.AppendLine("-----------------------------StackTrace-----------------------------");
            message.AppendLine("-----------------------------Inner Exception Message-----------------------------");
            message.AppendLine(ex.InnerException?.Message);
            message.AppendLine("-----------------------------Inner Exception Message-----------------------------");

            await emailService.SendEmail(message.ToString());
            throw;
        }
    }

    //TODO: Describe the logic
    private bool IsTokenValid(string token)
    {
        var uri = new Uri($"{options.Value.ServiceUrl}?{token}");
        var sasTokenQueryParams = HttpUtility.ParseQueryString(uri.Query);
        var expiresOn = DateTime.Parse(sasTokenQueryParams["se"], null, System.Globalization.DateTimeStyles.RoundtripKind);

        //If token is not expired
        if (dateTimeProvider.UtcNow < expiresOn)
            return true;

        return false;
    }
}
