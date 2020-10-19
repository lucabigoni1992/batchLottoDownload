using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using lbControlWebPages.webPagesData;
using Newtonsoft.Json;
using static lbControlWebPages.webPagesData.SiteData;

namespace libraryLotto.dlm
{
    public class SiteInputMapping{
        

        public string Url { get; set; }
        public string Email { get; set; }
        public int Ore { get; set; }
        public string Tag { get; set; }
        public bool Active { get; set; }

    }

    public class SiteMapping: SiteInputMapping
    {
        public bool State { get; set; }
        public string PreHtml { get; set; }
        public string PostHTML { get; set; }

        public SiteMapping(SiteRow tablotto)
        {
            Url = tablotto.Url;
            State =tablotto.IsStateNull()?false: tablotto.State;
            PreHtml = tablotto.IsPreHTMLNull() ? "" : tablotto.PreHTML;
            PostHTML = tablotto.IsPostHTMLNull() ? "" : tablotto.PostHTML; 
            Email = tablotto.IsEmailNull() ? "luca.bigoni@live.it" : tablotto.Email;
            Ore = tablotto.Ore;
            Tag = tablotto.IsTagNull() ? "" : tablotto.Tag;
            Active = tablotto.IsActiveNull()?false: tablotto.Active == 1;
            
        }
  
        static public void  FromInputSiteToRow(SiteInputMapping elem,ref SiteRow row)
        {
            row. Url = elem.Url;
            row.Email = elem.Email;
            row.Ore = elem.Ore;
            row.Tag = elem.Tag;
            row.Active = (byte)(elem.Active ? 1 : 0);
            row.State = false;
        }

    }

}
