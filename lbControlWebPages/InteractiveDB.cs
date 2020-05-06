using libraryLotto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using static lbControlWebPages.webPagesData.SiteData;

namespace lbControlWebPages
{
    public class InteractiveDB
    {

        public bool addSite(string url, int Htime)
        {
            if (String.IsNullOrEmpty(url))
                return false;
            SiteRow row = DbManagement._dsSiteData_newRow();
            row.Site = url;
            row.CadAggiornamento = Htime;
            if (row.Site.ToLower().CompareTo(@"https://acpol2.army.mil/vacancy/vacancy_list.asp") == 0)
                SendRequest(@"https://acpol2.army.mil/vacancy/vacancy_list.asp", "POST", "FormAction = 2");

            return true;
        }

        private static string SendRequest(string url, string Method, string Data)
        {
            try
            {
                // Create a request using a URL that can receive a post.
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = Method;

                byte[] byteArray = Encoding.UTF8.GetBytes(Data);

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                string resp = "";
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    resp = responseFromServer;
                }

                response.Close();
                return resp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
    }
}
