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
using FluentScheduler;
using System.Threading;

namespace lbControlWebPages
{
    public class InteractiveDB
    {
        static Registry Scheduler = new Registry();


        public async System.Threading.Tasks.Task<bool> addUpdateSiteAsync(string url, int Htime)
        {
            try
            {
                if (String.IsNullOrEmpty(url))
                    return false;
                SiteRow row = DbManagement._dsSiteData_newRow(url);
                row.Site = url;
                row.CadAggiornamento = Htime;
                if (row.Site.ToLower().CompareTo(@"https://acpol2.army.mil/vacancy/vacancy_list.asp") == 0)
                {
                    var ris = await SendRequestAsync(url, "POST", new Dictionary<string, string> { { "FormAction2", "2" } });
                    if (row.PeHTML == String.Empty)
                        row.PeHTML = ElaboraCampDearby(ris);
                    else
                        row.PeHTML = ElaboraCampDearby(ris);
                }
                DbManagement._LottoDs_addRow(row);
                Scheduler.Schedule(
                           () =>
                                 Console.WriteLine(" now is  ->", DateTime.Now.ToShortTimeString())
                           ).ToRunNow().AndEvery(10).Seconds();
                Thread.Sleep(60 * 1000);
                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }

        private string ElaboraCampDearby(string ris)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ris);
            return doc.DocumentNode.SelectNodes("//form[contains(@method,'POST')]")[0].InnerHtml;
        }

        private static readonly HttpClient client = new HttpClient();

        private async Task<string> SendRequestAsync(string url, string Method, Dictionary<string, string> Data)
        {
            try
            {


                var content = new FormUrlEncodedContent(Data);

                var response = client.PostAsync(url, content).Result;

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
    }
}
