using ETicaretServer.Application.Repositories;
using ETicaretServer.Application.Services;
using ETicaretServer.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_productReadRepository.GetAll(false));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           
            var result = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product product)
        {
            await _productWriteRepository.AddAsync(new(){
                Name = "Table",
                Price = 3500,
                Stock = 400
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPost("action")]
        public async Task<IActionResult> Upload(IFormFileCollection formFiles)
        {
            //todo formFiles -> Request.Form.Files

            await _fileService.UploadAsync("resource/product-images", formFiles);
          
            return Ok("Gorsel Eklendi");
        }
    }
}
