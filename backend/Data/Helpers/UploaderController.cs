using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Data.Helpers;

[Produces("application/json")]
[Route("api/Uploader/[action]")]
public  class UploaderController : Controller
{
    private IWebHostEnvironment _environment;

    public UploaderController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }


    [HttpPost("/api/Uploader/Upload")]
    [Produces("application/json")]
    [TypeFilter(typeof(AllowedExtensionsFilterAttribute), Arguments = new object[] { new string[] { ".webp", ".jpeg", ".jpg", ".png" } })]
    [RequestSizeLimit(10L * 1024L * 1024L * 1024L)]
    [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024L * 1024L * 1024L)]
    public async Task<string> Upload(IFormFile file)
    {
        string imagePath = "uploads/images";

        FileInfo fi = new FileInfo(file.FileName);

        var date = DateTime.Now.ToString("hhmmssffffff");
        var uploads = Path.Combine(_environment.WebRootPath, imagePath);
        var setFilename = "Image" + date + fi.Extension;

        var filePath = Path.Combine(uploads, setFilename);
        FileStream fileStream = new FileStream(filePath,
        FileMode.OpenOrCreate,
        FileAccess.ReadWrite,
        FileShare.None);
        await file.CopyToAsync(fileStream);
        try
        {
            await fileStream.DisposeAsync();
            fileStream.Close();

            return "/" + imagePath + "/" + setFilename;
            
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }



}

public class Uploader
{
    public string? link { get; set; }
    public string? tempLink { get; set; }
    public string? ex { get; set; }
}

