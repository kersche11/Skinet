using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTO;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        public readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        public readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo,
                                    IGenericRepository<ProductType> typeRepo,
                                    IGenericRepository<ProductBrand> brandRepo,
                                    IMapper mapper)
        {
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }


         [HttpGet("{id}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if(product==null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }

        [HttpGet("brands")]    
            public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
            {
                    return Ok(await _brandRepo.ListAllAsync());
            }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
                    return Ok(await _productRepo.ListAllAsync());
        }
        
        
    }
}