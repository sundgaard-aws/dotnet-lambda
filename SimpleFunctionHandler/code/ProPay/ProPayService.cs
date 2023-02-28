using System;
using System.Threading.Tasks;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.ProPay
{
    public class ProPayService : IPaymentService
    {
        public ProPayService() {
        }

        public Task ProcessPaymentAsync()
        {
            Console.WriteLine("Payment successful.");
            return Task.CompletedTask;
        }
    }
}