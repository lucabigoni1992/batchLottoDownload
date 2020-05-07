using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using lbControlWebPages.webPagesData;
using Newtonsoft.Json;

namespace libraryLotto.dlm
{

    public class SiteMapping
    {
        [JsonProperty("skip")]
        public string Site { get; set; }
        [JsonProperty("take")]
        public bool State { get; set; }
        [JsonProperty("filter")]
        public string PreHtml { get; set; }
        [JsonProperty("group")]
        public string PostHTML { get; set; }
        [JsonProperty("sort")]
        public int CadAggiornamento { get; set; }
        public SiteMapping(SiteData.SiteRow tablotto)
        {
            string Site = tablotto.Site;
            bool State = tablotto.State;
            string PreHtml = tablotto.PreHtml;
            string PostHTML = tablotto.PostHTML;
            int CadAggiornamento = tablotto.CadAggiornamento;
    }



    }

}
