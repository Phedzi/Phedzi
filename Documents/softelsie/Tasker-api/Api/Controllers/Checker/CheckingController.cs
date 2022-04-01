using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Controllers.Common;
using Logic.Checker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Checker;
using Models.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.Checker
{

    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckingController : ControllerBase
    {
        private readonly ICheckingService service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckingController(ICheckingService service, IHttpContextAccessor _httpContextAccessor)
        {
            this.service = service;
            this._httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet]
        public async Task<ListResponse<CheckingModel>> Get(string owner)
        {
          
            return await service.GetByUser(owner);
        }

        [Route("filter")]
        [HttpGet]
        public async Task<PagenatedPlace> Filter(string search="", string ownerId="", int PageNumber=1)
        {
            var model = await service.GetByCheckingTable( search, ownerId, PageNumber);
            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PagenatedPlace
            {
                Paging = model.GetHeader(),
                Links = model.GetLinks(_httpContextAccessor, "search", search),
                Items = model.List,
            };

            return outputModel;
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<CheckingModel>> Get(int id)
        {
            return await service.Get(id);
        }

        [Route("current")]
        public async Task<Response<CheckingModel>> CurrentCheking()
        {
            return await service.CurrentChecking();
        }

        //[Authorize(Policy = "SAdmin")]
        //[AllowAnonymous]
        [Route("create")]
        [HttpPost]
        public async Task<ListResponse<CheckingModel>> Create(CheckingModel Item)
        {
            var name = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value; 
            return await service.AddAsyc(Item,name);
        }


        [Authorize(Policy = "SAdmin")]
        [Route("update")]
        public async Task<Response<CheckingModel>> Update(CheckingModel Item)
        {

            return await service.UpdateAsyc(Item);
        }

        [Route("pay")]
        public async Task<Response<CheckingModel>> PayChecking(CheckingModel Item)
        {
            return await service.PayChecking(Item);
        }


        [Authorize(Policy = "SAdmin")]
        [Route("approve")]
        [HttpPost]
        public async Task<Response<CheckingModel>> ApproveChecking(CheckingModel check)
        {
            return await service.ApproveChecking(check.Id);
        }

        [Authorize(Policy = "SAdmin")]
        [Route("open")]
        [HttpPost]
        public async Task<Response<CheckingModel>> OpenChecking(CheckingModel check)
        {
            return await service.OpenChecking(check.Id);
        }


        [Authorize(Policy = "SAdmin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
