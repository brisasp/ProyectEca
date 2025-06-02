using System.Net.Mail;
using System.Net;
using API.Configuration;


public class EmailService
{
    public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        var mensaje = new MailMessage();
        mensaje.From = new MailAddress("brisasp01@gmail.com");
        mensaje.To.Add(destinatario);
        mensaje.Subject = asunto;
        mensaje.Body = cuerpo;
        mensaje.IsBodyHtml = true;

        var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("brisasp01@gmail.com", "oedhrnkkkejdbhvh"),
            EnableSsl = true
        };

        smtp.Send(mensaje);
    }
}

