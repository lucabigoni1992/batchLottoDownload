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
            public object value1 { get; set; }
            public object value2 { get; set; }

            public Struct_lotto_Statistics(string field, object value)
            {
                this.field = field;
                this.value1 = value.ToString();
                this.value2 = "";
            }
            public Struct_lotto_Statistics(string field, object value1, object value2)
            {
                this.field = field;
                this.value1 = value1.ToString();
                this.value2 = value2;
            }
            public Struct_lotto_Statistics()
            {
                this.field = "";
                this.value1 = "";
                this.value2 = "";
            }
        }
    }
}
