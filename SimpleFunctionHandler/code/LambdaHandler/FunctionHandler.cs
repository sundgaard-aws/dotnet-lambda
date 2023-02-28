using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using OM.AWS.Demo.Config;
using OM.AWS.Demo.SL;

namespace OM.AWS.Demo.API {
    public class FunctionHandler {
        private static OrderBO orderBO;

        static FunctionHandler() {
            var serviceProvider=AppConfig.Wireup();
            var paymentService=serviceProvider.GetService<IPaymentService>();
            if(paymentService==null) throw new Exception("Please make sure that IPaymentService is initialized!");
            var shippingService=serviceProvider.GetService<IShippingService>();
            if(shippingService==null) throw new Exception("Please make sure that IShippingService is initialized!");
            var objectStore=serviceProvider.GetService<IObjectStoreService>();
            if(objectStore==null) throw new Exception("Please make sure that IObjectStoreService is initialized!");
            var secretsService=serviceProvider.GetService<ISecretsService>();
            if(secretsService==null) throw new Exception("Please make sure that ISecretsService is initialized!");
            var orderBO=serviceProvider.GetService<OrderBO>();
            if(orderBO==null) throw new Exception("Please make sure that OrderBO is initialized!");
            FunctionHandler.orderBO=orderBO;
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<Object> Invoke() {
            await orderBO.ProcessOrderAsync(new DTL.OrderDTO());
            return new { Status="Success", Code=200 };
        }   
    }
}