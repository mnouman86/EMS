using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CleanArc.Web.UI.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace CleanArc.Web.UI
{

    public class FileUploadService
    {
        private readonly HttpClient _httpClient;

        public FileUploadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UploadFileResponse> UploadFileAsync(IFormFile file)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = file.OpenReadStream())
                {
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                    content.Add(streamContent, "file", file.FileName);

                    var response = await _httpClient.PostAsync("/api/Upload", content);

                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    var uploadFileResponse = JsonConvert.DeserializeObject<UploadFileResponse>(responseString);

                    return uploadFileResponse;
                }
            }
        }
    }

}
