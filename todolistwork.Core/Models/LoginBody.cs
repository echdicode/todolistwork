using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolistwork.Core.Models
{
    public class LoginBody
    {
        public LoginBody() { 
            Email = string.Empty;
            Password = string.Empty;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
