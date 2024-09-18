using CleanArc.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CleanArc.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FileUploadService _fileUploadService;

        public HomeController(ILogger<HomeController> logger, FileUploadService fileUploadService)
        {
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a file.");
                return View("Index");
            }

            var result = await _fileUploadService.UploadFileAsync(file);
            ViewBag.Message = $"File uploaded successfully. Path: {result.DbPath}";

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string category, string fileName)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(fileName))
            {
                ModelState.AddModelError("File", "Invalid category or file name.");
                return View("Index");
            }

            var result = await _fileUploadService.DeleteFileAsync(category, fileName);
            if (result)
            {
                ViewBag.Message = "File deleted successfully.";
            }
            else
            {
                ModelState.AddModelError("File", "File deletion failed.");
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string category, string fileName, IFormFile newFile)
        {
            if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(fileName) || newFile == null || newFile.Length == 0)
            {
                ModelState.AddModelError("File", "Invalid input.");
                return View("Index");
            }

            var result = await _fileUploadService.UpdateFileAsync(category, fileName, newFile);
            if (result)
            {
                ViewBag.Message = "File updated successfully.";
            }
            else
            {
                ModelState.AddModelError("File", "File update failed.");
            }

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string category, string fileName)
        {
            try
            {
                var imageData = await _fileUploadService.GetImageAsync(category, fileName);

                if (imageData != null && imageData.Length > 0)
                {
                    return File(imageData, "image/jpeg"); // Adjust content type based on your image type
                }
                else
                {
                    return NotFound(new { message = "Image not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving image: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
       
        private string GetImageContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                _ => "application/octet-stream", // Default for unknown file types
            };
        }
       
    }
}
