using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using SendGrid;
using smart_dorms.Utils;

namespace smart_dorms.Utils
{
    public class SendMail
    {
        public static async Task Execute(string numeCerere, string email, string numeUtilizator, int idStatus)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("UVTDorms@e-uvt.ro", "Admin UVT Dorms");
            var subject = "Update cerere " + numeCerere;
            var to = new EmailAddress(email, numeUtilizator);
            var plainTextContent = "Te anuntam ca cererea ta a fost ";
            switch ((RequestStatus)idStatus)
            {
                case RequestStatus.Completed:
                    {
                        plainTextContent += "aprobata! Felicitari!";

                        break;
                    }
                case RequestStatus.Invalid:
                    {
                        plainTextContent += "refuzata din motive logistice! Ne pare rau!";

                        break;
                    }
                case RequestStatus.NeedsTime:
                    {
                        plainTextContent += "data mai departe! Te rugam sa astepti!";

                        break;
                    }
                case RequestStatus.Allocated:
                    {
                        plainTextContent += "alocata! Ne ocupam de ea in cel mai scurt timp posibil!";

                        break;
                    }
                case RequestStatus.Pending:
                    {
                        plainTextContent += "trimisa! Ne ocupam de ea in cel mai scurt timp posibil!";

                        break;
                    }
                case RequestStatus.Rejected:
                    {
                        plainTextContent += "respinsa! Ne pare rau!";

                        break;
                    }
            }
            var htmlContent = $"<strong>{plainTextContent}</strong><br><strong>Beta Version</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}