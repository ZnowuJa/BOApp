using Application.Interfaces;

using Azure.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;


namespace Infrastructure.Services;

public class GraphEmailService : IEmailService
{
    //private readonly ITokenAcquisition _tokenAcquisition;
    private readonly GraphServiceClient _graphServiceClient;
    private readonly IConfiguration _config;

    public GraphEmailService(IConfiguration config)
    {
        
        _config = config;
        var section = _config.GetSection("AzureAd");
        string tId = section["TenantId"];
        string cId = section["ClientId"];
        string sId = Environment.GetEnvironmentVariable("Email_Secret");
     
        ClientSecretCredential credentials = new(tId, cId, sId);

        _graphServiceClient = new GraphServiceClient(credentials, new[] { "https://graph.microsoft.com/.default" });
    }

    public async Task SendEmailAsync(Message message)
    {

        var sendMailRequestBody = new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
        {
            Message = message,
            SaveToSentItems = true
        };
        
        string senderEmail = "BackOfficeApp@porscheinterauto.pl";
        await _graphServiceClient.Users[senderEmail].SendMail.PostAsync(sendMailRequestBody);
    }
}

