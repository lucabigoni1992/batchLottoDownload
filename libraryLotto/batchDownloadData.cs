using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static libraryLotto.Data.DsLotto;

namespace libraryLotto
{
    public class batchDownloadData
    {
        private HttpClient client;
        private HtmlDocument doc;
        private HtmlDocument docDetailes;
        private HtmlDocument doctempRow;
        private HtmlDocument doctemp;
        private int anno = Variabili.annoDiInizio;

        public batchDownloadData()
        {
            client = new HttpClient();
            doc = new HtmlDocument();
            docDetailes = new HtmlDocument();
            doctempRow = new HtmlDocument();
            doctemp = new HtmlDocument();
        }
        ~batchDownloadData()
        {
            client.Dispose();
        }
        public void downloadAllLotto()
        {
            for (int i = 0; i < (DateTime.Now.Year - Variabili.annoDiInizio); i++)
            {
                Task<string> task = Task.Run(async () => await downloadDataEstrazioniLotto());
                string result = task.Result;
                doc.LoadHtml(result);
                int countPalla = 0;
                LottoRow row = Variabili._LottoDs_newRow(); ;
                HtmlNodeCollection nodesAll = doc.DocumentNode.SelectNodes(@"(//a[contains(@href,'/risultati/estrazione')]|//td[@class='ball-24px']|//td[@class='superstar-24px']|//td[@class='jolly-24px'])"); //dal 28 marzo 2006
                for (int nc = nodesAll.Count - 1; nc != 0; nc--)// parto all'incontrario per un fattore d'indice
                {
                    HtmlNode node = nodesAll[nc];
                    if (node.Attributes.Count == 1 && node.Attributes[0].Value == "ball-24px")// dovrebbe esserci solamnente una classe ball-24px
                    {
                        int.TryParse(node.InnerText, out int value);
                        switch (++countPalla)
                        {
                            case 1:
                                row.palla1 = value; break;
                            case 2:
                                row.palla2 = value; break;
                            case 3:
                                row.palla3 = value; break;
                            case 4:
                                row.palla4 = value; break;
                            case 5:
                                row.palla5 = value; break;
                            case 6:
                                row.palla6 = value; break;
                        }
                    }
                    else if (node.Attributes.Count == 1 && node.Attributes[0].Value == "superstar-24px")
                    {
                        int.TryParse(node.InnerText, out int value);
                        row.superstar = value;
                    }
                    else if (node.Attributes.Count == 1 && node.Attributes[0].Value == "jolly-24px")
                    {
                        int.TryParse(node.InnerText, out int value);
                        row.jolly = value;
                    }
                    else if (node.Attributes.Count == 2 && node.Attributes[0].Name == "href")
                    {//primo campo che si legge
                        countPalla = 0;
                        DateTime.TryParse(node.Attributes[0].Value.Substring(Variabili.extractData.Length), out DateTime Data);
                        row.hrfQuotazioni = node.Attributes[0].Value;
                        row.data = Data;
                        row.anno = Data.Year;
                        Task<string> taskDetailes = Task.Run(async () => await downloadDataEstrazioniLottoDetailes(row.hrfQuotazioni));


                        int countEstrazione = dettagliEstrazione(taskDetailes.Result);
                        row.id = countEstrazione;
                        Variabili._LottoDs_addRow(row);
                        inserisciDettagli(row.id);
                        row = Variabili._LottoDs_newRow();

                    }
                }

                Variabili._DsLottoSave();
            }

        }

        private int dettagliEstrazione(string result)
        {
            docDetailes.LoadHtml(result);
            HtmlNodeCollection nodesid = docDetailes.DocumentNode.SelectNodes(@"(//p[@itemprop='description'])");
            int.TryParse(nodesid[0].InnerHtml.Split("<strong>")[1].Split("<sup>")[0], out int id);
            return creaIndice(id);
        }
        private void inserisciDettagli(int id)
        {
            HtmlNodeCollection nodesAll = docDetailes.DocumentNode.SelectNodes(@"(//table[@class='tbl3']|//h2)");
            for (int nc = 0; nc < nodesAll.Count; nc++)// parto all'incontrario per un fattore d'indice
            {
                HtmlNode node = nodesAll[nc];
                if (node.Attributes.Count == 1 && node.Attributes[0].Value == "tbl3")
                {
                    doctemp.LoadHtml(node.InnerHtml);
                    HtmlNodeCollection rows = doctemp.DocumentNode.SelectNodes(@"(//tr)");
                    for (int i = 1; i < rows.Count; i++)// saltro la prima riga
                    {
                        HtmlNode noderow = rows[i];
                        doctempRow.LoadHtml(noderow.InnerHtml);
                        HtmlNodeCollection trows = doctempRow.DocumentNode.SelectNodes(@"(//td)");
                        QuatazioniVinciteRow row = Variabili._QuatazioniVinciteRow_newRow();
                        row.id = id;
                        row.enumTipoVincita = "";
                        if (trows[0].InnerText == "") continue;
                        string valore = Normalize(trows[1].InnerText);
                        if (valore == "-") valore = "0";
                        string[] valuta = valore.Split(" ");//valore, valuta
                        row.valore = long.Parse(valuta[0].Replace(".", "").Split(",")[0]);
                        if (valuta.Length > 1) row.valuta = valuta[1];
                        row.vincitori = int.Parse(Normalize(trows[2].InnerText).Replace(".", ""));
                        row.premio = trows[0].InnerText;
                        row.enumTipoVincita = nodesAll[nc - 1].InnerText;
                        Variabili._QuatazioniVincite_addRow(row);
                        trows.Clear();
                    }
                    rows.Clear();
                }
            }
            nodesAll.Clear();
        }

        private string Normalize(string innerText)
        {
            return innerText.Replace("\t", "").Replace("\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace("&#8364;", "€");
        }

        private int creaIndice(int i)
        {
            string estr = (i).ToString();
            for (int c = estr.Length; c < 4; c++)
                estr = "0" + estr;

            int.TryParse((anno + estr), out int val);
            return val;
        }

        private async Task<string> downloadDataEstrazioniLotto()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string url = Variabili.urlLotto + Variabili.urlLottoRisultati + (anno++).ToString();
                Console.WriteLine(url);
                HttpResponseMessage response = await client.GetAsync(url);//++annoCoutnis è un operazione atomica ergo ogni chiamata ha un numero differente
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); ;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return "";
        }
        private async Task<string> downloadDataEstrazioniLottoDetailes(string hrf)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                string url = Variabili.urlLotto + hrf.Substring(1);
                Console.WriteLine(url);
                HttpResponseMessage response = await client.GetAsync(url);//++annoCoutnis è un operazione atomica ergo ogni chiamata ha un numero differente
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync(); ;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return "";
        }
    }
}
