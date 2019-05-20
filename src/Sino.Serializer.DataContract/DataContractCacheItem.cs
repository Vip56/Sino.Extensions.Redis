using System.Runtime.Serialization;

namespace Sino.Serializer.DataContract
{
    [DataContract]
    public class DataContractCacheItem
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
