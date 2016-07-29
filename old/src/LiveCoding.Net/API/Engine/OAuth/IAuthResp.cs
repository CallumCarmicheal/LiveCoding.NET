using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Engine.OAuth {
    class IAuthResp {
        public string access_token;
        public string token_type;
        public string expires_in;
        public string refresh_token;
        public string scope;
    }
}
