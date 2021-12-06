using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ModelsViews.Auth.Login
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
