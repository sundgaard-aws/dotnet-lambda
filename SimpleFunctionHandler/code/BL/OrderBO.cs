using System;
using System.Threading.Tasks;
using OM.AWS.Demo.DTL;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.API
{
    public class OrderBO
    {
        private IPaymentService paymentService;
        private IShippingService shippingService;
        private IDatabaseService databaseService;

        public OrderBO(IPaymentService paymentService, IShippingService shippingService) {
            this.paymentService=paymentService;
            this.shippingService=shippingService;
        }

        public async Task ProcessOrderAsync(OrderDTO order)
        {
            Console.WriteLine("Started ProcessOrderAsync...");
            //var secret=new SecretDTO();
            //secretsService.CreateSecret("secret1", secret).ConfigureAwait(false).GetAwaiter().GetResult();
            //var secret = secretsService.RestoreSecret<SecretDTO>("demo/secret2").ConfigureAwait(false).GetAwaiter().GetResult();
            //Console.WriteLine($"KeyType={secret.keyType}");
            await databaseService.SaveAsync<OrderDTO>(order);
            await paymentService.ProcessPaymentAsync();
            order.Status=OrderDTO.StatusEnum.PAID;
            await databaseService.SaveAsync<OrderDTO>(order);
            await shippingService.PickupPackageAsync(null, null, null);
            order.Status=OrderDTO.StatusEnum.PICKED_UP;
            await databaseService.SaveAsync<OrderDTO>(order);
            await shippingService.SendPackageAsync(null, null, null);
            order.Status=OrderDTO.StatusEnum.SENT;
            await databaseService.SaveAsync<OrderDTO>(order);
            Console.WriteLine($"Order ID={order.OrderGUID}");
            Console.WriteLine("Ended ProcessOrderAsync.");
        }
    }
}