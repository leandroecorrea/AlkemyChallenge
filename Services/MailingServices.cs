using System.Net.Http.Headers;
using DisneyAPI.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DisneyAPI.Services;

// Solo logro ver los mails que se mandan a casillas de correo GMail, Outlook no recibe.
public class MailingServices : IMailingServices
{
    private DisneyContext _ctx;
    private readonly IConfiguration Configuration;
    private static readonly string MessageId = "X-Message-Id";

    private string apiKey;

    public MailingServices(DisneyContext ctx, IConfiguration configuration)
    {
        _ctx = ctx;
        Configuration = configuration;
        apiKey = Configuration["SendGridApiKey"];
    }
    public async Task SendMailAsync(string email)
    {        
        var client = new SendGridClient(apiKey);
        var emailMessage = new SendGridMessage()
        {
            From = new EmailAddress("leandrocorrea1989@hotmail.com", "DisneyAPI"),
            Subject = "Su usuario ha sido registrado - API Mundo Disney",
            HtmlContent = "<div>Ya podés realizar peticiones a la API con tu usuario y contraseña</div>",
            PlainTextContent = "Bienvenido al Mundo Disney"
        };
        emailMessage.AddTo(new EmailAddress(email, email));
        ProcessResponse(client.SendEmailAsync(emailMessage).Result);
    }
    private void ProcessResponse(Response response)
    {
        if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted) || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
        {
            ToMailResponse(response);
            return;
        }
        var errorResponse = response.Body.ReadAsStringAsync().Result;
        throw new Exception(errorResponse);
    }
    private void ToMailResponse(Response response)
    {
        if (response == null) return;
        var headers = (HttpHeaders)response.Headers;
        var messageId = headers.GetValues(MessageId).FirstOrDefault();
        Console.WriteLine($"Mail sent: ID {messageId} Date {DateTime.UtcNow}");
    }
}