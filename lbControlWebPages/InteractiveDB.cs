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

namespace lbControlWebPages
{
    public static class InteractiveDB
    {
        public static async Task<bool> addUpdateSiteAsync(string url, int Htime)
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
                    Console.WriteLine("CARICO " + row.Site);
                    var ris = await SendRequestAsync(url, "POST", new Dictionary<string, string> { { "FormAction2", "2" } });
                    if (row.PeHTML == String.Empty)
                        row.PeHTML = ElaboraCampDearby(ris);
                    else
                    {
                        row.PostHTML = row.PeHTML;
                        row.PeHTML = ElaboraCampDearby(ris);
                    }
                    row.State = (row.PostHTML == row.PeHTML || row.PostHTML != string.Empty) ? true : false;
                }
                DbManagement._LottoDs_addRow(row);

                return true;
            }
            catch (Exception EX)
            {
                return false;
            }
        }

        private static string ElaboraCampDearby(string ris)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ris);
            return doc.DocumentNode.SelectNodes("//form[contains(@method,'POST')]")[0].InnerHtml;
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task<string> SendRequestAsync(string url, string Method, Dictionary<string, string> Data)
        {
            try
            {
                var content = new FormUrlEncodedContent(Data);
                var response = client.PostAsync(url, content).Result;

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
    }
}
