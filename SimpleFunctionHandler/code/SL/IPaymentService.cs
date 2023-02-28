using System.Threading.Tasks;

namespace OM.AWS.Demo.SL
{
    public interface IPaymentService
    {
        public Task ProcessPaymentAsync();
    }
}