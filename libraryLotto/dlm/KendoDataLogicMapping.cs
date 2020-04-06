using System;
using System.Collections.Generic;
using System.Text;
using libraryLotto.Data;
using Newtonsoft.Json;

namespace libraryLotto.dlm
{

    public class KendoDataLogicMapping
    {
        public class QueryDescriptor
        {
            [JsonProperty("skip")]
            public int Skip { get; set; }
            [JsonProperty("take")]
            public int Take { get; set; }
            [JsonProperty("filter")]
            public QueryDescriptorFilter Filter { get; set; }
            [JsonProperty("group")]
            public ICollection<QueryDescriptorGroup> Group { get; set; }
            [JsonProperty("sort")]
            public ICollection<QueryDescriptorSort> Sort { get; set; }
        }

        public class QueryDescriptorSort
        {
            [JsonProperty("dir")]
            public string Dir { get; set; }
            [JsonProperty("field")]
            public string Field { get; set; }

        }

        public class QueryDescriptorGroup
        {
            [JsonProperty("field")]
            public string Field { get; set; }
            [JsonProperty("operator")]
            public string Operator { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }
        public class QueryDescriptorFilter
        {
            [JsonProperty("logic")]
            public string Logic { get; set; }
            [JsonProperty("filters")]
            public ICollection<QueryDescriptorFilters> Filters { get; set; }
        }
        public class QueryDescriptorFilters
        {
            [JsonProperty("field")]
            public string Field { get; set; }
            [JsonProperty("operator")]
            public string Operator { get; set; }
            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }

}
