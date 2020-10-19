using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libraryLotto.dlm
{
    public class KendoResultDataSiteInputMapping
    {
        public KendoSiteInputMapping q = new KendoSiteInputMapping();//struttura di kendo

        public KendoResultDataSiteInputMapping(KendoSiteInputMapping q)
        {
            this.q = q;
        }
        public class KendoSiteInputMapping
        {
            public List<SiteInputMapping> results = new List<SiteInputMapping>();
            public int count;
          

            public KendoSiteInputMapping()
            {
            }
            public KendoSiteInputMapping(IEnumerable<SiteInputMapping> data, bool withSatistics = false)
            {
                this.count = data.Count();
                this.results = data.ToList();
                if (withSatistics)
                    computeSatistics(data);
            }

            public KendoSiteInputMapping(List<SiteInputMapping> results)
            {
                this.count = results.Count;
                this.results = results;
            }
            private void computeSatistics(IEnumerable<SiteInputMapping> data)
            {
                data = data.OrderBy(r => r.Url);
            }
        }
    }
}
