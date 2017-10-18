using System;
using Resto.Front.Api.V4;
using Resto.Front.Api.V4.Data.Security;
using Resto.Front.Api.V4.Exceptions;

namespace FunPay.PlaginLibrary4
{
    internal static class OperationServiceExtensions
    {
       

      
        public static ICredentials GetCredentials( this IOperationService operationService,string pin)
        {
            if (operationService == null)
                throw new ArgumentNullException("operationService");

            try
            {
                return operationService.AuthenticateByPin(pin);
            }
            catch (AuthenticationException)
            {
                PluginContext.Operations.AddNotificationMessage("Cannot authenticate. Check pin for plugin user.", "FanPay");
                PluginContext.Log.Warn("Cannot authenticate. Check pin for plugin user.");
                throw;
            }
        }
    }
}
