using System.Threading.Tasks;

namespace MyCompany.School.HomeworkDemo.EmailServices
{
    public interface IEmailSender
    {
         // Email Sender Bizim Email Göndermek İçin Birden Fazla Hizmeti Kullanmamızı Sağlabilir.
         // Mesela SMTP veya herhangi bir API kullanarak verilerimizi gönderebiliriz.
         Task SendEmailAsync(string email,string subject, string htmlMessage);
    }
}