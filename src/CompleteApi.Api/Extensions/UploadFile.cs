using System;
using System.IO;
using System.Threading.Tasks;

namespace CompleteApi.Api.Extensions
{
    public class UploadFile
    {
        public async Task<bool> UploadArquivo(string arquivo, string imgNome)
        {
            var imageDataByteArray = Convert.FromBase64String(arquivo);

            if (string.IsNullOrEmpty(arquivo)) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgNome);

            if (File.Exists(filePath)) return false;

            await File.WriteAllBytesAsync(filePath, imageDataByteArray);

            return true;
        }
    }
}
