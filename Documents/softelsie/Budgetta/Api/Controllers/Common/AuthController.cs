using System.Linq;
using System.Threading.Tasks;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.Common;

namespace Api.Controllers.Common
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdministration service;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController( IAdministration service, IHttpContextAccessor _httpContextAccessor)
        {
            this.service = service;
            this._httpContextAccessor = _httpContextAccessor;
        }

        [Route("users")]
        [HttpGet]
        public async Task<PagenatedPlace> GetUsers(string Id ,string search = " ", int PageNumber = 1)
        {
            var model = await service.GetByFilter(Id,search, PageNumber);
            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            
            var outputModel = new PagenatedPlace
            {
                Paging = model.GetHeader(),
                Links = model.GetLinks(_httpContextAccessor, "search", search),
                Items = model.List.Select(m => m).ToList(),
            };

            return outputModel;

        }

        
        [Authorize(Policy = "SAdmin")]
        [HttpPost("register")]
        [HttpPost]
        public async Task<Response<string>> Post([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return await service.CreateAsync(new User()); //service.CreateAsync(new User());

            return await service.CreateAsync(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<AuthResponse> LogIn([FromBody] User user)
        {
            if (!ModelState.IsValid) {
                return await service.SingInAsync(new User());
            }

            var _user = await service.SingInAsync(user);

           return _user;
           
        }

        [HttpPost("changepassword")]
        public async Task<Response<string>> ChangePassword(ChangePasswordUser user)
        {
            if (!ModelState.IsValid)
            {
                return await service.ChangePassword(new ChangePasswordUser());
            }

            var _user = await service.ChangePassword(user);

            return _user;

        }

        // [Authorize(Policy = "SAdmin")]
       /* [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<Response<string>> ResetPassword(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return await service.ResetPassword(new UserModel());
            }

            var _user = await service.ResetPassword(user);

            return _user;

        }*/
    }
}
