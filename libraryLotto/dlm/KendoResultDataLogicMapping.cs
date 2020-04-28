using System;
using System.Collections.Generic;
using System.Text;
using static libraryLotto.dlm.queryDataLogicMapping;

namespace libraryLotto.dlm
{
    public class KendoResultDataLogicMapping
    {
        public KendoData q = new KendoData();//struttura di kendo

        public KendoResultDataLogicMapping(KendoData q)
        {
            this.q = q;
        }
        public class KendoData
        {
            public List<Struct_Joing_AllTable> results = new List<Struct_Joing_AllTable>();
            public int count;

            public KendoData()
            {
            }

            public KendoData(int __count, List<Struct_Joing_AllTable> results)
            {
                this.count = __count;
                this.results = results;
            }
        }
    }
}
