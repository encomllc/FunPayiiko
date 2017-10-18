using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Resto.Front.Api.V4;
using Resto.Front.Api.V4.Attributes;
using Resto.Front.Api.V4.Data.Orders;
using Resto.Front.Api.V4.Data.Screens;
using Resto.Front.Api.V4.Extensions;


namespace FunPay.PlaginLibrary4
{
    
    [PluginLicenseModuleId(21015808)]
    public class Plagin : IFrontPlugin
    {
        private readonly Stack<IDisposable> subscriptions = new Stack<IDisposable>();
        private IDisposable subscription;
        private IDisposable changeWindow;
        public Core.Core Core { get; set; } = new Core.Core();

        public Plagin()
        {
            
            try
            {
                Core.Start();
                Core.AddDiscontEvent += Core_AddDiscontEvent;
                Subscribe();

            }
            catch (Exception e)
            {


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
        private void Subscribe()
        {
            subscription = PluginContext.Notifications.OrderChanged.Subscribe<IOrder>(new Action<IOrder>(OrderChanged));
            changeWindow = PluginContext.Notifications.ScreenChanged.Subscribe<IScreen>(new Action<IScreen>(ChangeScreen));



        }
        private void OrderChanged(IOrder inEvent)
        {



        }
        public void AddDiscont(int discont)
        {
            try
            {
                var order = PluginContext.Operations.GetOrders().Last(o => o.Status == OrderStatus.New);
                //Core.StartMessage(order.ResultSum+"");
                var dis = PluginContext.Operations.GetDiscountTypes();

                
                
              var discountType = PluginContext.Operations.GetDiscountTypes().Last(x => !x.Deleted && x.IsActive && x.DiscountByFlexibleSum);
                




                //Core.StartMessage(dis.Count+"");
                //foreach (var discountType in dis)
                //{
                //    Core.StartMessage(discountType.Name);
                //}

                PluginContext.Operations.AddFlexibleSumDiscount(discont, discountType, order, PluginContext.Operations.GetCredentials(Core.Settings.Password));
               // Core.StartMessage("Скидка успешно добавлена");

            }
            catch (Exception e)
            {
                Core.StartMessage("Ошибка добавления скидки");
                Core.StartMessage(e.Message);
                Core.StartMessage(e.Source);
                Core.StartMessage(e.StackTrace);
            }

        }
        private void ChangeScreen(IScreen screen)
        {
            //Core.IsScreen = true;
            try
            {
                if (screen != null)
                {
                    //Core.StartMessage("type: "+ screen.GetType().ToString());
                    //Core.StartMessage(Core.IsScreen.ToString());
                    Core.IsScreen = false;
                    if (screen.GetType().ToString().Contains("UnknownScreen"))
                    {

                        Core.IsScreen = true;
                    }


                }
            }
            catch (Exception e)
            {
                Core.StartMessage(e.Message);
                Core.StartMessage(e.Source);
                Core.StartMessage(e.StackTrace);

                PluginContext.Operations.AddNotificationMessage(e.Message, "FanPay");
            }

        }

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
    }
}
