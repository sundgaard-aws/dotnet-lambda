using System.Threading.Tasks;
using OM.AWS.Demo.DTL;

namespace OM.AWS.Demo.SL
{
    public interface IShippingService
    {
        public Task PickupPackageAsync(PackageDTO package, SenderDTO sender, ReceiverDTO receiver);
        public Task SendPackageAsync(PackageDTO package, SenderDTO sender, ReceiverDTO receiver);
    }
}