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

namespace lbControlWebPages
{
    public static class InteractiveDB
    {
        public static KendoSiteInputMapping  GetAllSite()
        {
            //         AddUpdateSiteAsync("https://acpol2.army.mil/vacancy/vacancy_list.asp", 24);
            var siteElems = DbManagement._SiteAllRow();
            return new KendoSiteInputMapping(siteElems.ToList());
        }
        public static bool AddSite(SiteInputMapping elem)
        {
            try
            {
                SiteRow row = DbManagement._dsSiteData_newRow(elem);
                DbManagement._LottoDs_addRow(row);
                DbManagement._SiteAllRow();
                return true;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public static async Task<bool> AddUpdateSiteAsync(string url, int Htime)
        {
            try
            {
                if (String.IsNullOrEmpty(url))
                    return false;
                SiteRow row = DbManagement._dsSiteData_newRow(url);
                row.Url = url;
                row.Ore = Htime;
                if (row.Url.ToLower().CompareTo(@"https://acpol2.army.mil/vacancy/vacancy_list.asp") == 0)
                {
                    Console.WriteLine("CARICO " + row.Url);
                    var ris = await SendRequestAsync(url, "POST", new Dictionary<string, string> { { "FormAction2", "2" } });
                    if (row.PreHTML == String.Empty)
                        row.PreHTML = ElaboraCampDearby(ris);
                    else
                    {
                        row.PostHTML = row.PreHTML;
                        row.PreHTML = ElaboraCampDearby(ris);
                    }
                    row.State = (row.PostHTML == row.PreHTML || row.PostHTML != string.Empty) ? true : false;
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
