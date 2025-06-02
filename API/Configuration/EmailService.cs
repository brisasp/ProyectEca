using System.Net.Mail;
using System.Net;
using API.Configuration;


public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        try
        {
            var from = _config["EmailSettings:From"];
            var password = _config["EmailSettings:Password"];
            var host = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:Port"]);

            var mensaje = new MailMessage
            {
                From = new MailAddress(from),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = true
            };
            mensaje.To.Add(destinatario);

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mensaje);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al enviar el correo: {ex.Message}");
            throw;
        }
    }

}