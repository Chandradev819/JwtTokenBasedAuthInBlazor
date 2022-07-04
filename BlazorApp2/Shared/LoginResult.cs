using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
