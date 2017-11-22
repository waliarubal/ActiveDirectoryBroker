using ActiveDirectoryBroker.Models;
using System;
using System.DirectoryServices.AccountManagement;

namespace ActiveDirectoryBroker.Services
{
    public class Broker : IBroker
    {
        public AuthenticationResponse Authenticate(AuthenticationRequest request)
        {
            var response = new AuthenticationResponse();
            if (request == null)
            {
                response.Message = "Empty request received.";
                return response;
            }

            response.Message = request.Validate();
            if (!string.IsNullOrEmpty(response.Message))
                return response;

            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, request.Domain))
                {
                    response.IsValid = context.ValidateCredentials(request.UserName, request.Password, ContextOptions.Negotiate);
                    response.Message = response.IsValid ? string.Empty : "Invalid user name and/or password.";
                }
            }
            catch(Exception ex)
            {
                
                response.Message = ex.Message;
            }
            

            return response;
        }
    }
}
