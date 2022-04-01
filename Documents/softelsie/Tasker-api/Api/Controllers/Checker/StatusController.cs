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
    public class StatusController : ControllerBase
    {
        private readonly IStatusService service;

        public StatusController(IStatusService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<StatusModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<StatusModel>> Get(int id)
        {
            return await service.Get(id);
        }

        //[AllowAnonymous]
        [HttpPost]
        public async Task<Response<StatusModel>> Create(StatusModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<StatusModel>> Update(StatusModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
