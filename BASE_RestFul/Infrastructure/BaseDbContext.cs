using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BASE_RestFul.Infrastructure
{
    public class BaseDbContext : IdentityDbContext<IdentityUser>
    {
        public BaseDbContext()
            : base("DefaultConnectionString")
        {

        }
    }
}