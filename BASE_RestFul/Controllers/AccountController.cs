namespace BASE_RestFul.Controllers
{
    using Infrastructure;
    using Models;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class AccountController : ApiController
    {
        private AuthRepo repo;

        public AccountController()
        {
            repo = new AuthRepo();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await repo.Register(userModel);

            return GetErrorResult(result) ?? Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState error are available to send, so just return an empty BadRequest
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
