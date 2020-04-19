using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libraryLotto.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace libraryLotto.dlm
{

    public class queryDataStatisticsLogicMapping
    {
        public class DateFormatConverter : IsoDateTimeConverter
        {
            public DateFormatConverter(string format)
            {
                DateTimeFormat = format;
            }
        }
        public class Struct_lotto_Statistics
        {
            public string field { get; set; }
            public string value { get; set; }

            public Struct_lotto_Statistics(string field, string value)
            {
                this.field = field;
                this.value = value;
            }
            public Struct_lotto_Statistics(string field, int value)
            {
                this.field = field;
                this.value = value.ToString();
            }
            public Struct_lotto_Statistics()
            {
                this.field = "";
                this.value = "";
            }
        }

        private void SetStatistics(LottoDs.LottoRow tablotto)
        {
           
        }
    }
}
