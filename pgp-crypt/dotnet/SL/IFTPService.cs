using System.IO;

namespace PGPCrypt.SL {
    public interface IFTPService {
        void UploadFile(FileInfo fileToUpload, string ftpTargetFolder);
    }
}