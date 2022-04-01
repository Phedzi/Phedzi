using System.Threading.Tasks;
using Api.Controllers.Common;
using Logic.Checker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Checker;
using Models.Common;

namespace Api.Controllers.Checker
{
   // [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AgreementController(IAgreementService service, IHttpContextAccessor _httpContextAccessor)
        {
            this.service = service;
            this._httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet]
        public async Task<ListResponse<AgreementModel>> Get()
        {
            return await service.GetAll();
        }

        [Route("filter")]
        [HttpGet]
        public async Task<PagenatedPlace> Filter(int page)
        {
            var model = await service.Filter(page);
            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PagenatedPlace
            {
                Paging = model.GetHeader(),
                Links = model.GetLinks(_httpContextAccessor, "none", null),
                Items = model.List,
            };

            return outputModel;
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<AgreementModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<AgreementModel>> Create(AgreementModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<AgreementModel>> Update(AgreementModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
