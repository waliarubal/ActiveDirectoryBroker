using ActiveDirectoryBroker.Models;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ActiveDirectoryBroker.Services
{
    [ServiceContract]
    public interface IBroker
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AuthenticationResponse Authenticate(AuthenticationRequest request);
    }
}
