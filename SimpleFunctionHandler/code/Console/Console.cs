using Microsoft.Extensions.DependencyInjection;
using OM.AWS.Demo.API;
using OM.AWS.Demo.Config;
using OM.AWS.Demo.DTL;
using OM.AWS.Demo.ProPay;
using OM.AWS.Demo.S3;
using OM.AWS.Demo.SecretsManager;
using OM.AWS.Demo.ShipFast;
using OM.AWS.Demo.SL;

Console.WriteLine("Started Simple Console...");

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


await orderBO.ProcessOrderAsync(new OrderDTO());
//var secret=new SecretDTO();
//secretsService.CreateSecret("secret1", secret).ConfigureAwait(false).GetAwaiter().GetResult();
//var secret=secretsService.RestoreSecret<SecretDTO>("demo/secret2").ConfigureAwait(false).GetAwaiter().GetResult();
//Console.WriteLine($"KeyType={secret.keyType}");
//var encryptedFile=pgpCryptFacade.EncryptFile();
//await objectStore.UploadFile("pgp-demo-output-archive", encryptedFile);
//ftpService.UploadFile(encryptedFile, "/pgp-demo-output-archive/demo-user/content.aaaa");
//crypt.GenerateKeyPair();
//var decryptedContents=crypt.DecryptFile();
//Console.WriteLine($"Decrypted contents are {decryptedContents}");
Console.WriteLine("Ended Simple Console.");