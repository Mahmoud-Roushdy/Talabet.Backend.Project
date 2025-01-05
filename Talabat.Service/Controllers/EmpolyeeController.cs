using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpolyeeController : ControllerBase
    {
        private IGenericRepositories<Empolyee> _GenericRepositories { get; }
        public EmpolyeeController(IGenericRepositories<Empolyee> genericRepositories)
        {
            _GenericRepositories = genericRepositories;
        }
        [HttpGet]
        public async Task<IEnumerable<Empolyee>> GetAllEmpolyeeWithspec()
        {
            var spec = new EMpolyeeWithDepartmentSpecification();
             var empolyees = await  _GenericRepositories.GetAllAsyncWithSpec(spec);
            return empolyees;
        }

        
    }
}
