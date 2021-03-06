﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resto.Front.Api.V5;
using Resto.Front.Api.V5.Attributes.JetBrains;
using Resto.Front.Api.V5.Data.Security;
using Resto.Front.Api.V5.Exceptions;

namespace FunPay.PlaginLibrary
{
    internal static class OperationServiceExtensions
    {
        

        [NotNull]
        public static ICredentials GetCredentials([NotNull] this IOperationService operationService,string pin)
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
