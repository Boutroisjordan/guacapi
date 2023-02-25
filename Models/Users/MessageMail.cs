using System.Net.Mail;
using System.Net.Mime;
using MimeKit;
using MimeKit.Utils;

namespace GuacAPI.Models.Users
{
    public class MessageMail
    {
        public List<MailboxAddress> To{ get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public MessageMail(IEnumerable<string> to, string subject,string userName, string contactEmail, string recipientName, string confirmationLink)
        {
            To = new List<MailboxAddress>();
            foreach (var item in to)
            {
                To.AddRange(to.Select(x => new MailboxAddress("email",x)));
            }
            Subject = subject;

// Créer le corps du message avec l'image incluse
var body = $"<html><head><style>" +
           "img {{ float: left; margin-right: 10px; }}" +
           "h1 {{ font-size: 24px; margin-top: 0; }}" +
           "p {{ font-size: 16px; }}" +
           "</style></head>" +
           "<body>" +
           "<h1>Confirmation de validation - GuacaGnole</h1>" +
           $"<p>Cher/Chère {recipientName},</p>" +
           "<p>Nous sommes ravis de vous informer que votre compte a été validé avec succès sur notre plateforme GuacaGnole, spécialisée dans la vente de produits alimentaires mexicains de qualité supérieure.</p>" +
           "<p>Votre compte vous permettra d'accéder à toutes les fonctionnalités de notre site, notamment la visualisation des produits, l'ajout de produits à votre panier d'achat, la gestion de vos commandes et le suivi de leur livraison.</p>" +
           "<p>Veuillez trouver ci-dessous votre nom d'utilisateur et votre mot de passe pour vous connecter à votre compte :</p>" +
           $"<p>Nom d'utilisateur : {userName}</p>" +
           "<p>N'oubliez pas que vous pouvez à tout moment modifier votre mot de passe et vos informations de compte en vous connectant à votre profil.</p>" +
           $"<p>Si vous avez des questions ou des préoccupations, n'hésitez pas à nous contacter à l'adresse e-mail suivante : {contactEmail}.</p>" +
           "<p>Nous sommes impatients de vous offrir une expérience de magasinage exceptionnelle sur GuacaGnole.</p>" +
           "<p>Cordialement,</p>" +
           "<p>L'équipe Guacateam</p>" +
           $"<a href=\"" + confirmationLink + "\"style=\"background-color: #007bff; border: none; color: white; padding: 15px 32px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 4px;\">Vérifier mon adresse e-mail</a>" +
           "</body></html>";
            Content = body;
        
        }
        }
    }