using System;
using System.Runtime.Serialization;

namespace ActiveDirectoryBroker.Models
{
    [DataContract]
    public class AuthenticationRequest
    {
        [DataMember]
        public string Domain { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        internal string Validate()
        {
            var errorMessage = string.Empty;
            if (string.IsNullOrEmpty(Domain))
                return "Domain name not specified.";
            if (string.IsNullOrEmpty(UserName))
                return "User name not specified.";
            if (string.IsNullOrEmpty(Password))
                return "User's password not specified.";

            var date = DateTime.Now;
            if (date.Date > new DateTime(2017, 12, 16).Date)
                return "Evaluation period has expired.";

            return null;
        }
    }
}
