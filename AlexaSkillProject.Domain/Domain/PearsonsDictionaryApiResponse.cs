using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Domain
{
    [DataContract]
    public class PearsonsDictionaryApiResponse
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "results")]
        public List<ResultSet> Results { get; set; }

        [DataContract]
        public class ResultSet
        {
            [DataMember(Name = "datasets")]
            public string[] Datasets { get; set; }

            [DataMember(Name = "headword")]
            public string Headword { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "part_of_speech")]
            public string PartOfSpeech { get; set; }

            [DataMember(Name = "senses")]
            public List<SenseSet> Senses { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }

            [DataContract]
            public class SenseSet
            {
                // ignore
                public string Translation { get; set; }

                [DataMember(Name = "definition")]
                public dynamic Definition { get; set; }

                [DataMember(Name = "examples")]
                public List<Example> Examples { get; set; }

                [DataContract]
                public class Example
                {
                    [DataMember(Name = "text")]
                    public string Text { get; set; }

                }

            }
        }
    }
}
