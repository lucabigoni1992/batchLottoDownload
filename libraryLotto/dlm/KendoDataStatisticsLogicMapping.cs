using System;
using System.Collections.Generic;
using System.Text;
using libraryLotto.Data;
using Newtonsoft.Json;

namespace libraryLotto.dlm
{

    public class KendoDataStatisticsLogicMapping
    {
        public class StatisticsDescriptor
        {
            [JsonProperty("field")]
            public string Field { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }

}
