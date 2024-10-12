using AutoMapper;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;
        public ImagesController(IImageRepository imageRepository,
            IMapper mapper
            )
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task <IActionResult> UploadImage([FromForm] IFormFile file,
            [FromForm] string fileName,
            [FromForm ]string title)
        {
            ValidateFileUpload(file);
            if(ModelState.IsValid)
            {
                //file upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage=await imageRepository.Upload(file, blogImage);
                return Ok(mapper.Map<BlogImageDTO>(blogImage));
            }
            return BadRequest();
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("File", $"unsopported file format:{Path.GetExtension(file.FileName).ToLower()}");
            }
            if (file.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size cannot be more than 10MB");
            }
        }
    }
}
