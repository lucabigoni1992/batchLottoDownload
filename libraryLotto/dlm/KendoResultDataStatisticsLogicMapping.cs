using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static libraryLotto.dlm.queryDataLogicMapping;
using static libraryLotto.dlm.queryDataStatisticsLogicMapping;

namespace libraryLotto.dlm
{
    public class KendoResultDataStatisticsLogicMapping
    {
        public KendoStatisticsData q = new KendoStatisticsData();//struttura di kendo

        public KendoResultDataStatisticsLogicMapping(KendoStatisticsData q)
        {
            this.q = q;
        }
        public class KendoStatisticsData
        {
            public List<Struct_lotto_Statistics> results = new List<Struct_lotto_Statistics>();
            public int count;
            public object min;
            public object max;

            public KendoStatisticsData() 
            {
            }
            public KendoStatisticsData(IEnumerable<Struct_lotto_Statistics> results)
            {
                this.count = results.Count();
                this.results = results.ToList();
                this.min = results.Min().value1;
                this.max = results.Max().value1;
            }

            public KendoStatisticsData( List<Struct_lotto_Statistics> results)
            {
                this.count = results.Count;
                this.results = results;
                this.min = results.Min().value1;
                this.max = results.Max().value1;
            }

        }
    }
}
