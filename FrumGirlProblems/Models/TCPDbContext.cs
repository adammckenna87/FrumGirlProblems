using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FrumGirlProblems.Models
{
    public class TCPDbContext : IdentityDbContext<TCPUser>
    {
        public TCPDbContext(DbContextOptions options) : base(options)
        {

        }

        protected TCPDbContext()
        {

        }
    }

    public class TCPUser : IdentityUser
    {

    }
}
