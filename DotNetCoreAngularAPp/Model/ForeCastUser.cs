using Microsoft.AspNetCore.Identity;
using System;

namespace DotNetCoreAngularApp.Model
{
    public class ForeCastUser : IdentityUser
    {
        public string Password { get; set; }


        public ForeCastUser()
        {
            
        }

            
    }
}