using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository;
using Talabat.Service.DTOs;
using Talabat.Service.Helpers;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        ///public IGenericRepositories<Product> _ProductRepository { get; }
        ///public IGenericRepositories<ProductBrand> _BrandRepositories { get; }
        ///public IGenericRepositories<ProductType> _TypeRepositories { get; } 

        public IMapper _Mapper { get; }

        public ProductController(
            ///IGenericRepositories<Product> ProductRepository,
            ///IGenericRepositories<ProductBrand> BrandRepositories,
            ///IGenericRepositories<ProductType> TypeRepositories,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            ///_ProductRepository = ProductRepository;
            ///_BrandRepositories = BrandRepositories;
            ///_TypeRepositories = TypeRepositories;
            _Mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<Pageation<ProtuctToDto>>> GetAllProducts ([FromQuery]ProductSpecParams productSpecParams)
        { 

            //var Proudcts =await _ProductRepository.GetAllAsync(); 
            var spec = new ProductWithTypeAndBrandSpecfication(productSpecParams);
            var Products =await _unitOfWork.Repository<Product>().GetAllAsyncWithSpec(spec);
            var data = _Mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProtuctToDto>>(Products);
            var SpecWithCount = new ProductWithFilterationWithCountSpec(productSpecParams);
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecAsync(SpecWithCount);

             return Ok(new Pageation<ProtuctToDto>(productSpecParams.PageSize, productSpecParams.PageIndex, count, data));
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<ProtuctToDto>> GetById(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecfication(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpec(spec);
              
            //var product = await _ProductRepository.GetByIdAsync(id);

            return Ok(_Mapper.Map<Product, ProtuctToDto>(product));
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands ()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands); 
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }


    }
}
