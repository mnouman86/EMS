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
        public async Task<bool> DeleteFileAsync(string category, string fileName)
        {
            var requestUrl = $"/api/Upload/{category}/{fileName}"; // Use route parameters
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFileAsync(string category, string fileName, IFormFile newFile)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(category), "category");
                content.Add(new StringContent(fileName), "fileName");

                using (var fileStream = newFile.OpenReadStream())
                {
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(newFile.ContentType);

                    content.Add(streamContent, "newFile", newFile.FileName);

                    var requestUrl = $"/api/Upload/{category}/{fileName}";
                    var response = await _httpClient.PutAsync(requestUrl, content);

                    return response.IsSuccessStatusCode;
                }
            }
        }
        public async Task<byte[]> GetImageAsync(string category, string fileName)
        {
            try
            {
                var requestUrl = $"/api/Upload/{category}/{fileName}";
                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new ApplicationException($"Failed to retrieve image: {ex.Message}", ex);
            }
        }
    }
}
