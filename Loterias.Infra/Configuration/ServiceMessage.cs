using System.Runtime.Serialization;

namespace Loterias.Infra.Configuration
{
    [DataContract]
    public class ServiceMessage<T>
    {
        [DataMember(Name ="IsSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name ="ReturnMessage")]
        public string ReturnMessage { get; set; }

        [DataMember(Name ="Data")]
        public T Data { get; set; }
    }
}
