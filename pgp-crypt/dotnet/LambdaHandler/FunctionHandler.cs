using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using PGPCrypt.SL;
using PGPCrypto;
using PGPDemo.Amazon.S3;

namespace PGPCrypt.API {
    public class FunctionHandler {
        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<Object> Invoke() {
            var sc=new ServiceCollection();
            //sc.AddSingleton<IInternalPaymentService, ProPayment>();
            sc.AddSingleton<PGPCryptFacade, PGPCryptFacade>();
            sc.AddSingleton<S3Facade, S3Facade>();
            sc.AddSingleton<IFTPService, OpenSSHFacade>();
            var serviceProvider=sc.BuildServiceProvider();

            var pgpCryptFacade=serviceProvider.GetService<PGPCryptFacade>();
            if(pgpCryptFacade==null) throw new Exception("Please make sure that ICryptoService is initialized!");
            var objectStore=serviceProvider.GetService<S3Facade>();
            if(objectStore==null) throw new Exception("Please make sure that IObjectStoreService is initialized!");
            var ftpService=serviceProvider.GetService<IFTPService>();
            if(ftpService==null) throw new Exception("Please make sure that IFTPService is initialized!");

            var encryptedFile=pgpCryptFacade.EncryptFile();
            //await objectStore.UploadFile("pgp-demo-output-archive", encryptedFile);
            await objectStore.UploadFile("pgp-demo-output-archive", encryptedFile);
            ftpService.UploadFile(encryptedFile, "/pgp-demo-output-archive/demo-user/content.aaaa");
            //crypt.GenerateKeyPair();
            //var decryptedContents=crypt.DecryptFile();
            //Console.WriteLine($"Decrypted contents are {decryptedContents}");
            Console.WriteLine("Ended PGP Encrypt Console.");
            return new { Status="Success"};
        }   
    }
}