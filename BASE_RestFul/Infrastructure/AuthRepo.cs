namespace BASE_RestFul.Infrastructure
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Threading.Tasks;
    public class AuthRepo : IDisposable
    {
        private BaseDbContext context;
        private UserManager<IdentityUser> userManager;


        public AuthRepo()
        {
            context = new BaseDbContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        //Permite registrar el usuario
        public async Task<IdentityResult> Register(UserModel data)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = data.userName
            };

            var result = await userManager.CreateAsync(user, data.password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string username, string password)
        {
            IdentityUser user = await userManager.FindAsync(username, password);
            return user;

        }



        public void Dispose()
        {
            context.Dispose();
            userManager.Dispose();
        }
    }
}