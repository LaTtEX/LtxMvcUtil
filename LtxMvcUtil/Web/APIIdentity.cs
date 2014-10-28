using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LtxMvcUtil.Web
{
    public class ApiIdentity : IIdentity
    {
        public ApiIdentity(string username)
        {
            this.Username = username;
        }

        public string Username { get; set; }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return this.Username; }
        }
    }

}
