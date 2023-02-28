using System.IO;
using System.Threading.Tasks;
using PGPDemo.DTL;

namespace PGPCrypt.SL {
    public interface ISecretsService {
        Task CreateSecret<T>(string secretName, T secret);
        Task UpdateSecret<T>(string secretName, T secret);
        Task<T> RestoreSecret<T>(string secretName);
    }
}