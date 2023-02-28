using Microsoft.Extensions.DependencyInjection;
using OM.AWS.Demo.API;
using OM.AWS.Demo.ProPay;
using OM.AWS.Demo.S3;
using OM.AWS.Demo.SecretsManager;
using OM.AWS.Demo.ShipFast;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.Config
{
    public class AppConfig
    {
        public static ServiceProvider Wireup() {
            var sc=new ServiceCollection();
            sc.AddSingleton<IPaymentService, ProPayService>();
            sc.AddSingleton<IShippingService, ShipFastService>();
            sc.AddSingleton<IObjectStoreService, AmazonS3Service>();
            sc.AddSingleton<ISecretsService, AWSSecretsManagerService>();
            sc.AddSingleton<OrderBO, OrderBO>();
            return sc.BuildServiceProvider();
        }
    }
}