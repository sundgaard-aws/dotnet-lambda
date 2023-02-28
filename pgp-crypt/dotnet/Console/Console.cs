using FluentFTPService;
using Microsoft.Extensions.DependencyInjection;
using PGPCrypt.SL;
using PGPCrypto;
using PGPDemo.Amazon.S3;
using PGPDemo.Amazon.SecretsManager;
using PGPDemo.DTL;

Console.WriteLine("Started PGP Encrypt Console...");


var sc=new ServiceCollection();
//sc.AddSingleton<IInternalPaymentService, ProPayment>();
sc.AddSingleton<PGPCryptFacade, PGPCryptFacade>();
sc.AddSingleton<S3Facade, S3Facade>();
sc.AddSingleton<IFTPService, OpenSSHFacade>();
sc.AddSingleton<ISecretsService, AWSSecretsManagerService>();
var serviceProvider=sc.BuildServiceProvider();

var pgpCryptFacade=serviceProvider.GetService<PGPCryptFacade>();
if(pgpCryptFacade==null) throw new Exception("Please make sure that ICryptoService is initialized!");
var objectStore=serviceProvider.GetService<S3Facade>();
if(objectStore==null) throw new Exception("Please make sure that IObjectStoreService is initialized!");
var ftpService=serviceProvider.GetService<IFTPService>();
if(ftpService==null) throw new Exception("Please make sure that IFTPService is initialized!");
var secretsService=serviceProvider.GetService<ISecretsService>();
if(secretsService==null) throw new Exception("Please make sure that ISecretsService is initialized!");

//var secret=new SecretDTO();
//secretsService.CreateSecret("secret1", secret).ConfigureAwait(false).GetAwaiter().GetResult();
var secret=secretsService.RestoreSecret<SecretDTO>("demo/secret2").ConfigureAwait(false).GetAwaiter().GetResult();
Console.WriteLine($"KeyType={secret.keyType}");
//var encryptedFile=pgpCryptFacade.EncryptFile();
//await objectStore.UploadFile("pgp-demo-output-archive", encryptedFile);
//ftpService.UploadFile(encryptedFile, "/pgp-demo-output-archive/demo-user/content.aaaa");
//crypt.GenerateKeyPair();
//var decryptedContents=crypt.DecryptFile();
//Console.WriteLine($"Decrypted contents are {decryptedContents}");
Console.WriteLine("Ended PGP Encrypt Console.");