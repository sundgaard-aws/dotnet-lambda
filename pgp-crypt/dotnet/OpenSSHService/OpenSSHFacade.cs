using System;
using System.Collections.Generic;
using System.IO;
using PGPCrypt.SL;
using Renci.SshNet;

public class OpenSSHFacade : IFTPService
{
    public void UploadFile(FileInfo fileToUpload, string ftpTargetFolder)
    {
        var localPath = @"C:\temp\";
        var keyFile = new PrivateKeyFile(@"C:\Users\sundgaar\Documents\demo-user.openssh");
        var keyFiles = new[] { keyFile };
        var username = "demo-user";

        var methods = new List<AuthenticationMethod>();
        //methods.Add(new PasswordAuthenticationMethod(username, "password"));
        methods.Add(new PrivateKeyAuthenticationMethod(username, keyFiles));

        var con = new ConnectionInfo("s-10cb592396c24f3eb.server.transfer.eu-central-1.amazonaws.com", 22, username, methods.ToArray());
        using (var client = new SftpClient(con))
        {
            client.Connect();
            Console.WriteLine($"FTPWorkingDirectory={client.WorkingDirectory}");
            client.UploadFile(new FileStream(fileToUpload.FullName, FileMode.Create), ftpTargetFolder);
            //var files = client.ListDirectory("pgp-demo-output-archive");
            //foreach (var file in files)
            //{
              //  Console.WriteLine(file);
                /*using (var fs = new FileStream(localPath + file.Name, FileMode.Create))
                {
                    client.DownloadFile(file.FullName, fs);
                }*/
            //}
        }
    }
}