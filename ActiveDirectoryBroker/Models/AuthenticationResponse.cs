using System.Runtime.Serialization;

namespace ActiveDirectoryBroker.Models
{
    [DataContract]
    public class AuthenticationResponse
    {
        [DataMember]
        public bool IsValid { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
