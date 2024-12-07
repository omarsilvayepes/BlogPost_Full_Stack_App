using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var categoryDto=new CategoryDto { Name = request.Name ,UrlHandle=request.UrlHandle};

            await categoryRepository.CreateAsync(categoryDto);
            return Ok(categoryDto);
        }

        //GET: https://localhost:7152/api/Categories?query=java&sortBy=name&sortDirection=desc
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize
            )
        {
            var categoryDtos=await categoryRepository.GetAllAsync(
                query,
                sortBy,
                sortDirection,
                pageNumber,
                pageSize);

            return Ok(categoryDtos);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var categoryDto = await categoryRepository.GetById(id);

            if(categoryDto == null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditCategory([FromRoute] Guid id,UpdateCategoryRequesDto request)
        {
            var categoryDto = new CategoryDto { Id=id,Name = request.Name, UrlHandle = request.UrlHandle };
            var response = await categoryRepository.UpdateAsync(categoryDto);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var response = await categoryRepository.DeleteAsync(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
