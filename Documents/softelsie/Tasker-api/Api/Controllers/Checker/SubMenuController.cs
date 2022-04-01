using System.Threading.Tasks;
using Logic.Checker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Checker;
using Models.Common;

namespace Api.Controllers.Checker
{
   // [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenuController : ControllerBase
    {
        private readonly ISubMenuService service;

        public SubMenuController(ISubMenuService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<SubMenuModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<SubMenuModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<SubMenuModel>> Create(SubMenuModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<SubMenuModel>> Update(SubMenuModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
