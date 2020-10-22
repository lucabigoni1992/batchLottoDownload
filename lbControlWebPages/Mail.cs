using HtmlAgilityPack;
using libraryLotto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static lbControlWebPages.webPagesData.SiteData;
using System.Threading;
using libraryLotto.dlm;
using static libraryLotto.dlm.KendoResultDataSiteInputMapping;
using System.Linq;
using System.Net.Mail;

namespace lbControlWebPages
{
    public static class Mail
    {
        public static void SendMessage(SiteRow row)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new System.Net.Mail.MailAddress("luca.bigoni@live.it");

                // The important part -- configuring the SMTP client
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mail.From.Address, "Hunter_92");
                smtp.Host = "smtp.live.com";

                //recipient address
                mail.To.Add(new MailAddress(row.Email));

                //Formatted mail body
                mail.IsBodyHtml = true;
                string st = CompareStringDiffPlex.Compare(row.PreHTML, row.PostHTML);

                mail.Body = st;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }
    }
}
