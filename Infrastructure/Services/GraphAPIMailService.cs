using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using Azure.Identity;

namespace Infrastructure.Services;

public class GraphAPIMailService : IEmailService
{
    private readonly IConfidentialClientApplication _confidentialClientApplication;
    private readonly string _clientId;
    private readonly string _tenantId;
    private readonly string _clientSecret;

    public GraphAPIMailService(string clientId, string tenantId, string clientSecret)
    {
        _clientId = clientId;
        _tenantId = tenantId;
        _clientSecret = clientSecret;

        _confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(_clientId)
            .WithTenantId(_tenantId)
            .WithClientSecret(_clientSecret)
            .Build();
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };

        var authResult = await _confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
        var accessToken = authResult.AccessToken;

        var graphClient = new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) =>
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return Task.CompletedTask;
        }));

        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Text,
                Content = body
            },
            ToRecipients = new List<Recipient>()
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = to
                        }
                    }
                }
        };

        await graphClient.Me.SendMail(message, null).Request().PostAsync();
    }
}
