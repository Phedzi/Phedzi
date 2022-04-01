using System.Threading.Tasks;
using Logic.Checker;
using Microsoft.AspNetCore.Mvc;
using Models.Checker;
using Models.Common;

namespace Api.Controllers.Checker
{
   // [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService service;

        public MenuController(IMenuService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<MenuModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<MenuModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<MenuModel>> Create(MenuModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<MenuModel>> Update(MenuModel model)
        {
            return await service.UpdateAsyc(model);
        }

        // PUT api/<Checking>/5
        [Route("update/sub")]
        public async Task<Response<MenuModel>> UpdateSub(MenuModel model)
        {
            return await service.UpdateSub(model);
        }
    }
}
