using System.Security.Claims;
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
    public class AgreementTypeController : ControllerBase
    {
        private readonly IAgreementTypeService service;

        public AgreementTypeController(IAgreementTypeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<AgreementTypeModel>> Get()
        {
         

            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<AgreementTypeModel>> Get(int id)
        {
            return await service.Get(id);
        }

    //    [AllowAnonymous]
        [HttpPost]
        public async Task<Response<AgreementTypeModel>> Create(AgreementTypeModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<AgreementTypeModel>> Update(AgreementTypeModel model)
        {
            return await service.UpdateAsyc(model);
        }
    }
}
