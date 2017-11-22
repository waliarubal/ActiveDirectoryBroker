using ActiveDirectoryBroker.Models;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ActiveDirectoryBroker.Services
{
    [ServiceContract]
    public interface IBroker
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AuthenticationResponse Authenticate(AuthenticationRequest request);
    }
}
