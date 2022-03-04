using System.Threading.Tasks;
using Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Budget;
using Models.Common;

namespace Api.Controllers.Checker
{
   // [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService service;

        public UserTypeController(IUserTypeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<UserTypeModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<UserTypeModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<UserTypeModel>> Create(UserTypeModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<UserTypeModel>> Update(UserTypeModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
