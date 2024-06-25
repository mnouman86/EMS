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
    }
}

