using System;
using System.Globalization;
using System.Net;
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

            var date = GetInternetTime();
            if (date.Date > new DateTime(2017, 12, 16).Date)
                return "Evaluation period has expired.";

            return null;
        }

        DateTime GetInternetTime(Uri url = null)
        {
            if (url == null)
                url = new Uri("Https://www.google.com/");

            try
            {
                var myHttpWebRequest = WebRequest.Create(url);
                var response = myHttpWebRequest.GetResponse();
                var date = response.Headers["date"];
                return DateTime.ParseExact(date,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
            }
            catch
            {
                return DateTime.Now;
            }
        }
    }
}
