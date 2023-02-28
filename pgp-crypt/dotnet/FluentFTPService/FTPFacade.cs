using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;
using PGPCrypt.SL;

namespace FluentFTPService
{
    public class FTPFacade : IFTPService
    {
        public void UploadFile(FileInfo fileToUpload, string ftpTargetFolder)
        {
            throw new System.NotImplementedException();
        }

        public async Task UploadFileAsync(FileInfo fileToUpload, string ftpTargetFolder)
        {
            var token = new CancellationToken();
            using (var ftp = new AsyncFtpClient("127.0.0.1", "ftptest", "ftptest"))
            {
                await ftp.Connect(token);
                //await ftp.UploadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", token: token);
                await ftp.UploadFile(fileToUpload.FullName, ftpTargetFolder + fileToUpload.Name, FtpRemoteExists.Overwrite, true, token: token);
                //await ftp.UploadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpRemoteExists.Overwrite, true, FtpVerify.Retry, token: token);
            }
        }
    }
}