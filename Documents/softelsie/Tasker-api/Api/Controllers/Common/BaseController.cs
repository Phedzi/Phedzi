using System;
using Microsoft.AspNetCore.Mvc;
using Models.Common;

namespace Api.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase
    {
        // GET: api/Base
        [HttpGet]
        public virtual ListResponse<T> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Base/5
        [HttpGet]
        public virtual Response<T> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Base
        [HttpPost]
        public virtual Response<T> Create(T Item)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Base/5
        [HttpPost]
        public virtual Response<T> Upadte(T Item)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost]
        public virtual Response<T> Delete(T Item)
        {
            throw new NotImplementedException();
        }
    }
}
