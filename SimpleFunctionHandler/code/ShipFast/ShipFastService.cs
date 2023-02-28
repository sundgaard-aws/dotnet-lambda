using System;
using System.Threading.Tasks;
using OM.AWS.Demo.DTL;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.ShipFast
{
    public class ShipFastService : IShippingService
    {
        public ShipFastService() {
        }

        public Task PickupPackageAsync(PackageDTO package, SenderDTO sender, ReceiverDTO receiver)
        {
            Console.WriteLine("Package picked up at sender location.");
            return Task.CompletedTask;
        }

        public Task SendPackageAsync(PackageDTO package, SenderDTO sender, ReceiverDTO receiver)
        {
            Console.WriteLine("Package shipped to sender.");
            Console.WriteLine("Package received by receiver.");
            return Task.CompletedTask;
        }
    }
}