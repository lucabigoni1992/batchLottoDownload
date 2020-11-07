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
        public static KendoSiteInputMapping GetAllSite()
        {
            //         AddUpdateSiteAsync("https://acpol2.army.mil/vacancy/vacancy_list.asp", 24);
            var siteElems = DbManagement.SiteAllRow();
            return new KendoSiteInputMapping(siteElems.ToList());
        }
        public static bool AddSite(SiteInputMapping elem)
        {
            try
            {
                SiteRow row = DbManagement.DsSiteData_newRow(elem);
                DbManagement.LottoDs_addRow(row);
                DbManagement.SiteAllRow();
                return true;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public static bool ChangeSite(SiteActionMapping elem)
        {
            try
            {
                if (elem.Action == siteAction.delete)
                    DbManagement.DsSiteData_deleteRow(elem.Url);
                else if (elem.Action == siteAction.disable)
                    DbManagement.DsSiteData_disableRow(elem.Url);
                return true;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public static async Task<bool> AddUpdateSiteAsync(SiteRow row)
        {
            try
            {
                //      return true;
                if (row.Active == 0)
                    return false;
                if (String.IsNullOrEmpty(row.Url))
                    return false;
                if (row.Url.ToLower().CompareTo(@"https://acpol2.army.mil/vacancy/vacancy_list.asp") == 0)
                {
                    Console.WriteLine("CARICO " + row.Url);
                    var ris = await SendRequestAsync(row.Url, "POST", new Dictionary<string, string> { { "FormAction2", "2" } });
                    if (row.IsPreHTMLNull() || row.PreHTML == String.Empty)
                        row.PreHTML = ElaboraCampDearby(ris);
                    else
                    {
                        row.PostHTML = row.PreHTML;
                        row.PreHTML = ElaboraCampDearby(ris);
                    }
                    row.State = (row.PostHTML == row.PreHTML || row.PostHTML != string.Empty);
                }
                else
                {
                    Console.WriteLine("CARICO " + row.Url);
                    var ris = await SendRequestAsync(row.Url, "GET", new Dictionary<string, string> { { "FormAction2", "2" } });
                    if (row.IsPreHTMLNull() || row.PreHTML == String.Empty)
                        row.PreHTML = ElaboraByTag(ris, row.Tag);
                    else
                    {
                        row.PostHTML = row.PreHTML;
                        row.PreHTML = ElaboraByTag(ris, row.Tag);
                        if (row.PostHTML != row.PreHTML)
                        {
                            //send mail
                            Mail.SendMessage(row);
                        }
                    }
                }
                DsSiteSave();
                return true;
            }
            catch (Exception EX)
            {
                Console.WriteLine(EX.ToString());
                return false;
            }
        }

        private static void DsSiteSave()
        {
            throw new NotImplementedException();
        }

        private static string ElaboraByTag(string ris, string tag)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ris);

            if (string.IsNullOrEmpty(tag))
                tag = "body";
            else
                tag.Replace("<", "").Replace(">", "");
            if (ris.Contains("<rss version="))
                return ris;

            var postcd = doc.DocumentNode.SelectNodes($"//{tag}");
            if (postcd != null)
                return postcd[0].InnerHtml;
            else return "";
        }

        private static string ElaboraCampDearby(string ris)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(ris);
            var postcd = doc.DocumentNode.SelectNodes("//form[contains(@method,'POST')]");
            if (postcd != null)
                return postcd[0].InnerHtml;
            else return "";
        }


        private static async Task<string> SendRequestAsync(string url, string Method, Dictionary<string, string> Data)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                if (url.StartsWith("https"))
                    client = new HttpClient(clientHandler);
                var content = new FormUrlEncodedContent(Data);
                HttpResponseMessage response = null;
                if (Method == "POST")
                    response = client.PostAsync(url, content).Result;
                if (Method == "GET")
                    response = client.GetAsync(url).Result;

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
