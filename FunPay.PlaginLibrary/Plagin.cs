using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.Remoting;
using System.Text;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Resto.Front.Api.V5;
using Resto.Front.Api.V5.Attributes;
using Resto.Front.Api.V5.Attributes.JetBrains;
using Resto.Front.Api.V5.Data.Orders;
using Resto.Front.Api.V5.Data.Payments;
using Resto.Front.Api.V5.Extensions;


namespace FunPay.PlaginLibrary
{
    [UsedImplicitly]
    [PluginLicenseModuleId(21005108)]
    public class Plagin : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();
        private IDisposable subscription;
        private CompositeDisposable unsubscribe;

        public Plagin()
        {
            unsubscribe = new CompositeDisposable();
            try
            {
                Core.Start();
                Core.AddDiscontEvent += Core_AddDiscontEvent;
                Subscribe();
                PluginContext.Operations.AddNotificationMessage("Start", "FanPay");
               


            }
            catch (Exception e)
            {

                PluginContext.Operations.AddNotificationMessage(e.Message, "FanPay");
            }

        }

        private void Core_AddDiscontEvent(int discont)
        {
            try
            {
               
               AddDiscont(discont);
              //var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New);
              //  var discountType = PluginContext.Operations.GetDiscountTypes().Last(x => !x.Deleted && x.IsActive && x.DiscountByFlexibleSum);

              //  //PluginContext.Operations.AddFlexibleSumDiscount(discont, discountType, Order, PluginContext.Operations.GetCredentials());
              //  var create = PluginContext.Operations.CreateEditSession();
              //  create.AddFlexibleSumDiscount(discont, discountType, order);
              //  PluginContext.Operations.SubmitChanges(PluginContext.Operations.GetCredentials(), create);
            }
            catch (Exception e)
            {
                
                PluginContext.Operations.AddNotificationMessage(e.Message, "FanPay");
            }
          
        }

        public void AddDiscont(int discont)
        {
            var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New);
            var discountType = PluginContext.Operations.GetDiscountTypes().Last(x => !x.Deleted && x.IsActive && x.DiscountByFlexibleSum);
            if (PluginContext.Operations.CheckCanEditOrder(PluginContext.Operations.GetCredentials(), order))
            {
                PluginContext.Operations.AddFlexibleSumDiscount(discont, discountType, order, PluginContext.Operations.GetCredentials());
                PluginContext.Operations.AddNotificationMessage("Заказ модифицирован", "FanPay");
            }
            else
            {
                PluginContext.Operations.AddNotificationMessage("Невозможность модификации заказа", "FanPay");
            }

            



        }

        private void Subscribe()
        {
            subscription = PluginContext.Notifications.OrderChanged.Subscribe<IOrder>(new Action<IOrder>(OrderChanged));
            
            
        }
        public IOrder Order { get; set; }
        private void OrderChanged([NotNull] IOrder inEvent)
        {
            
          
            
        }


        public Core.Core Core { get; set; } = new Core.Core();


        public void Dispose()
        {
            while (subscriptions.Any())
            {
                var subscription = subscriptions.Pop();
                try
                {
                    subscription.Dispose();
                }
                catch (RemotingException)
                {
                    // nothing to do with the lost connection
                }
            }

            PluginContext.Log.Info("SamplePlugin stopped");
        }


        //private void OrderChanged([NotNull] IOrder inEvent)
        //{
        //}
        private void AddFlexibleSumDiscount()
        {
           
        }
    }

}
