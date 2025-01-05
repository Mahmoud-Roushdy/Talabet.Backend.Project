using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Service.DTOs;

namespace Talabat.Service.Helpers
{
    public class PictureResolver : IValueResolver<Product, ProtuctToDto, string>
    {
        private IConfiguration _Configuration { get; }
        public PictureResolver(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

  

        public string Resolve(Product source, ProtuctToDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"https://localhost:7184/{source.PictureUrl}";
            else
                return string.Empty;
          
               
        }
    }
}
