using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace CompleteApi.Api.Extensions
{
    public class UploadFile
    {
        public async Task<bool> UploadArquivo(IFormFile arquivo, string imgNome)
        {
            //var imageDataByteArray = Convert.FromBase64String(arquivo);

            if (arquivo == null || arquivo.Length == 0) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgNome + arquivo.FileName);

            if (File.Exists(filePath)) return false;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

                return true;
        }
    }
}
