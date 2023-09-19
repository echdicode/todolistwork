using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolistwork.Core.Models
{
    public class RegistrationBody
    {
        public RegistrationBody()
        {
            Email = string.Empty;
            Password = string.Empty;
            Password= string.Empty;
            RetypedPassword= string.Empty;
        }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RetypedPassword { get; set; }
    }
}
