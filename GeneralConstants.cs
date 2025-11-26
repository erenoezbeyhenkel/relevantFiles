namespace Hcb.Rnd.Pwn.Application.Common.Constants;

public static class GeneralConstants
{
    public const string ApiRouteV1 = "api/v1.0/[controller]/";
    public const string Groups = "groups";
    public const string Oid = "oid";


    //Swagger
    public static readonly string SwaggerEndpointUrl = "/swagger/v1.0/swagger.json";
    public static string SwaggerEndpointName(string environment) => $"Henkel Hcb.Rnd v1.0 - {environment}";
    public static string SwaggerDocDescription(string environment) => $"Henkel Hcb.Rnd Swager Doc. - {environment}";
    public static string SwaggerDocTitle(string environment) => $"Henkel Hcb.Rnd v1.0 - {environment}";
    public static readonly string SwaggerDocVersion = "v1.0";
    public static readonly string SwaggerDocName = "v1.0";
    public static readonly string CorrelationIdHeaderName = "X-Correlation-Id";

    //Jwt token items
    public static readonly string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    public static readonly string PreferredUsername = "preferred_username";
}
