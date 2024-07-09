using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CleanArc.Web.FileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var category = formCollection["category"].ToString();
                if (string.IsNullOrWhiteSpace(category))
                {
                    category = "Default";
                }

                var folderName = Path.Combine("Resources", "Images", category);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpDelete("{category}/{fileName}")]
        public IActionResult Delete(string category, string fileName)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images", category);
                var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

                if (System.IO.File.Exists(pathToDelete))
                {
                    System.IO.File.Delete(pathToDelete);
                    return Ok(new { message = "File deleted successfully." });
                }
                else
                {
                    return NotFound(new { message = "File not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("{category}/{fileName}")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Update(string category, string fileName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file == null)
                {
                    return BadRequest(new { message = "No file uploaded." });
                }

                var folderName = Path.Combine("Resources", "Images", category);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                // Construct new file path with the new file name
                var newFileName = file.FileName;

                var fullPath = Path.Combine(pathToSave, newFileName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                // Delete the existing file if it exists
                var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);
                if (System.IO.File.Exists(pathToDelete))
                {
                    System.IO.File.Delete(pathToDelete);
                }

                // Save the new file with the new file name
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Return success message
                return Ok(new { message = "File updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("{category}/{fileName}")]
        public IActionResult GetImage(string category, string fileName)
        {
            try
            {
                var folderName = Path.Combine("Resources", "Images", category);
                var pathToRetrieve = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

                if (System.IO.File.Exists(pathToRetrieve))
                {
                    var imageData = System.IO.File.ReadAllBytes(pathToRetrieve);
                    return File(imageData, "image/jpeg"); // Adjust content type based on your image type
                }
                else
                {
                    return NotFound(new { message = "Image not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        public IActionResult GetImages(string category, string imagePath)
        {
            try
            {
                // Combine the base directory, category, and image path to construct the full path to the image file
                var folderName = Path.Combine("Resources", "Images");
                var pathToRetrieve = Path.Combine(Directory.GetCurrentDirectory(), folderName, imagePath);

                // Check if the file exists at the constructed path
                if (System.IO.File.Exists(pathToRetrieve))
                {
                    // Read the file bytes
                    var imageData = System.IO.File.ReadAllBytes(pathToRetrieve);

                    // Get the content type based on the file extension
                    var contentType = GetImageContentType(imagePath);

                    // Return the file with the appropriate content type
                    return File(imageData, contentType);
                }
                else
                {
                    // Return a 404 Not Found response if the file does not exist
                    return NotFound(new { message = "Image not found." });
                }
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response if an exception occurs
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //// Helper method to determine the content type based on the file extension
        //private string GetImageContentType(string fileName)
        //{
        //    var extension = Path.GetExtension(fileName).ToLowerInvariant();
        //    return extension switch
        //    {
        //        ".jpg" or ".jpeg" => "image/jpeg",
        //        ".png" => "image/png",
        //        ".gif" => "image/gif",
        //        ".bmp" => "image/bmp",
        //        _ => "application/octet-stream", // Default for unknown file types
        //    };
        //}
        //[HttpGet]
        //public IActionResult GetImagebypath( string imagePath)
        //{
        //    try
        //    {
        //        // Combine the base directory, category, and image path to construct the full path to the image file
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToRetrieve = Path.Combine(Directory.GetCurrentDirectory(), folderName, imagePath);

        //        // Check if the file exists at the constructed path
        //        if (System.IO.File.Exists(pathToRetrieve))
        //        {
        //            // Read the file bytes
        //            var imageData = System.IO.File.ReadAllBytes(pathToRetrieve);

        //            // Get the content type based on the file extension
        //            var contentType = GetImageContentType(imagePath);

        //            // Return the file with the appropriate content type
        //            return File(imageData, contentType);
        //        }
        //        else
        //        {
        //            // Return a 404 Not Found response if the file does not exist
        //            return NotFound(new { message = "Image not found." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Return a 500 Internal Server Error response if an exception occurs
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}
    }
}

