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
    public class TrackActionController : ControllerBase
    {
        private readonly ITrackActionService service;

        public TrackActionController(ITrackActionService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<TrackActionModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<TrackActionModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<TrackActionModel>> Create(TrackActionModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<TrackActionModel>> Update(TrackActionModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
