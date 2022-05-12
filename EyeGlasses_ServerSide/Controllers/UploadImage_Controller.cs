using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EyeGlasses_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImage_Controller : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length < 0)
                    return BadRequest();

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images/", file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
