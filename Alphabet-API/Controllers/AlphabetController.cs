using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Alphabet_API.Models;
using System.Threading.Tasks;

namespace Alphabet_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/Alphabet")]
    public class AlphabetController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(ValidationResult))]
        public async Task<IHttpActionResult> Post([FromBody] AlphabetModel data)
        {
            if (ModelState.IsValid && data != null)
            {
                var result = Validate(data);
                return Content(HttpStatusCode.OK, result);
            }
            return BadRequest(ModelState);
        }

        private ValidationResult Validate(AlphabetModel data)
        {
            var result = new ValidationResult();
            return result;
        }
    }
}
