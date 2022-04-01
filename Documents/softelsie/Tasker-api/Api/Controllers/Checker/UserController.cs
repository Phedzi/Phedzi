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
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<User>> Get()
        {
            return await service.GetAll();
        }
        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<User>> Get(int id)
        {
            return await service.Get(id);
        }


        // GET api/<Checking>/5
        [Route("searchByName")]
        public async Task<Response<User>> GetByName(string userName)
        {
            return await service.getUserByUserName(userName);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Response<User>> Create(User Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<User>> Update(User Item)
        {
            return await service.UpdateAsyc(Item);
        }

        // DELETE api/<Checking>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
