using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Checker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Checker;
using Models.Common;


namespace Api.Controllers.Checker
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService service;

        public TaskController(ITaskService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ListResponse<TaskModel>> Get()
        {
            return await service.GetAll();
        }

        // GET api/<Checking>/5
        [HttpGet("{id}")]
        public async Task<Response<TaskModel>> Get(int id)
        {
            return await service.Get(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Response<TaskModel>> Create(TaskModel Item)
        {
            return await service.AddAsyc(Item);
        }

        // PUT api/<Checking>/5
        [Route("update")]
        public async Task<Response<TaskModel>> Update(TaskModel Item)
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
