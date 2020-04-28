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
            public List<Struct_lotto_Statistics> data = new List<Struct_lotto_Statistics>();
            public int Count;
            public object Min;
            public object Max;
            public double Average;

            public KendoStatisticsData()
            {
            }
            public KendoStatisticsData(IEnumerable<Struct_lotto_Statistics> data, bool withSatistics = false)
            {
                this.Count = data.Count();
                this.data = data.ToList();
                if (withSatistics)
                    computeSatistics(data);
            }

            public KendoStatisticsData(List<Struct_lotto_Statistics> results)
            {
                this.Count = results.Count;
                this.data = results;
            }
            private void computeSatistics(IEnumerable<Struct_lotto_Statistics> data)
            {
                data = data.OrderBy(r => r.field);
                this.Min = data.Min(r => r.value1);// data.Min().value1;
                this.Max = data.Max(r => r.value1);
                this.Average = data.Average(r => double.TryParse(r.value1.ToString(), out double val) ? val : 0);
            }
        }
    }
}
